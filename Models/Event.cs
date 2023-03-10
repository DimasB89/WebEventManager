using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEventManager.Models
{
    public class Event
    {
        [Key] public int EventID { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "vähemalt 3 tähemärki")]
        public string Name { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        [MaxLength(30)]
        public string Place { get; set; }
        public string AdditionalInformation { get; set; }
        [NotMapped]
        public string DateTimeString { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

    }
}
