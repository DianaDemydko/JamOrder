using JamOrder.Models;
using JamOrder.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace JamOrder.Services
{
    public class TokenService : ITokenService
    {
        private readonly List<Token> _tokens = new();
        private const string SecretKey = "jam-order-secret-key";

        public Token GenerateToken(string username)
        {
            var token = $"{username}:{DateTime.UtcNow.Ticks}";
            var secretKeyBytes = Encoding.ASCII.GetBytes(SecretKey);

            using var hmac = new HMACSHA256(secretKeyBytes);
            var computedSignatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));
            var signature = Convert.ToBase64String(computedSignatureBytes);

            var customToken = new Token { TokenString = $"{token}:{signature}", ExpirationTime = DateTime.UtcNow.AddMinutes(5) };

            _tokens.Add(customToken);

            return customToken;
        }

        public void RemoveToken(string token)
        {
            var tokenToRemove = _tokens.Find(x => x.TokenString == token);
            if (tokenToRemove is not null)
            {
                _tokens.Remove(tokenToRemove);
            } 
        }

        public bool ValidateToken(string token)
        {
            if (!_tokens.Select(x => x.TokenString).Contains(token))
            {
                return false;
            }

            var expirationTime = _tokens?.Find(x => x.TokenString == token)?.ExpirationTime;
            return DateTime.UtcNow < expirationTime;
        }
    }
}
