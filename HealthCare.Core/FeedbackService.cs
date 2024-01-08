using HealthCare.Core.Data;
using HealthCare.Core.Models;


namespace HealthCare.Core
{
    public class FeedbackService

    {
        private readonly HealthCareContext _dbContext;

        public FeedbackService(HealthCareContext dbContext)
        {
            _dbContext = dbContext;

            // Ensure the database is created
            _dbContext.Database.EnsureCreated();
        }

        public IEnumerable<Feedback> GetAllFeedback()
        {
            return _dbContext.Feedbacks.ToList();
        }

        public void AddFeedback(Feedback feedback)
        {
            try
            {
                // Check for empty comment
                if (string.IsNullOrWhiteSpace(feedback.Comment))
                {
                    // Throw an exception if the comment is empty
                    throw new ArgumentException("Comment cannot be empty or whitespace.", nameof(feedback.Comment));
                }

                // Rest of your logic
                _dbContext.Feedbacks.Add(feedback);
                _dbContext.SaveChanges();
            }
            catch (ArgumentException ex)
            {
                // Handle the exception (log it, display a message to the user, etc.)
                Console.WriteLine("Please provide a valid comment.");
                // You can also throw the exception again if needed
                // throw;
            }
        }

    }
}





