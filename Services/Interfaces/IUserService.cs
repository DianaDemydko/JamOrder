using JamOrder.Models;

namespace JamOrder.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(User user);
        User? GetUser(UserCredentials userCredentials);
    }
}
