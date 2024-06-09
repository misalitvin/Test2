using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DTOs;
using WebApplication2.Entities;


namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api")]
    public class Controller : ControllerBase
    {
        private readonly MyContext _context;

        public Controller(MyContext context)
        {
            _context = context;
        }
        [HttpGet("getReservations/{id:int}")]
        public async Task<IActionResult> GetReservations(int id)
        {
            var reservations = await _context.Reservations
                .Where(r => r.IdClient == id)
                .Include(r => r.Client) 
                .ToListAsync();
            if (reservations == null)
            {
                return NotFound();
            }
        
            return Ok(reservations);
        }

        [HttpPost("addreservation")]
        public async Task<IActionResult> AddReservation([FromBody] ReservationDto form)
        {
            if (await MoreReservationsExist(form.IdClient))
            {
                return BadRequest("Client has more than one reservation");
            }
            /*if (await EnoughBoats(form.NumOfBoats))
            {
                return BadRequest("We dont have enough boats");
            }*/
            var newReservation = await _context.Reservations.AddAsync(CreateReservation(form));
            await _context.SaveChangesAsync();
            
            return Ok(newReservation.Entity.IdReservation);
        }
        [HttpDelete("/deletereservation/{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
    
            if (reservation == null)
            {
                return BadRequest("There is no such reservation");
            }
    
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return Ok("Reservation removed");
        }
        [HttpPut("updatereservation/{id}")]
        public async Task<IActionResult> UpdateReservation(int id, ReservationDto form)
        {
            var existingReservation = await _context.Reservations.FindAsync(id);

            if (existingReservation == null)
            {
                return NotFound("Reservation not found");
            }

            existingReservation.DateFrom = form.DateFrom;
            existingReservation.DateTo = form.DateTo;
            existingReservation.NumOfBoats = form.NumOfBoats;


            _context.Reservations.Update(existingReservation);
            await _context.SaveChangesAsync();

            return Ok(existingReservation);
        }
        
        
        private Reservation CreateReservation(ReservationDto form)
        {
            return new Reservation()
            {
                IdClient = form.IdClient,
                DateTo = form.DateTo,
                DateFrom = form.DateFrom,
                NumOfBoats = form.NumOfBoats,
                IdBoatStandard = form.IdBoatStandard,
                Fullfilled = false,
                Capacity = form.NumOfBoats
            };
        }
        private async Task<bool> MoreReservationsExist(int IdClient)
        {
            var count = await _context.Reservations
                .CountAsync(c => IdClient==c.IdClient);
            if (count < 1)
            {
                return false;
            }
            return true;
        }
        private async Task<bool> EnoughBoats(int reqBoats)
        {
            var count = _context.Sailboats.CountAsync().Result -
                        _context.Sailboats.CountAsync(c => c.SailboatReservations != null).Result;
            if (count<reqBoats)
            {
                return false;
            }
            return true;
        }
       
    }
}