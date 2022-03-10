using System;
using System.Linq;
using Bookstore.Models;

namespace WaterProject.Models
{
    public interface IPurchaseRepository
    {
        public IQueryable<Purchase> Purchases { get; }

        public void SavePurchase(Purchase p);
    }
}
