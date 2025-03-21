
namespace SmartBin.Infrastructure.Domain.Resources.Users
{
    [DataContract]
    public class CreateNewUserViewModel
    {
        [DataMember]
        [JsonIgnore]
        public string Id {  get; set; }
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
        public string IdentificationNumber { get; set; } 
        [DataMember]
        public string Sex { get; set; }
        [DataMember]
        public DateTime Birthday { get; set; } 
        [DataMember]
        public string HomeTown { get; set; }
        [DataMember]
        public DateTime IssuanceDate { get; set; } 
        [DataMember]
        [JsonIgnore]
        public string Role {  get; set; }

        public CreateNewUserViewModel(string name, string userName, string password, string identificationNumber, string sex, DateTime birthday, string homeTown, DateTime issuanceDate)
        {
            Id = "";
            Name = name;
            UserName = userName;
            Password = password;
            Point = 0;
            IdentificationNumber = identificationNumber;
            Sex = sex;
            Birthday = birthday;
            HomeTown = homeTown;
            IssuanceDate = issuanceDate;
            Role = "User";
        }
    }
}
