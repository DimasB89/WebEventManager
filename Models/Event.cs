using System.ComponentModel.DataAnnotations;

namespace WebEventManager.Models
{
    public class Event
    {
        [Key] public int EventID { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Place { get; set; }
        public string AdditionalInformation { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

    }
}
