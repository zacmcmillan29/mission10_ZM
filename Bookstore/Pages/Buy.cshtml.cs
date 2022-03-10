using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Infrastructure;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookstore.Pages
{
    public class BuyModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }


        //here are the variable CLASSES, but we need to actually set them to values later
        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }


        public BuyModel (IBookstoreRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

       



        public void OnGet(string returnUrl)
        {
            //basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            ReturnUrl = returnUrl ?? "/"; //or if it's null, just go back home!
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            //setup basket first
            //basket = new Basket();
            //basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            basket.AddItem(b, 1);

            //setting the json when they click donate!
            //saves from page to page
            //HttpContext.Session.SetJson("basket", basket);    --> this is done in the SessionBasket!

            //return URL
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }


        //later add OnPostRemove here
        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            //comes from the Basket Class!
            basket.RemoveItem(basket.Items.First(x => x.Book.BookId == bookId).Book);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
