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

    }
}
