using System;
using System.Collections.Generic;

namespace DemoService.Models
{
    public partial class Customers
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public string EmailAddress { get; set; }
        public int? CustomerTypeId { get; set; }
        public string Notes { get; set; }

        public virtual CustomersTypes CustomerType { get; set; }
    }
}
