using ConsoleRunnerX.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRunnerX.Engine
{
    public class Renderer
    {
        private const int GameWidth = 40; // Width of the game area
        private const int GameHeight = 25; // Height of the game area   
        private const int LaneWidth = 10; // Width of each lane
        private const char LaneSeparator = '|'; // Character to separate lanes

        public static void DrawGame(GameState state, User loggedInUser)
        {
            AnsiConsole.Clear();
            // Draw top border
            AnsiConsole.MarkupLine($"[yellow bold]SCORE:[/][yellow] {state.Score}[/] | [grey]BEST:[/][grey] {loggedInUser.HighScore}[/] | [green]PLAYER:[/][green] {loggedInUser.Username}[/]");
            AnsiConsole.MarkupLine($"[grey]Speed: {state.Speed}[/]");

            var canvas = new Panel(CreateGrid(state))
                .Header("[bold white]CONSOLE RUNNER X[/]")
                .BorderColor(Color.Grey);

            AnsiConsole.Write(canvas);
        }
        private static string CreateGrid(GameState state)
        {
            var grid = new System.Text.StringBuilder(); // Using StringBuilder for efficient string concatenation
            for (int r = GameHeight - 1; r >= 0; r--)
            {
                var rowContent = new System.Text.StringBuilder();
                for (int l = 1; l <= state.MaxLanes; l++)
                {
                    if (l > 1)
                    {
                        rowContent.Append($"[grey]{LaneSeparator}[/]"); // Add lane separator
                    }
                    string content = new string(' ', LaneWidth); // Default empty space
                    string elementMarkup = null;

                    if (r == state.Player.Row && l == state.Player.Lane)
                    {
                        int playerPos = LaneWidth / 2;
                        elementMarkup = $"[{state.Player.Color}]{state.Player.Symbol}[/]";
                        content = content.Remove(playerPos, 1).Insert(playerPos, elementMarkup);
                    }
                    var obstacle = state.Obstacles.FirstOrDefault(o => o.Row == r && o.Lane == l);
                    if (obstacle != null)
                    {
                        int obstaclePos = LaneWidth / 2;
                        elementMarkup = $"[{obstacle.Color}]{obstacle.Symbol}[/]";

                        if (r != state.Player.Row || l != state.Player.Lane)
                        {
                            content = content.Remove(obstaclePos, 1).Insert(obstaclePos, elementMarkup);
                        }
                    }
                    rowContent.Append(content);
                }
                grid.AppendLine(rowContent.ToString());
            }
            return grid.ToString();
        }
    }
}
