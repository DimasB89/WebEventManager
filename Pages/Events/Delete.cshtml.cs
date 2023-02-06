using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebEventManager.Data;
using WebEventManager.Models;

namespace WebEventManager.Pages.Events
{
    public class DeleteModel : PageModel
    {
        private readonly WebEventManager.Data.EMContext _context;

        public DeleteModel(WebEventManager.Data.EMContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var evento = await _context.Events.FirstOrDefaultAsync(m => m.EventID == id);

            if (evento == null)
            {
                return NotFound();
            }
            else 
            {
                Event = evento;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }
            var evento = await _context.Events.FindAsync(id);

            if (evento != null)
            {
                Event = evento;
                _context.Events.Remove(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Index");
        }
    }
}
