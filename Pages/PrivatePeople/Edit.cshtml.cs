using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebEventManager.Data;
using WebEventManager.Models;

namespace WebEventManager.Pages.PrivatePeople
{
    public class EditModel : PageModel
    {
        private readonly WebEventManager.Data.EMContext _context;

        public EditModel(WebEventManager.Data.EMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PrivatePerson PrivatePerson { get; set; } = default!;

        [BindProperty]
        public Attendance Attendance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PrivatePersons == null)
            {
                return NotFound();
            }

            var privateperson =  await _context.PrivatePersons.FirstOrDefaultAsync(m => m.PrivatePersonID == id);
            if (privateperson == null)
            {
                return NotFound();
            }
            PrivatePerson = privateperson;
           ViewData["PrivatePersonID"] = new SelectList(_context.Participants, "ParticipantID", "ParticipantID");


            var attendance = await _context.Attendances.FirstOrDefaultAsync(l => l.ParticipantID == id);
            if (attendance == null)
            {
                return NotFound();
            }
            Attendance = attendance;
            ViewData["AttendanceID"] = new SelectList(_context.Attendances, "AttendanceID", "AttendanceID");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PrivatePerson.FullName = PrivatePerson.FirstName + " " + PrivatePerson.LastName;

            _context.Attach(PrivatePerson).State = EntityState.Modified;
            _context.Attach(Attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrivatePersonExists(PrivatePerson.PrivatePersonID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Events/Details", new { id = Attendance.EventID });
        }

        private bool PrivatePersonExists(int id)
        {
          return _context.PrivatePersons.Any(e => e.PrivatePersonID == id);
        }
    }
}
