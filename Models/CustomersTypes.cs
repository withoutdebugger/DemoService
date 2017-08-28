using System;
using System.Collections.Generic;

namespace DemoService.Models
{
    public partial class CustomersTypes
    {
        public CustomersTypes()
        {
            Customers = new HashSet<Customers>();
        }

        public int CustomerTypeId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
    }
}
