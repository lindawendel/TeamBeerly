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
            _dbContext.Feedbacks.Add(feedback);
            _dbContext.SaveChanges();
        }

    }
}

/*    public class FeedbackService
    {
        private List<string> feedbackList = new List<string>();

        public FeedbackService()
        {
            LoadDummyData();
        }

        private void LoadDummyData()
        {
            // mock data
            feedbackList.Add("Great service, thank you!");
            feedbackList.Add("Very satisfied with the care provided.");
        }

        public void SaveFeedback(string feedback)
        {
            feedbackList.Add(feedback);
        }

        public IEnumerable<string> GetAllFeedback()
        {
            return feedbackList;
        }
    }*/




