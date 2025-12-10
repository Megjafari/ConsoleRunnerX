using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRunnerX.Models;

namespace ConsoleRunnerX.Services
{
    public static class Spawner
    {
        private static Random _random = new Random();
        private const int MaxLanes = 3;
        public static void CheckAndSpawnObstacle(GameState state) //Add Obstacle if needed
        {
            int spawnRow = 0; 
            if (state.Obstacles.Any(o => o.Row <= spawnRow + 2)) 
            {
                return;
            }
            int spawnChance = 100 - (state.Speed * 5);
            if (_random.Next(0,100) < (state.Speed * 5 + 5))//spawns more often as the speed raises
            {
                int randomLane = _random.Next(1, MaxLanes + 1);//choose a random lane for the obstacle

                var newObstacle = new Obstacle(randomLane, spawnRow);//make a new obstacle och add it to the gamestate
                state.Obstacles.Add(newObstacle);
            }
        }
    }
}
