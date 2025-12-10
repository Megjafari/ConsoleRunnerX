using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRunnerX.Models;
using ConsoleRunnerX.Services;
using Spectre.Console;

namespace ConsoleRunnerX.Menus
{
    public static class MainMenu
    {
        public static void Show(User loggedInUser)
        {
            bool running = true;
            while (running)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("ConsoleRunnerX")
                        .LeftJustified()
                        .Color(Color.Green));
                AnsiConsole.MarkupLine($"[grey]Welcome, [/][green bold]{loggedInUser.Username}![/] [grey](Best Score: {loggedInUser.HighScore})[/]\n");

                var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]What would you like to do?[/]")
                        .PageSize(10)
                        .AddChoices(new[] {
                            "Start Game",
                            "Show Highscores",
                            "Logout",
                            "[red]Exit[/]"
                        }));
                switch (selection)
                {
                    case "Start Game":
                        AnsiConsole.Clear();
                        AnsiConsole.MarkupLine("[bold blue] The game starts soon...[/]");
                        AnsiConsole.WriteLine("Implementera Game.cs nästa gång!");
                        AnsiConsole.MarkupLine("\nPress any [green]Key[/] to go back to the menu...");
                        Console.ReadKey(true);
                        // Game.Run(loggedInUser); 
                        break;

                    case "Show Highscores":
                        HighscoreMenu.Show(); // Anropar vår nya Highscore-meny
                        break;

                    case "Logout":
                        running = false;
                        break;

                    case "[red]Exit[/]":
                        Environment.Exit(0);
                        break;
                
                }
            }
        }
    }
}
