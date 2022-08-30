using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Content
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Contents { get; set; }
        public byte[] Image { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Colour { get; set; }
        public string size { get; set; }
        public double  bill { get; set; }
        public double total { get; set; }
        public int regID { get; set; }
      

       
       
        public double calcbill()
        {
            return Price * Quantity;
        }

    }
}