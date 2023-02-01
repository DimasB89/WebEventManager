using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEventManager.Models
{
    public class PrivatePerson
    {
        [ForeignKey("Participant")]
        public int PrivatePersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        [RegularExpression(@"^[1-6]{1}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|[1-2][0-9]|3[0-1])[0-9]{4}$", ErrorMessage = "Invalid ID")]
        public long PersonalID { get; set; }

        public virtual Participant Participant { get; set; }

        public PrivatePerson(string firstName, string lastName, long personalID)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = firstName + " " +  lastName;
            PersonalID = personalID;
        }

        public PrivatePerson() { }
    }
}
