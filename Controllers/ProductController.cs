using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Development.Middleware.Auth;
using Web_Development.Models;
using Web_Development.Utils;

namespace Web_Development.Controllers
{
    public class ProductController : BaseController
    {
        private readonly Database _database;

        [BindProperty] public NewProduct CreateNewProduct { get; set; }

        public ProductController(Database database, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _database = database;
        }

        [HttpPost("/product/lookup")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public Dictionary<long, string> Lookup()
        {
            var dictionary = new Dictionary<long, string>();
            if (string.IsNullOrEmpty(Request.Form["lookup"]))
                return dictionary;

            var records = _database.Records
                .Where(record => record.Title.Contains(Request.Form["lookup"]) ||
                                 record.Artist.Contains(Request.Form["lookup"]))
                .Take(5);
            foreach (var record in records)
            {
                dictionary.Add(record.Id, $"{record.Title} - {record.Artist}");
            }

            return dictionary;
        }

        [HttpGet("/product/create")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Add()
        {
            return View("Create");
        }

        [HttpPost("/product/create")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Create()
        {
            if (!ModelState.IsValid)
            {
                SetErrorTempData();
                ViewBag.createNewProduct = CreateNewProduct;
                ViewBag.record =
                    _database.Records.FirstOrDefault(record => record.Id == Convert.ToInt64(CreateNewProduct.RecordId));
                return View("Create");
            }

            _database.Products.Add(new Product
            {
                RecordId = Convert.ToInt64(CreateNewProduct.RecordId),
                UserId = user.Id,
                Price = Convert.ToDouble(CreateNewProduct.Price),
                ForSale = false,
                PlateCondition = Enum.Parse<Condition>(CreateNewProduct.PlateCondition),
                SleeveCondition = Enum.Parse<Condition>(CreateNewProduct.SleeveCondition),
                CreatedAt = DateTime.Now,
                Format = CreateNewProduct.Format,
                Label = CreateNewProduct.Label
            });
            _database.SaveChanges();
            return RedirectToAction("Index", "Account");
        }

        [HttpGet("/product/update/{productId}")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Edit(int productId)
        {
            var product = ViewBag.product = _database.Products
                .Include(product => product.Record)
                .FirstOrDefault(product => product.Id == productId);
            if (product == null) return RedirectToAction("Index", "Account");
            return View("Update");
        }

        [HttpPost("/product/update/{productId}")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Update(int productId)
        {
            var product = _database.Products
                .Include(product => product.Record)
                .FirstOrDefault(product => product.Id == productId);
            if (product == null) return RedirectToAction("Edit");
            if (!ModelState.IsValid)
            {
                SetErrorTempData();
                ViewBag.createNewProduct = CreateNewProduct;
                ViewBag.product = product;
                return View("Update");
            }
            product.RecordId = Convert.ToInt32(CreateNewProduct.RecordId);
            product.Price = Convert.ToDouble(CreateNewProduct.Price);
            product.PlateCondition = Enum.Parse<Condition>(CreateNewProduct.PlateCondition);
            product.SleeveCondition = Enum.Parse<Condition>(CreateNewProduct.SleeveCondition);
            product.Format = CreateNewProduct.Format;
            product.Label = CreateNewProduct.Label;
            _database.Products.Update(product);
            _database.SaveChanges();
            return RedirectToAction("Index", "Account");
        }

        private void SetErrorTempData()
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(CreateNewProduct);
            Validator.TryValidateObject(CreateNewProduct, context, validationResults, true);

            foreach (var x in validationResults.ToArray())
            {
                TempData[$"error_for_{x.MemberNames.First()}"] = x.ErrorMessage;
            }
        }
    }

    public class NewProduct
    {
        [Required(ErrorMessage = "Je moet een record selecteren!")]
        [Range(1, long.MaxValue, ErrorMessage = "De record is een niet geldige waarde!")]
        public string RecordId { get; set; }

        [Required(ErrorMessage = "Je moet een prijs opgeven!")]
        [Range(0d, double.MaxValue, ErrorMessage = "De prijs is een niet geldige waarde!")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Je moet een format opgeven!")]
        public string Format { get; set; }

        [Required(ErrorMessage = "Je moet een label opgeven!")]
        public string Label { get; set; }

        [Required(ErrorMessage = "Je moet een plaat conditie selecteren!")]
        [EnumDataType(typeof(Condition), ErrorMessage = "Je moet een conditie selecteren die bestaat!")]
        public string PlateCondition { get; set; }

        [Required(ErrorMessage = "Je moet een sleeve conditie selecteren!")]
        [EnumDataType(typeof(Condition), ErrorMessage = "Je moet een conditie selecteren die bestaat!")]
        public string SleeveCondition { get; set; }
    }
}