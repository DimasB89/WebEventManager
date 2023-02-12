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
                new Attendance(1, 1, PaymentMethod.BankTransfer, "brings cake"),
                new Attendance(2, 1, PaymentMethod.Cash, "brings joy"),
                new Attendance(3, 2, PaymentMethod.BankTransfer, "brings flowers"),
                new Attendance(4, 2, PaymentMethod.Cash, "brings fireworks"),
                new Attendance(5, 3, PaymentMethod.BankTransfer, "brings fun"),
                new Attendance(6, 3, PaymentMethod.Cash, "brings happiness"),
                new Attendance(7, 4, PaymentMethod.BankTransfer, "brings no errors"),
                new Attendance(8, 4, PaymentMethod.Cash, "brings jobs for everyone"),
            };

            context.Attendances.AddRange(attendances);
            context.SaveChanges();
        }
    }
}
