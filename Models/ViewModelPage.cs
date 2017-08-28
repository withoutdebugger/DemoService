using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoService.Models;

namespace DemoService.Models
{
    public class ViewModelPage
    {
        public int CountPages { get; set; }
        public int CountCustomers { get; set; }
        public int ActualPage { get; set; }
        public IList<Customers> customers { get; set; }
    }
}
