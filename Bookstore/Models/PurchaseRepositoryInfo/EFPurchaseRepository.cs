using System;
using System.Linq;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace WaterProject.Models
{
    public class EFPurchaseRepository : IPurchaseRepository
    {
        // --------- getting the data from the context file! -------------

        //private instance of the Context file!
        private BookstoreContext context;

        //adding in the contstructor
        public EFPurchaseRepository(BookstoreContext temp)
        {
            context = temp;
        }

        public IQueryable<Purchase> Purchases =>
            context.Purchases
            .Include(x => x.Lines)         //gets the details of the donation
            .ThenInclude(x => x.Book);  //gets the detials of the project itself!

        public void SavePurchase(Purchase p)
        {
            context.AttachRange(p.Lines.Select(x => x.Book));

            //if new or id is 0
            if (p.PurchaseId == 0)
            {
                context.Purchases.Add(p);
            }

            context.SaveChanges();
        }
    }
}
