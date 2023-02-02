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
        public int AttendanceID { get; set; }
        public int ParticipantID { get; set; }
        public int EventID { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string AdditionalInformation { get; set; }

        [ForeignKey("ParticipantID")]
        public Participant Participant { get; set; }

        [ForeignKey("EventID")]
        public Event Event { get; set; }
    }
}
