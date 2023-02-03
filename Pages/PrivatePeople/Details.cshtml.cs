using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebEventManager.Data;
using WebEventManager.Models;

namespace WebEventManager.Pages.PrivatePeople
{
    public class DetailsModel : PageModel
    {
        private readonly WebEventManager.Data.EMContext _context;

        public DetailsModel(WebEventManager.Data.EMContext context)
        {
            _context = context;
        }

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
    }
}
