using JamOrder.Models;
using JamOrder.Services.Interfaces;

namespace JamOrder.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new();
        public User? GetUser(UserCredentials userCredentials)
        {
            return _users.Find(x => x.Username == userCredentials.Username && x.Password == userCredentials.Password) ?? null;
        }

        public void RegisterUser(User user)
        {
            if(!_users.Contains(user))
            {
                user.Id = _users.Count + 1;
                _users.Add(user);
            }
        }
    }
}
