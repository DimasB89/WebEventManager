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
    public class DetailsModel : PageModel
    {
        private readonly WebEventManager.Data.EMContext _context;

        public DetailsModel(WebEventManager.Data.EMContext context)
        {
            _context = context;
        }

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
    }
}
