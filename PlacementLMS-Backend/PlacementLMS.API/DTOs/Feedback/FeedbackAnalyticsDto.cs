namespace PlacementLMS.DTOs.Feedback
{
    public class FeedbackAnalyticsDto
    {
        public int TotalFeedbackCount { get; set; }
        public double AverageRating { get; set; }
        public int FiveStarCount { get; set; }
        public int FourStarCount { get; set; }
        public int ThreeStarCount { get; set; }
        public int TwoStarCount { get; set; }
        public int OneStarCount { get; set; }
        public Dictionary<string, int> FeedbackByCategory { get; set; }
        public Dictionary<string, int> FeedbackByType { get; set; }
        public List<MonthlyFeedbackDto> MonthlyTrends { get; set; }
        public List<TopRatedEntityDto> TopRatedStudents { get; set; }
        public List<TopRatedEntityDto> TopRatedCompanies { get; set; }
        public List<TopRatedEntityDto> TopRatedCourses { get; set; }
    }

    public class MonthlyFeedbackDto
    {
        public string Month { get; set; }
        public int Count { get; set; }
        public double AverageRating { get; set; }
    }

    public class TopRatedEntityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AverageRating { get; set; }
        public int FeedbackCount { get; set; }
    }
}