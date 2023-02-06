using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Moq;
using WebEventManager.Data;
using WebEventManager.Models;
using WebEventManager.Pages.Events;
using Xunit;

namespace WebEventManager.Tests.Pages.Events
{
    public class DetailsModelTests
    {
        private readonly EMContext _context;

        public DetailsModelTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EMContext>();
            optionsBuilder.UseInMemoryDatabase("TestDb");
            _context = new EMContext(optionsBuilder.Options);
        }

        [Fact]
        public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var model = new DetailsModel(_context);

            // Act
            var result = await model.OnGetAsync(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnGetAsync_ReturnsNotFound_WhenEventIsNotFound()
        {
            // Arrange
            var model = new DetailsModel(_context);
            int? id = 1;

            // Act
            var result = await model.OnGetAsync(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnGetAsync_ReturnsPageResult_WhenEventIsFound()
        {
            // Arrange
            var model = new DetailsModel(_context);
            var evento = new Event
            {
                EventID = 1,
                Name = "Test event",
                DateTime = DateTime.Now,
                Place = "Test location"
            };
            _context.Events.Add(evento);
            await _context.SaveChangesAsync();
            int? id = evento.EventID;

            // Act
            var result = await model.OnGetAsync(id);

            // Assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenModelStateIsNotValid()
        {
            // Arrange
            var model = new DetailsModel(_context);
            model.ModelState.AddModelError("error", "error");
            var httpContext = new DefaultHttpContext();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>{{ "ParticipantType", "Company" }});
            httpContext.Request.Form = formCollection;
            model.PageContext.HttpContext = httpContext;

            // Act
            var result = await model.OnPostAsync();

            // Assert
            Assert.IsType<PageResult>(result);
        }

    }
}
