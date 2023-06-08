using FirstAttempt.Data;
using FirstAttempt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FirstAttempt.Controllers
{
    public class DriverController : Controller
    {
        private readonly FirstAttemptContext _context;

        public DriverController(FirstAttemptContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> home()
        {
            int DriID = Convert.ToInt32(HttpContext.Session.GetString("driid"));
            int BusID = Convert.ToInt32(HttpContext.Session.GetString("BusID"));
            var bus = await _context.Bus.FromSqlRaw("SELECT * From Bus Where DriID = '" + DriID + "' ").ToListAsync();
			var shift = await _context.Schedule.FromSqlRaw("SELECT * From Schedule Where BusID = '"+ BusID +"'").OrderBy(b => b.Time).ToListAsync();
            ViewBag.Schedule = shift;
			ViewBag.Bus = bus;
            DateTime Today = DateTime.Today;
            ViewBag.Condition = await _context.Condition.FromSqlRaw("SELECT Passenger.PassID, Passenger.PassCondition, Ticket.ScheduleID, Ticket.TicketDate FROM Passenger RIGHT JOIN Ticket ON Passenger.PassID = Ticket.PassID WHERE PassCondition = 1 AND TicketDate = {0}", Today).ToListAsync();
            
            
            return View();
            
            
        }
    }
    }

