﻿using System;
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
            
            foreach (Attendance a in _context.Attendances.ToList())
            {
                if (a.EventID.Equals(Event.EventID))
                {
                    foreach (Participant p in _context.Participants.ToList())
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
                foreach (PrivatePerson person in _context.PrivatePersons)
                {
                    if (person.PrivatePersonID.Equals(participant.ParticipantID))
                    {
                        _participants.Add((person.FullName, person.PersonalID, true, person.PrivatePersonID));
                    }
                }
                foreach (Company company in _context.Companies)
                {
                    if (company.CompanyID.Equals(participant.ParticipantID))
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

                foreach (var value in ModelState.Values)
                {
                    if (value.Errors.Count > 0)
                    {
                        foreach (var error in value.Errors)
                        {
                            Console.WriteLine(error.ErrorMessage);
                        }
                    }
                }
                return Page();
            }


            //add participant
            Participant participant = new Participant();
            if (selectedParticipantType == "PrivatePerson")
            {
                participant = new Participant(NewPrivatePerson.FirstName, NewPrivatePerson.LastName, NewPrivatePerson.PersonalID);
            }
            else if (selectedParticipantType == "Company")
            {
                participant = new Participant(NewCompany.Name, NewCompany.RegistryNumber, NewCompany.NumberOfParticipants);
            }
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            //add attendance
            NewAttendance.ParticipantID = participant.ParticipantID;
            Attendance attendance = new Attendance(NewAttendance.ParticipantID, Event.EventID, NewAttendance.PaymentMethod, NewAttendance.AdditionalInformation);
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Event.EventID });
        }

    }
}
