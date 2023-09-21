using JamOrder.Models;

namespace JamOrder.Services.Interfaces
{
    public interface ITokenService
    {
        Token GenerateToken(string username);
        bool ValidateToken(string token);
        void RemoveToken(string token);
    }
}
