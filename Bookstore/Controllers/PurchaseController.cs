using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using WaterProject.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.Controllers
{
    public class PurchaseController : Controller
    {

        //p
        private IPurchaseRepository repo { get; set; }
        private Basket basket { get; set; }


        //constructor
        public PurchaseController(IPurchaseRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }


        // GET: /<controller>/
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Purchase());
        }


        //Good ole post
        [HttpPost]
        public IActionResult Checkout (Purchase p)
        {
            if (basket.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your model is empty!");
            }

            if (ModelState.IsValid)
            {
                p.Lines = basket.Items.ToArray();
                repo.SavePurchase(p);
                basket.ClearBasket();

                return RedirectToPage("/PurchaseConfirmed");
            }

            else
            {
                return View();
            }
        }
    }
}
