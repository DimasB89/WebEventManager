using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEventManager.Models
{
    public class Company 
    {
        [ForeignKey("Participant")]
        public int CompanyID { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "vähemalt 3 tähemärki")]
        public string Name { get; set; }
        [Required]
        [Utilities.MaxLengthInt(8, ErrorMessage = "maksimaalselt 8 numbrit")]
        public int RegistryNumber { get; set; }
        [Required]
        [Range(1,100000, ErrorMessage = "vähemalt 1, maksimaalselt 100 000")]
        public int NumberOfParticipants { get; set; }

        public virtual Participant Participant { get; set; }

        public Company(string name, int registryNumber, int numberOfParticipants)
        {
            Name = name;
            RegistryNumber = registryNumber;
            NumberOfParticipants = numberOfParticipants;
        }

        public Company() { } //todo add the company int values a default value
    }
}
