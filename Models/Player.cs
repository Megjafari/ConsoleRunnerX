using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRunnerX.Models
{
    public class Player
    {
        // Player's position on the lane (1, 2, or 3)
        public int Lane { get; set; } = 2; // Default lane
        // Player's row position. 20 is the bottom of the console.
        public int Row { get; set; } = 20; 
        public char Symbol { get; } = '@'; // Representation of the player
        public string Color { get; } = "green"; // Color of the player
        public Player() { }
    }
}
