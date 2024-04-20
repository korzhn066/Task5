using Bogus;

namespace Task5.Interfaces
{
    public interface IDataErrorService
    {
        string GenerateError(string data, Faker faker);
        string GenerateErrorWithProbability(string data, float probability, Faker faker);
    }
}
