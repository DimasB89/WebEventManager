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
        //private List<Participant> _participants;
        private List<(string name, long id)> _participants;

        public DetailsModel(WebEventManager.Data.EMContext context)
        {
            _context = context;
            _participants = new List<(string name, long id)> { };
        }

        public Event Event { get; set; }
        public IEnumerable<(string name, long id)> GetParticipants { get; set; }

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
            List<Participant> participants = new List<Participant>();
            /*participants.AddRange(from Attendance a in _context.Attendances.ToList()
                                   where a.EventID.Equals(Event.EventID)
                                   select _context.Participants.Find(a.ParticipantID));*/
            foreach(Attendance a in _context.Attendances.ToList())
            {
                if (a.EventID.Equals(Event.EventID))
                {
                    foreach(Participant p in _context.Participants.ToList())
                    {
                        if (a.ParticipantID.Equals(p.ParticipantID))
                        {
                            participants.Add(p);
                        }
                    }
                }
            }


            foreach (var participant in participants)
            {
                foreach(PrivatePerson person in _context.PrivatePersons)
                {
                    if (person.PrivatePersonID.Equals(participant.ParticipantID))
                    {
                        _participants.Add((person.FullName, person.PersonalID));
                    }
                }
                foreach(Company company in _context.Companies)
                {
                    if (company.CompanyID.Equals(participant.ParticipantID))
                    {
                        _participants.Add((company.Name, company.RegistryNumber));
                    }
                }
            }
            GetParticipants = _participants;

            return Page();
        }
    }
}
