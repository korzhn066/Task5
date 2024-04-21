using Task5.Models;

namespace Task5.Interfaces
{
    public interface IDataService
    {
        List<UserInformation> GenerateUsersInformation(int count, float errorProbability, string locale = "ru", int seed = 0);

        List<UserInformation> GenerateUsersInformationByPage(int page, float errorProbability, string locale = "ru", int seed = 0);
    }
}
