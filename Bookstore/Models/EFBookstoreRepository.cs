using System;
using System.Linq;

namespace Bookstore.Models
{



    public class EFBookstoreRepository : IBookstoreRepository
    {
        private BookstoreContext context { get; set; }

        public EFBookstoreRepository(BookstoreContext temp)
        {
            context = temp;
        }

        public IQueryable<Book> Books => context.Books;




        //-------- CRUD -------------


        public void SaveBook(Book b)
        {
            //go to context file --> liason between project and database!

            context.SaveChanges();
        }

        public void CreateBook(Book b)
        {
            context.Add(b);
            context.SaveChanges();
        }

        public void DeleteBook(Book b)
        {
            context.Remove(b);
            context.SaveChanges();
        }

        //public IQueryable<Book> Books => throw new NotImplementedException();
    }
}
