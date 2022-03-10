using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bookstore.Models
{
    public class Basket
    {
        //part 1 declares, 2nd= instantiates
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();

        public virtual void AddItem(Book book, int qty)
        {
            BasketLineItem line = Items
                .Where(p => p.Book.BookId == book.BookId)
                .FirstOrDefault();

            if (line == null)
            {
                Items.Add(new BasketLineItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }

        }


        //removing one item
        public virtual void RemoveItem(Book book)
        {
            Items.RemoveAll(x => x.Book.BookId == book.BookId);
        }



        //clearing one item
        public virtual void ClearBasket()
        {
            Items.Clear();
        }




        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * 25);

            return sum;
        }

    }


    //line item (just one)
    public class BasketLineItem
    {
        [Key]
        public int LineID { get; set; }
        //INSTANCE OF A PROJECT, not the type/class itself!
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }

}

