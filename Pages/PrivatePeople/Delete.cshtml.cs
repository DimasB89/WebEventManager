using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebEventManager.Data;
using WebEventManager.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebEventManager.Pages.PrivatePeople
{
    public class DeleteModel : PageModel
    {
        private readonly WebEventManager.Data.EMContext _context;

        public DeleteModel(WebEventManager.Data.EMContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PrivatePerson PrivatePerson { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PrivatePersons == null)
            {
                return NotFound();
            }

            var privateperson = await _context.PrivatePersons.FirstOrDefaultAsync(m => m.PrivatePersonID == id);

            if (privateperson == null)
            {
                return NotFound();
            }
            else 
            {
                PrivatePerson = privateperson;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PrivatePersons == null)
            {
                return NotFound();
            }
            var privateperson = await _context.PrivatePersons.FindAsync(id);

            if (privateperson != null)
            {
                foreach (Attendance a in _context.Attendances)
                {
                    if (a.ParticipantID == privateperson.PrivatePersonID)
                    {
                        _context.Attendances.Remove(a);
                        break;
                    }
                }
                //////////
                PrivatePerson = privateperson;
                _context.PrivatePersons.Remove(PrivatePerson);

                //find the table row in participat and remove it too
                foreach (Participant p in _context.Participants)
                {
                    if (p.ParticipantID == privateperson.PrivatePersonID)
                    {
                        _context.Participants.Remove(p);
                        break;
                    }
                }
                /////////
                
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
