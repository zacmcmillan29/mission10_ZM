using System;
using System.Linq;
using Bookstore.Models;

namespace Bookstore.Models
{
    public interface IPurchaseRepository
    {
        public IQueryable<Purchase> Purchases { get; }

        public void SavePurchase(Purchase p);
    }
}
