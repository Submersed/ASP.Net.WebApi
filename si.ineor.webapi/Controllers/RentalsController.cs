using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using si.ineor.webapi.Authorization;
using si.ineor.webapi.Entities;

namespace si.ineor.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IneorwebapiContext _context;

        public RentalsController(IneorwebapiContext context)
        {
            _context = context;
        }

        // GET: api/Rentals
        [Authorize(Role.Admin)]
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Rental>>> GetAllRentals()
        {
            if (_context.Rental == null)
            {
                return NotFound();
            }

            return await _context.Rental.ToListAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Rental>>> GetUserRentals(Guid userId)
        {
          if (_context.Rental == null)
          {
              return NotFound();
          }

            return await _context.Rental.Where(user => user.Id == userId).ToListAsync();
        }

        // PUT: api/Rentals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(Guid id, Rental rental)
        {
            if (id != rental.Id)
            {
                return BadRequest();
            }

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
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

        // POST: api/Rentals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rental>> PostRental(Rental rental)
        {
          if (_context.Rental == null)
          {
              return Problem("Entity set 'IneorwebapiContext.Rental'  is null.");
          }
            _context.Rental.Add(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRental", new { id = rental.Id }, rental);
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(Guid id)
        {
            if (_context.Rental == null)
            {
                return NotFound();
            }
            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            _context.Rental.Remove(rental);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentalExists(Guid id)
        {
            return (_context.Rental?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
