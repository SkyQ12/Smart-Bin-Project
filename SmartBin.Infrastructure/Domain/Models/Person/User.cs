
namespace SmartBin.Infrastructure.Domain.Models.Person
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Point {  get; set; }
        public string IdentificationNumber { get; set; }
        public string Sex {  get; set; }
        public DateTime Birthday { get; set; }
        public string HomeTown { get; set; }
        public DateTime IssuanceDate { get; set; }
        public string Role { get; set; }
        public List<LoginHistory> LoginHistories { get; set; }
        public List<PointChangedHistory> PointChangedHistories { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public User(string id, string name, string userName, string password, int point, string identificationNumber, string sex, DateTime birthday, string homeTown, DateTime issuanceDate, string role, List<LoginHistory> loginHistories, List<PointChangedHistory> pointChangedHistories)
        {
            Id = id;
            Name = name;
            UserName = userName;
            Password = password;
            Point = point;
            IdentificationNumber = identificationNumber;
            Sex = sex;
            Birthday = birthday;
            HomeTown = homeTown;
            IssuanceDate = issuanceDate;
            Role = role;
            LoginHistories = loginHistories;
            PointChangedHistories = pointChangedHistories;
        }
    }
}
