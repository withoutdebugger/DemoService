using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DemoService.Models;
using System.Linq;

namespace DemoService.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly DemoDBContext _context;

        public CustomersController(DemoDBContext context)
        {
            _context = context;         
        }       

        [HttpGet]
        public IEnumerable<Customers> GetAll()
        {
            return _context.Customers.ToList();
        }

    }
}