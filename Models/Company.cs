using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEventManager.Models
{
    public class Company : Participant
    {
        public string Name { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)] public int RegistryNumber { get; set; }
        public int NumberOfParticipants { get; set; }

        public Company(string name, int registryNumber, int numberOfParticipants) //: base(payMethod, info)
        {
            Name = name;
            RegistryNumber = registryNumber;
            NumberOfParticipants = numberOfParticipants;
        }
    }
}
