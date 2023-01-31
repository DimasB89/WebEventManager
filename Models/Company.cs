using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEventManager.Models
{
    public class Company 
    {
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public int RegistryNumber { get; set; }
        public int NumberOfParticipants { get; set; }

        public Company(string name, int registryNumber, int numberOfParticipants)
        {
            Name = name;
            RegistryNumber = registryNumber;
            NumberOfParticipants = numberOfParticipants;
        }

        public Company() { }
    }
}
