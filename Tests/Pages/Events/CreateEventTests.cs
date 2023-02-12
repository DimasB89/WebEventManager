using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebEventManager.Data;
using WebEventManager.Models;
using WebEventManager.Pages.Events;
using Xunit;

namespace WebEventManager.Tests.Pages.Events
{
    public class CreateEventTests
    {
        private readonly EMContext _context;

        public CreateEventTests()
        {
            var options = new DbContextOptionsBuilder<EMContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new EMContext(options);
        }

        [Fact]
        public async Task OnPostAsync_WhenEventIsValid_AddsEventToDatabase()
        {
            var createModel = new CreateModel(_context);
            var eventToAdd = new Event
            {
                Name = "Test Event",
                Place = "Test Place",
                DateTime = DateTime.Now
            };

            createModel.Event = eventToAdd;

            var result = await createModel.OnPostAsync();

            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("../Index", (result as RedirectToPageResult).PageName);
            Assert.Equal(1, _context.Events.Count());
            Assert.Equal("Test Event", _context.Events.First().Name);
            Assert.Equal("Test Place", _context.Events.First().Place);
        }

    }
}
