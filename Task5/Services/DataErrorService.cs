using Bogus;
using System;
using System.Data.Common;
using Task5.Enums;
using Task5.Interfaces;

namespace Task5.Services
{
    public class DataErrorService : IDataErrorService
    {
        public string GenerateError(string data, Faker faker)
        {
            if (data.Length <= 5)
            {
                return AddSymbol(faker, data);
            }

            if (data.Length >= 30)
            {
                return RemoveSymbol(faker, data);
            }

            var mode = (ModeEnum)faker.Random.Int(0, 2);

            return mode switch
            {
                ModeEnum.AddSymbol => AddSymbol(faker, data),
                ModeEnum.RemoveSymbol => RemoveSymbol(faker, data),
                ModeEnum.SwapSymbol => SwapSymbol(faker, data),
                _ => throw new NotImplementedException()
            };
        }

        public string GenerateErrorWithProbability(string data, float probability, Faker faker)
        {
            var result = faker.Random.Int(0, (int)(probability * 100));

            if (result < (int)(probability * 100))
            {
                return GenerateError(data, faker);
            }
            else
            {
                return data;
            }
        }

        private string RemoveSymbol(Faker faker, string data)
        {
            var position = faker.Random.Int(0, data.Length - 1);

            return data.Remove(position, 1);
        }

        private string AddSymbol(Faker faker, string data) 
        {
            var position = faker.Random.Int(0, data.Length - 1);
            var symbol = GetRandomChar(faker);

            return data.Insert(position, symbol.ToString());
        }

        private string SwapSymbol(Faker faker, string data) 
        {
            var position = faker.Random.Int(0, data.Length - 2);

            var dataArray = data.ToCharArray();
            
            var temp = dataArray[position];
            dataArray[position] = dataArray[position + 1];
            dataArray[position + 1] = temp;

            return new string(dataArray);
        }

        private char GetRandomChar(Faker faker)
        {
            var mode = (CharacterEnum)faker.Random.Int(0, 2);

            return mode switch
            {
                CharacterEnum.UpperCase => faker.Name.FirstName()[0],
                CharacterEnum.LowerCase => faker.Name.FirstName()[1],
                CharacterEnum.Digit => faker.Random.Digits(1)[0].ToString()[0],
                _ => throw new NotImplementedException(),
            };
        }
    }
}
