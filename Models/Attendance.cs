using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEventManager.Models
{

    public enum PaymentMethod
    {
        CreditCard,
        Cash
    }

    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }
        public int ParticipantID { get; set; }
        public int EventID { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string AdditionalInformation { get; set; }

        [ForeignKey("ParticipantID")]
        public Participant Participant { get; set; }

        [ForeignKey("EventID")]
        public Event Event { get; set; }

        public Attendance() { }
        public Attendance(int participantID, int eventID, PaymentMethod paymethod, string info)
        {
            ParticipantID = participantID;
            EventID = eventID;
            PaymentMethod = paymethod;
            AdditionalInformation = info;
        }
    }
}
