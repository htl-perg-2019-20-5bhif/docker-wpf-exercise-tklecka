using docker_wpf_exercise_tklecka.Data;
using docker_wpf_exercise_tklecka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace docker_wpf_exercise_tklecka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly CarDataContext _context;

        public ReservationsController(CarDataContext context)
        {
            _context = context;
        }

        // GET: api/Reservations/all
        [HttpGet]
        [Route("all", Name = "get all")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.Include(r => r.Car).ToListAsync();
        }

        // GET: api/Reservations/id/5
        [HttpGet]
        [Route("id/{id}", Name = "get by id")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }
        // GET: api/Reservations/date/2020-01-09T00:00:00
        [HttpGet]
        [Route("date/{date}", Name = "get by Date")]
        public async Task<ActionResult<IEnumerable<Car>>> GetReservation(string date)
        {
            DateTime datet = DateTime.Parse(date);
            var reservations = await _context.Reservations.Where(r => r.ReservationDay.Equals(datet)).ToListAsync();
            var cars = await _context.Cars.ToListAsync();

            if (reservations == null)
            {
                return cars;
            }
            foreach (Reservation r in reservations)
            {
                cars.Remove(r.Car);
            }
            if (cars == null)
            {
                return NotFound("All Cars are Reserved!");
            }
            return cars;

        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.ID)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            var reservations = await _context.Reservations
                .Where(r => r.ReservationDay
                .Equals(reservation.ReservationDay))
                .ToListAsync();
            foreach (Reservation r in reservations)
            {
                if (r.CarID == reservation.CarID)
                {
                    return BadRequest("Is Reserved");
                }
            }
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.ID }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ID == id);
        }
    }
}
