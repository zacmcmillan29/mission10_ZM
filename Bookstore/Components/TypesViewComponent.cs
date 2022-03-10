using System;
using System.Linq;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Components
{
    public class TypesViewComponent : ViewComponent
    {
        private IBookstoreRepository bookYo { get; set; }

        public TypesViewComponent (IBookstoreRepository temp)
        {
            bookYo = temp;
        }

        public IViewComponentResult Invoke()
        {
            //ViewBag.SelectedType = RouteData?.Values["category"]; //means can be nullable
            var categories = bookYo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x); //default order

            return View(categories); // pass the query of WHAT will be shown and in what order into the view
        }
    }
}
