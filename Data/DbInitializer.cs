using System.Diagnostics;
using WebEventManager.Models;

namespace WebEventManager.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EMContext context)
        {
            // Look for any participants.
            if (context.Participants.Any())
            {
                return;   // DB has been seeded
            }

            var participants = new Participant[]
            {
                new Participant("Person1", "P1Surname", 38912104444),
                new Participant("Person2", "P2Surname", 38912105555),
                new Participant("Person3", "P3Surname", 38912106666),
                new Participant("Person4", "P4Surname", 38912107777),
                new Participant("Company1", 12345678, 10),
                new Participant("Company2", 12121212, 4),
                new Participant("Company3", 14141414, 12),
                new Participant("Company4", 13131313, 15),
            };

            context.Participants.AddRange(participants);
            context.SaveChanges();

            var events = new Event[]
            {
                new Event{Name="Event1", DateTime = new DateTime(2023, 12, 20, 12, 0,0), Place = "Event1Place", AdditionalInformation = "testing Event1"},
                new Event{Name="Event2", DateTime = new DateTime(2023, 11, 21, 12, 0,0), Place = "Event2Place", AdditionalInformation = "testing Event2"},
                new Event{Name="Event5", DateTime = new DateTime(2022, 12, 22, 12, 0,0), Place = "Event5Place", AdditionalInformation = "testing Event5"},
                new Event{Name="Event6", DateTime = new DateTime(2022, 11, 23, 12, 0,0), Place = "Event6Place", AdditionalInformation = "testing Event6"},
            };

            context.Events.AddRange(events);
            context.SaveChanges();

            var attendances = new Attendance[]
            {
                new Attendance{ParticipantID=1, EventID=1, PaymentMethod = PaymentMethod.CreditCard, AdditionalInformation = "brings cake"},
                new Attendance{ParticipantID=2, EventID=1, PaymentMethod = PaymentMethod.Cash, AdditionalInformation = "brings joy"},
                new Attendance{ParticipantID=3, EventID=2, PaymentMethod = PaymentMethod.CreditCard, AdditionalInformation = "brings flowers"},
                new Attendance{ParticipantID=4, EventID=2, PaymentMethod = PaymentMethod.Cash, AdditionalInformation = "brings fireworks"},
                new Attendance{ParticipantID=5, EventID=3, PaymentMethod = PaymentMethod.CreditCard, AdditionalInformation = "brings fun"},
                new Attendance{ParticipantID=6, EventID=3, PaymentMethod = PaymentMethod.Cash, AdditionalInformation = "brings happiness"},
                new Attendance{ParticipantID=7, EventID=4, PaymentMethod = PaymentMethod.CreditCard, AdditionalInformation = "brings no errors"},
                new Attendance{ParticipantID=8, EventID=4, PaymentMethod = PaymentMethod.Cash, AdditionalInformation = "brings jobs for everyone"},

            };

            context.Attendances.AddRange(attendances);
            context.SaveChanges();
        }
    }
}
