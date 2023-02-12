using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebEventManager.Data;
using WebEventManager.Models;
using Xunit;
using WebEventManager.Pages.Events;

namespace WebEventManager.Tests.Pages.Events
{
    public class DeleteEventTests
    {
        [Fact]
        public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var context = GetContext();
            var model = new DeleteModel(context);

            // Act
            var result = await model.OnGetAsync(null);

            // Assert
            var redirectToActionResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnGetAsync_ReturnsNotFound_WhenEventNotFound()
        {
            // Arrange
            var context = GetContext();
            var model = new DeleteModel(context);

            // Act
            var result = await model.OnGetAsync(0);

            // Assert
            var redirectToActionResult = Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task OnPostAsync_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var context = GetContext();
            var model = new DeleteModel(context);

            // Act
            var result = await model.OnPostAsync(null);

            // Assert
            var redirectToActionResult = Assert.IsType<NotFoundResult>(result);
        }


        private EMContext GetContext()
        {
            var options = new DbContextOptionsBuilder<EMContext>()
                .UseInMemoryDatabase(databaseName: "DeleteModelTests")
                .Options;

            return new EMContext(options);
        }
    }
}
