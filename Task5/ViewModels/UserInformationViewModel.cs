using Task5.Models;

namespace Task5.ViewModels
{
    public class UserInformationViewModel
    {
        public int ErrorValue { get; set; }
        public int Locale { get; set; }
        public int Seed { get; set; }
        public int Page { get; set; }
        public List<UserInformation> UsersInformation { get; set; } = null!;
    }
}
