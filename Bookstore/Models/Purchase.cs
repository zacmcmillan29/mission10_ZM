using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bookstore.Models
{
    public class Purchase
    {
        [Key]            //uniquely identify EACH donation
        [BindNever] //info can't be passed/shown in the URL
        public int PurchaseId { get; set; }


        //keeps track of WHICH thing we donated to!
        [BindNever]
        public ICollection<BasketLineItem> Lines { get; set; }

        [Required(ErrorMessage = "Please enter a name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the first address name:")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name:")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter the state name:")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter a country name:")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter the zipcode:")]
        public string Zip { get; set; }

        public bool Anonymous { get; set; }
    }
}
