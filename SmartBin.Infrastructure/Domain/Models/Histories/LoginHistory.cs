
namespace SmartBin.Infrastructure.Domain.Models.Histories
{
    public class LoginHistory
    {
        public int Id {  get; set; }
        public string UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public User User { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public LoginHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public LoginHistory(int id, string userId, DateTime loginTime, User user)
        {
            Id = id;
            UserId = userId;
            LoginTime = loginTime;
            User = user;
        }
    }
}
