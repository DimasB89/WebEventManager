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
        private List<(string name, long id, bool isPerson, int actualID)> _participants;

        public DetailsModel(WebEventManager.Data.EMContext context)
        {
            _context = context;
            _participants = new List<(string name, long id, bool isPerson, int actualID)> { };
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        [BindProperty]
        public Company NewCompany { get; set; } = default!;

        [BindProperty]
        public PrivatePerson NewPrivatePerson { get; set; } = default!;

        [BindProperty]
        public Attendance NewAttendance { get; set; } = default!;

        public IEnumerable<(string name, long id, bool isPerson, int actualID)> GetParticipants { get; set; }

        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.FirstOrDefaultAsync(m => m.EventID == id);
            if (Event == null)
            {
                return NotFound();
            }

            List<Participant> participants = await _context.Attendances
                .Where(a => a.EventID == Event.EventID)
                .Select(a => _context.Participants.FirstOrDefault(p => p.ParticipantID == a.ParticipantID))
                .ToListAsync();

            foreach (var participant in participants)
            {
                PrivatePerson person = _context.PrivatePersons.FirstOrDefault(p => p.PrivatePersonID == participant.ParticipantID);
                if (person != null)
                {
                    _participants.Add((person.FullName, person.PersonalID, true, person.PrivatePersonID));
                }
                else
                {
                    Company company = _context.Companies.FirstOrDefault(c => c.CompanyID == participant.ParticipantID);
                    if (company != null)
                    {
                        _participants.Add((company.Name, company.RegistryNumber, false, company.CompanyID));
                    }
                }
            }

            GetParticipants = _participants;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var selectedParticipantType = Request.Form["ParticipantType"].ToString();

            var fieldsToSkip = selectedParticipantType == "Company"
                ? new List<string> { "NewPrivatePerson.PersonalID", "NewPrivatePerson.FirstName", "NewPrivatePerson.LastName" }
                : new List<string> { "NewCompany.Name", "NewCompany.RegistryNumber", "NewCompany.NumberOfParticipants" };

            foreach (var field in fieldsToSkip)
            {
                ModelState.ClearValidationState(field);
                ModelState.MarkFieldValid(field);
            }


            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("NewPrivatePerson.PersonalID", "Vale ID");
                return Page();
            }

            var participant = selectedParticipantType switch
            {
                "PrivatePerson" => new Participant(NewPrivatePerson.FirstName, NewPrivatePerson.LastName, NewPrivatePerson.PersonalID),
                "Company" => new Participant(NewCompany.Name, NewCompany.RegistryNumber, NewCompany.NumberOfParticipants),
                _ => throw new ArgumentException("Invalid participant type")
            };

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            var attendance = new Attendance(participant.ParticipantID, Event.EventID, NewAttendance.PaymentMethod, NewAttendance.AdditionalInformation);
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Event.EventID });
        }


    }
}
