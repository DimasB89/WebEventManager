using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebEventManager.Data;
using WebEventManager.Models;

namespace WebEventManager.Pages.Companies
{
    public class DeleteModel : PageModel
    {
        private readonly WebEventManager.Data.EMContext _context;

        public DeleteModel(WebEventManager.Data.EMContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Company Company { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FirstOrDefaultAsync(m => m.CompanyID == id);

            if (company == null)
            {
                return NotFound();
            }
            else 
            {
                Company = company;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }
            var company = await _context.Companies.FindAsync(id);
            int eventID = 1;

            if (company != null)
            {
                //added attendance removal code
                foreach (Attendance a in _context.Attendances)
                {
                    if (a.ParticipantID == company.CompanyID)
                    {
                        eventID = a.EventID;
                        _context.Attendances.Remove(a);
                        break;
                    }
                }
                //////////
                Company = company;
                _context.Companies.Remove(Company);

                //find the table row in participat and remove it too
                foreach(Participant p in _context.Participants)
                {
                    if(p.ParticipantID == company.CompanyID)
                    {
                        _context.Participants.Remove(p);
                        break;
                    }
                }
                /////////
                
                await _context.SaveChangesAsync();
            }

            //return RedirectToPage("./Index");
            return RedirectToPage("../Events/Details", new { id = eventID });
        }
    }
}
