using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Day8.Models
{
    public class Customer:IdentityUser
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       // public int CustomerID { get; set; }
        public int budget { get; set; }

    }
}