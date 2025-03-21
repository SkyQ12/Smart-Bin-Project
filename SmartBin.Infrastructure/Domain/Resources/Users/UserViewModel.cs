
namespace SmartBin.Infrastructure.Domain.Resources.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Point { get; set; }
        public string IdentificationNumber { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string HomeTown { get; set; }
        public DateTime IssuanceDate { get; set; }

        public UserViewModel(string id, string name, string userName, string password, int point, string identificationNumber, string sex, DateTime birthday, string homeTown, DateTime issuanceDate)
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
        }
    }
}
