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

namespace WebEventManager.Pages.Companies
{
    public class EditModel : PageModel
    {
        private readonly WebEventManager.Data.EMContext _context;

        public EditModel(WebEventManager.Data.EMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Company Company { get; set; } = default!;

        [BindProperty]
        public Attendance Attendance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company =  await _context.Companies.FirstOrDefaultAsync(m => m.CompanyID == id);
            if (company == null)
            {
                return NotFound();
            }
            Company = company;
           ViewData["CompanyID"] = new SelectList(_context.Participants, "ParticipantID", "ParticipantID");

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

            _context.Attach(Company).State = EntityState.Modified;
            _context.Attach(Attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(Company.CompanyID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");//go back to event details page
        }

        private bool CompanyExists(int id)
        {
          return _context.Companies.Any(e => e.CompanyID == id);
        }
    }
}
