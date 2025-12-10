using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleRunnerX.Models;

namespace ConsoleRunnerX.Services
{
    public class UserService
    {
        private const string FilePath = "users.json";
        private List<User> _users = new();

        public UserService()
        {
            LoadUsers();
        }
        public bool Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username))
            {
                return false; // Username already exists
            }
            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
            };
            _users.Add(user);
            SaveUsers();
            return true;
        }
        public User? Login(string username, string password)
        {
            var hash = HashPassword(password);  
            return _users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hash);
        }
        private void LoadUsers()
        {
            if (!File.Exists(FilePath))
            {
                _users = new List<User>();
                SaveUsers();
                return;
            }
            var json = File.ReadAllText(FilePath);
            // KORRIGERING: Kontrollera om JSON-strängen är tom/null/mellanslag
            if (string.IsNullOrWhiteSpace(json))
            {
                _users = new List<User>();
                return; // Gå ut och behåll tom lista
            }
            _users = JsonSerializer.Deserialize<List<User>>(json)!;
        }
        private void SaveUsers()
        {
            var json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
        private string HashPassword(string password)
        {
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }
        public static List<User> LoadAllUsers()
        {
            const string FilePath = "users.json";

            if (!File.Exists(FilePath))
            {
                return new List<User>();
            }

            try
            {
                var json = File.ReadAllText(FilePath);

                // Returnera en tom lista om filen är tom eller ogiltig JSON
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<User>();
                }

                // Deserialisera direkt. Vi använder System.Text.Json.JsonSerializer.
                var allUsers = System.Text.Json.JsonSerializer.Deserialize<List<User>>(json);

                return allUsers ?? new List<User>();
            }
            catch
            {
                // Returnera en tom lista vid fel (t.ex. korrupt fil)
                return new List<User>();
            }
        }
        public void UpdateUserHighScore(User updateUser)
        {
            var existingUser = _users.FirstOrDefault(u => u.Username == updateUser.Username);
            if (existingUser != null)
            {
                existingUser.HighScore = updateUser.HighScore;
                SaveUsers();
            }
        }
    }
}
