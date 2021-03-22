using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_Development.Models;

namespace Web_Development.Pages
{
    public class Index : PageModel
    {

        public List<Product> PreviewItems { get; set; }



        public IActionResult OnGet()
        {
            //Get 4 (random or 4 first) products
            //These will be shown on the homepage
            //PreviewItems = getRecords  
            return Page();



        }


    }
}