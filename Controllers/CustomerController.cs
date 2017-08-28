using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DemoService.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<Customers> GetCustomers()
        {
            return _context.Customers;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customers = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerId == id);

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        [HttpGet]
        [Route("{numberPage}/{perPage}")]
        public ViewModelPage GetCustomerPaging(int numberPage, int perPage)
        {

            int _customersCount = GetCustomers().Count();
            int _countPages = (_customersCount / perPage) + ( _customersCount % perPage == 0 ? 0 : 1);

            var page = new ViewModelPage();
            page.ActualPage = numberPage;
            page.CountCustomers = GetCustomers().Count();
            page.CountPages = _countPages;
            page.customers = GetCustomers().Skip(numberPage * perPage).Take(perPage).ToList();

            return page;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomers([FromRoute] int id, [FromBody] Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customers.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public  IActionResult PostCustomers([FromBody] Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customers);
             _context.SaveChanges();

            return CreatedAtAction("GetCustomers", new { id = customers.CustomerId }, customers);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customers = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();

            return Ok(customers);
        }

        private bool CustomersExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

    }
}