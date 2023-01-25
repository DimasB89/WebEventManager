using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEventManager.Models
{
    public class PrivatePerson : Participant
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] public int PersonalID { get; set; }

        public PrivatePerson(string firstName, string lastName, int personalID) //: base(payMethod, info)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = firstName + " " +  lastName;
            PersonalID = personalID;
        }
    }
}
