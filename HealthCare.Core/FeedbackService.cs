using HealthCare.Core.Models;
using System;


namespace HealthCare.Core
{
    public class FeedbackService
    {
        private List<Feedback> feedbackList = new List<Feedback>();

        public FeedbackService()
        {
            LoadDummyData();
        }

        private void LoadDummyData()
        {
            // mock data
            feedbackList.Add(new Feedback { Id = Guid.NewGuid(), Title = "Great Service", Comment = "Great service, thank you!", Time = DateTime.Now });
            feedbackList.Add(new Feedback { Id = Guid.NewGuid(), Title = "Satisfaction", Comment = "Very satisfied with the care provided.", Time = DateTime.Now });
        }

        public void SaveFeedback(Feedback feedback)
        {
            feedbackList.Add(feedback);
        }

        public IEnumerable<Feedback> GetAllFeedback()
        {
            return feedbackList;
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




