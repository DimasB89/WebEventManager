using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebEventManager.Models
{
    
    public class Participant
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] public int ParticipantID { get; set; }
        public int NumberOfParticipants { get; set; }
        public string Name { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

        /*public Participant(PaymentMenthod payMethod, string info = "")
        {
            PaymentMenthod = payMethod;
            AdditionalInformation = info;
        }*/

    }
}
