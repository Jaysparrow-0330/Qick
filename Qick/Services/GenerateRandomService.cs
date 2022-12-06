using Qick.Services.Interfaces;

namespace Qick.Services
{
    public class GenerateRandomService : IGenerateRandomService
    {
        public string GenerateRandomNumber(int size)
        {
            Random random = new();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, size)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GenerateRandomString(int size)
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, size)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
