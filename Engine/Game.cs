using ConsoleRunnerX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRunnerX.Services;
using ConsoleRunnerX.Menus;
using Spectre.Console;

namespace ConsoleRunnerX.Engine
{
    public static class Game
    {
        private const int GameSpeedMs = 150;
        public static void Run(User loggedInUser)
        {
            var gameState = new GameState();
            bool gameRunning = true;

            while (gameRunning)
            {
                HandleInput(gameState.Player, gameState.MaxLanes);
                UpdateGame(gameState);

                if (CheckCollision(gameState))
                {
                    gameRunning = false;
                    break;
                }

                Renderer.DrawGame(gameState, loggedInUser);
                Thread.Sleep(GameSpeedMs);
                gameState.Score++;
            }
            HandleGameOver(gameState, loggedInUser);
        }
        private static void HandleInput(Player player, int maxLanes)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        if (player.Lane > 1)
                        {
                            player.Lane--;
                        }
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        if (player.Lane < maxLanes)
                        {
                            player.Lane++;
                        }
                        break;
                        //maybe w for jump later
                }
            }
        }
        private static void UpdateGame(GameState state)
        {
            foreach (var obstacle in state.Obstacles)
            {
                obstacle.Row += state.Speed;
            }
            state.Obstacles.RemoveAll(o => o.Row > state.Player.Row + 5);

            Spawner.CheckAndSpawnObstacle(state);

            if (state.Score > 0 && state.Score % 100 == 0)
            {
                //state.Speed++;
            }
        }
         private static bool CheckCollision(GameState state)
        {
            return state.Obstacles.Any(o =>
                  o.Lane == state.Player.Lane &&
                  o.Row == state.Player.Row);
        }
        private static void HandleGameOver(GameState state, User loggedInUser)
        {
            if (state.Score > loggedInUser.HighScore)
            {
                loggedInUser.HighScore = state.Score;
                var userService = new UserService();    
                userService.UpdateUserHighScore(loggedInUser);

                AnsiConsole.MarkupLine($"[bold yellow] NEW HIGHSCORE! {state.Score}[/]");   
            }
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[red bold]GAME OVER[/]").RuleStyle(Style.Parse("red")));
            AnsiConsole.MarkupLine($"Your score: [yellow bold]{state.Score}[/]");
            AnsiConsole.MarkupLine($"Best score: [yellow bold]{loggedInUser.HighScore}[/]");
            AnsiConsole.MarkupLine("\nPress any [green]Key[/] to go back to the menu...");
            Console.ReadKey(true);
        }
    }
}
