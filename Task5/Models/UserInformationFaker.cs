using Bogus;

namespace Task5.Models
{
    public class UserInformationFaker : Faker<UserInformation>
    {
        public UserInformationFaker(string locale) : base(locale: locale)
        {
            RuleFor(ui => ui.Number, f => f.IndexFaker);
            RuleFor(ui => ui.Id, f => f.Random.Uuid().ToString());
            RuleFor(ui => ui.FullName, f => f.Name.FirstName() + " " + f.Name.LastName());
            RuleFor(ui => ui.Address, f => f.Address.FullAddress());
            RuleFor(ui => ui.PhoneNumber, f => f.Phone.PhoneNumber());
        }
    }
}
