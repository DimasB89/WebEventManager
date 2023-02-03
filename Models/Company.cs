using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEventManager.Models
{
    public class Company 
    {
        [ForeignKey("Participant")]
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public int RegistryNumber { get; set; }
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
