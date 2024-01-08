using Xunit;
using HealthCare.Core;
using HealthCare.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using HealthCare.Core.Data;

namespace HealthCareTests
{
    /*public class FeedbackServiceTests : IDisposable
    {
        private readonly HealthCareContext _context;
        private readonly FeedbackService _feedbackService;

        public FeedbackServiceTests()
        {
            // Use a unique database name for each test
            var options = new DbContextOptionsBuilder<HealthCareContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new HealthCareContext(options);
            _feedbackService = new FeedbackService(_context);
        }

        [Fact]
        public void Should_Add_A_Feedback_To_DB_Using_FeedbackService()
        {
            // Arrange
            var feedback = new Feedback { Id = Guid.NewGuid(), Title = "Great Service", Comment = "Thank you for the excellent care!", Time = DateTime.Now };

            // Act
            _feedbackService.AddFeedback(feedback);

            // Assert
            var addedFeedback = _context.Feedbacks.FirstOrDefault(f => f.Title == "Great Service");
            Assert.NotNull(addedFeedback);
            Assert.Equal(feedback.Comment, addedFeedback.Comment);
        }


        [Fact]
        public void Should_Intentionally_Fail_With_Empty_Comment()
        {
            // Arrange
            var feedback = new Feedback { Id = Guid.NewGuid(), Title = "Intentional Failure", Comment = string.Empty, Time = DateTime.Now };

            // Act
            _feedbackService.AddFeedback(feedback);

            // Assert
            var addedFeedback = _context.Feedbacks.FirstOrDefault();
            Assert.Null(addedFeedback); // Ensure that no feedback was added due to empty comment
        }


        public void Dispose()
        {
            // Dispose of the context after each test
            _context.Dispose();
        }
    }*/
}
