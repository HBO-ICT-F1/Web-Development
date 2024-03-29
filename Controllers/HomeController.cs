﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Development.Utils;

namespace Web_Development.Controllers
{
    public class HomeController : BaseController
    {
        private readonly Database _database;

        public HomeController(Database database, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _database = database;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            ViewBag.records = _database.Records
                .OrderByDescending(record => record.Id)
                .Take(5);
            return View("Index");
        }
    }
}