using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRunnerX.Models
{
    public class GameState
    {
        public Player Player { get; set; } = new Player(); // The player instance
        public List<Obstacle> Obstacles { get; set; } = new List<Obstacle>(); // List of current obstacles
        public int Score { get; set; } = 0;
        public bool IsRunning { get; set; } = true; 
        public int Speed { get; set; } = 1; //speed of the game
        public int MaxLanes { get; } = 3; // Number of lanes in the game
        public GameState()
        { 

        }
    }
}
