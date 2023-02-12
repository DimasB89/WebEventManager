using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebEventManager.Data;
using WebEventManager.Models;

namespace WebEventManager.Pages
{
    public class IndexModel : PageModel
    {

        private readonly EMContext _context;
        private DateTime _currentDateTime;
        private List<Event> _upcomingEvents;
        private List<Event> _pastEvents;

        public IndexModel(EMContext context)
        {
            _context = context;
            _currentDateTime = DateTime.Now;
            _upcomingEvents = new List<Event>();
            _pastEvents = new List<Event>();
        }

        public IEnumerable<Event> GetUpcomingEvents { get; set; }
        public IEnumerable<Event> GetPastEvents { get; set; }

        
        public void OnGet()
        {
            IEnumerable<Event> events = _context.Events.ToList();
            foreach (Event e in events)
            {
                if(DateTime.Compare(e.DateTime, _currentDateTime) > 0)
                {
                    _upcomingEvents.Add(e);
                }
                else
                {
                    _pastEvents.Add(e);
                }
            }
            GetUpcomingEvents = _upcomingEvents;
            GetPastEvents = _pastEvents;
        }
    }
}