
namespace SmartBin.Infrastructure.Domain.Resources.Users
{
    public class UpdateUserInfoViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string IdentificationNumber { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string HomeTown { get; set; }
        public DateTime IssuanceDate { get; set; }
    }
}
