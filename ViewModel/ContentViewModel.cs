using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Ecommerce.ViewModel
{
    public class ContentViewModel
    {
            /// <summary>
            /// Get and Set id
            /// </summary>
            public int ID { get; set; }
            /// <summary>
            /// Get and set title of content 
            /// </summary>
            [Required]
            public string Title { get; set; }
            /// <summary>
            /// Get and set Description for content
            /// </summary>
            [Required]
            public string Description { get; set; }
            /// <summary>
            /// Get and set Content for content
            /// </summary>
            [AllowHtml]
            [Required]
            public string Contents { get; set; }
            /// <summary>
            /// Images
            /// </summary>
            [Required]
            public byte[] Image { get; set; }
            public double Price { get; set; }
            public int Quantity { get; set; }
            public string Colour { get; set; }
            public string size { get; set; }
        public double bill { get; set; }
        public double total { get; set; }
        public double calcbill()
        {
            return Price * Quantity;
        }

    }
    }
