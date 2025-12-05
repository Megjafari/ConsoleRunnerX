using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRunnerX.Models
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int HighScore { get; set; }
    }
}
