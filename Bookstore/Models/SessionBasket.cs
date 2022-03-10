using System;
using System.Text.Json.Serialization;
using Bookstore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Models
{
    public class SessionBasket : Basket
    {
       //return instance of a basket!
       public static Basket GetBasket(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //this line says is this a NEW session or an old session
            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket();

            //need to set our session object Session to something
            basket.Session = session;

            return basket;
        }

        [JsonIgnore]
        public ISession Session { get; set; }



        //--------override the parent class methods!!-----------

        //add item
        public override void AddItem(Book book, int qty)
        {
            base.AddItem(book, qty);
            Session.SetJson("Basket", this);
        }

        //remove item
        public override void RemoveItem(Book book)
        {
            base.RemoveItem(book);
            Session.SetJson("Basket", this);

        }

        //clear basket
        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("Basket");
        }
    }
}
