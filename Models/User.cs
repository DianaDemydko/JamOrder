using System.Text.Json.Serialization;

namespace JamOrder.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public Token? Token { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
    }
}
