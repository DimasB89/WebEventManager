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
    public class IndexModel : PageModel
    {
        private readonly WebEventManager.Data.EMContext _context;

        public IndexModel(WebEventManager.Data.EMContext context)
        {
            _context = context;
        }

        public IList<PrivatePerson> PrivatePerson { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PrivatePersons != null)
            {
                PrivatePerson = await _context.PrivatePersons
                .Include(p => p.Participant).ToListAsync();
            }
        }
    }
}
