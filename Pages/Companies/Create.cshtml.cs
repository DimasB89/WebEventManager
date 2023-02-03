using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebEventManager.Data;
using WebEventManager.Models;

namespace WebEventManager.Pages.Companies
{
    public class CreateModel : PageModel
    {
        private readonly WebEventManager.Data.EMContext _context;

        public CreateModel(WebEventManager.Data.EMContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CompanyID"] = new SelectList(_context.Participants, "ParticipantID", "ParticipantID");
            return Page();
        }

        [BindProperty]
        public Company Company { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Companies.Add(Company);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
