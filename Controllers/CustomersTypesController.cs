using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoService.Models;

namespace DemoService.Controllers
{
   
    [Route("api/[controller]")]
    public class CustomersTypesController : Controller
    {
        private readonly DemoDBContext _context;

        public CustomersTypesController(DemoDBContext context)
        {
            _context = context;
        }

        // GET: api/CustomersTypes
        [HttpGet]
        public IEnumerable<CustomersTypes> GetCustomersTypes()
        {
            return _context.CustomersTypes;
        }

        // GET: api/CustomersTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomersTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customersTypes = await _context.CustomersTypes.SingleOrDefaultAsync(m => m.CustomerTypeId == id);

            if (customersTypes == null)
            {
                return NotFound();
            }

            return Ok(customersTypes);
        }

        // PUT: api/CustomersTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomersTypes([FromRoute] int id, [FromBody] CustomersTypes customersTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customersTypes.CustomerTypeId)
            {
                return BadRequest();
            }

            _context.Entry(customersTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersTypesExists(id))
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

        // POST: api/CustomersTypes
        [HttpPost]
        public async Task<IActionResult> PostCustomersTypes([FromBody] CustomersTypes customersTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CustomersTypes.Add(customersTypes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomersTypesExists(customersTypes.CustomerTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomersTypes", new { id = customersTypes.CustomerTypeId }, customersTypes);
        }

        // DELETE: api/CustomersTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomersTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customersTypes = await _context.CustomersTypes.SingleOrDefaultAsync(m => m.CustomerTypeId == id);
            if (customersTypes == null)
            {
                return NotFound();
            }

            _context.CustomersTypes.Remove(customersTypes);
            await _context.SaveChangesAsync();

            return Ok(customersTypes);
        }

        private bool CustomersTypesExists(int id)
        {
            return _context.CustomersTypes.Any(e => e.CustomerTypeId == id);
        }
    }
}