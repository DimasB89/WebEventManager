using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace WebEventManager.Models
{
    
    public class Participant
    {
        [Key] 
        public int ParticipantID { get; set; }
        //public int PrivatePersonID { get; set; }
        //public int CompanyID { get; set; }
        
        public ICollection<Attendance> Attendances { get; set; }

        public virtual PrivatePerson Person { get; set; }
        public virtual Company Company { get; set; }

        public Participant() { }
        public Participant(string firstName, string lastName, long personalID) {
            Person = new PrivatePerson(firstName, lastName, personalID);    
        }
        public Participant(string companyName, int registryNumber, int numberOfParticipants) {
            Company = new Company(companyName, registryNumber, numberOfParticipants);
        }

    }
}
