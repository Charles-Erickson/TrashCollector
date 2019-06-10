using System;
using System.Collections.Generic;
using Owin;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        [Display(Name="Pickups will occur on a weekly basis from the start of the servic" )]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime OneTimePickUp { get; set; }
        public string OneTimePickUpDay { get; set; }
        public string DayOfWeek { get; set; }
        [DataType(DataType.Currency)]
        public double BillAmount { get; set; }


        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }


        [ForeignKey("ApplicationUser")]
       public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Billing")]
        public int BillingId { get; set; }
        public virtual Billing Billing { get; set; }
    }
}