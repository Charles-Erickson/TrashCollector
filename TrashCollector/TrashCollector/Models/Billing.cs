﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrashCollector.Models
{
    public class Billing
    {
        [Key]
        public int BillingId { get; set; }
        public double BillAmount { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer customer { get; set; }
    }
}