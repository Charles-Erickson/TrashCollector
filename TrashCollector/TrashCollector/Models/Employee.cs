﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrashCollector
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        
    }
}