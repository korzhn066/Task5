using Bogus;
using Task5.Interfaces;
using Task5.Models;

namespace Task5.Services
{
    public class DataService : IDataService
    {
        private readonly IDataErrorService _dataErrorService;
        public DataService(IDataErrorService dataErrorService)
        {
            _dataErrorService = dataErrorService;
        }

        public List<UserInformation> GenerateUsersInformation(int count, float errorProbability, string locale = "ru", int seed = 0)
        {
            var usersInformation = new UserInformationFaker(locale).UseSeed(seed).Generate(count);

            var faker = new Faker(locale: locale);
            faker.Random = new Randomizer(1);

            return ApplyErrors(usersInformation, errorProbability, faker);
        }

        private List<UserInformation> ApplyErrors(List<UserInformation> data, float errorProbability, Faker faker)
        {
            foreach (var userInformation in data)
            {
                for (int i = 0; i < (int)errorProbability; i++)
                {
                    userInformation.PhoneNumber = _dataErrorService.GenerateError(userInformation.PhoneNumber, faker);
                    userInformation.FullName = _dataErrorService.GenerateError(userInformation.FullName, faker);
                    userInformation.Address = _dataErrorService.GenerateError(userInformation.Address, faker);
                }

                userInformation.PhoneNumber = _dataErrorService.GenerateErrorWithProbability(userInformation.PhoneNumber, 
                    errorProbability - (int)errorProbability,
                    faker);
                userInformation.FullName = _dataErrorService.GenerateErrorWithProbability(userInformation.FullName,
                    errorProbability - (int)errorProbability,
                    faker);
                userInformation.Address = _dataErrorService.GenerateErrorWithProbability(userInformation.Address,
                    errorProbability - (int)errorProbability,
                    faker);
            }

            return data;
        }
    }
}
