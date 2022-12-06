namespace Qick.Services.Interfaces
{
    public interface IGenerateRandomService
    {
        string GenerateRandomNumber(int size);

        string GenerateRandomString(int size);
    }
}
