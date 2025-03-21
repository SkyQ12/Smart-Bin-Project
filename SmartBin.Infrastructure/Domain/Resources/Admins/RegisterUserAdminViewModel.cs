
namespace SmartBin.Infrastructure.Domain.Resources.Admins
{
    public class RegisterUserAdminViewModel
    {
        [DataMember]
        [JsonIgnore]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        [JsonIgnore]
        public int Point { get; set; }
        [DataMember]
        [JsonIgnore]
        public string IdentificationNumber { get; set; }
        [DataMember]
        [JsonIgnore]
        public string Sex { get; set; }
        [DataMember]
        [JsonIgnore]
        public DateTime Birthday { get; set; }
        [DataMember]
        [JsonIgnore]
        public string HomeTown { get; set; }
        [DataMember]
        [JsonIgnore]
        public DateTime IssuanceDate { get; set; }
        [DataMember]
        [JsonIgnore]
        public string Role { get; set; }

        public RegisterUserAdminViewModel(string id, string name, string userName, string password, int point, string identificationNumber, string sex, DateTime birthday, string homeTown, DateTime issuanceDate, string role)
        {
            Id = "";
            Name = name;
            UserName = userName;
            Password = password;
            Point = 0;
            IdentificationNumber = "";
            Sex = "";
            Birthday = DateTime.MinValue;
            HomeTown = "";
            IssuanceDate = DateTime.MinValue;
            Role = "UserAdmin";
        }
    }
}
