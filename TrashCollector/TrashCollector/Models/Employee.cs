﻿using System;
using System.Collections.Generic;
using System.Linq;
using Owin;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrashCollector.Models;

namespace TrashCollector
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ZipCode { get; set; }



        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        //[ForeignKey("Customers")]
        //public int CustomersId { get; set; }
        //public Customer Customers { get; set;}

        public List<bool> PickUps { get; set; }


    }
}