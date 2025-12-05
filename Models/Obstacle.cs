using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRunnerX.Models
{
    public class Obstacle
    {
        public int Lane { get; set; } // Lane position (1, 2, or 3)
        public int Row { get; set; } // Row position
        public char Symbol { get; } = '#'; // Representation of the obstacle
        public string Color { get; } = "red"; // Color of the obstacle
        public Obstacle(int lane, int row)
        {
            Lane = lane;
            Row = row;
        }
    }
}
