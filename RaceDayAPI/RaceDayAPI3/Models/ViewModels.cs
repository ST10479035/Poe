namespace RaceDayAPI3.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class LoginViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public double Distance { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string BannerImageUrl { get; set; } = string.Empty;
        public int TotalEnrolments { get; set; }
        // Backward-compatible aliases for legacy views
        public string Name { get => Title; set => Title = value; }
        public DateTime Date { get => EventDate; set => EventDate = value; }
        public string Type { get => CategoryName; set => CategoryName = value; }
        public string EventBannerUrl { get => BannerImageUrl; set => BannerImageUrl = value; }
    }

    public class EnrolmentViewModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;
        public string? EventBannerUrl { get; set; }
        public string? Location { get; set; }
        public DateTime EventDate { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public string? FinishTime { get; set; }
        public string? FinishingPosition { get; set; }

        // Backward-compatible alias
        public string EventName { get => EventTitle; set => EventTitle = value; }
    }

    public class ResultViewModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string FinishTime { get; set; } = string.Empty;
        public int Position { get; set; }
        public int TotalParticipants { get; set; }

        // Backward-compatible alias
        public string EventName { get => EventTitle; set => EventTitle = value; }
    }

    public class UserProfileViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ProfilePictureUrl { get; set; } = string.Empty;
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
