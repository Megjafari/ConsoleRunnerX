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
    public static class HighscoreMenu
    {
        public static void Show()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Rule("[bold yellow]Highscores Top 10[/]")
                    .RuleStyle(Style.Parse("yellow")));

            try
            {
                var allUsers = UserService.LoadAllUsers();
                var table = new Table()
                    .BorderColor(Color.Yellow)
                    .AddColumns(
                            new TableColumn("[bold]Rank[/]").Centered(),
                            new TableColumn("[bold]Username[/]"),
                            new TableColumn("[bold]Highscore[/]").Alignment(Justify.Right));
                var rankedUsers = allUsers
                    .OrderByDescending(u => u.HighScore)
                    .Take(10);

                int rank = 1;
                foreach (var user in rankedUsers)
                {
                    table.AddRow(
                        $"[grey]{rank++}.[/]",
                        user.Username,
                        $"[yellow bold]{user.HighScore}[/]"
                    );
                }
                if (!rankedUsers.Any())
                {
                    table.AddRow("---", "[red]No highscores found![/]", "0");
                }
                AnsiConsole.Write(table);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error loading highscores: {ex.Message}[/]");
            }
            AnsiConsole.MarkupLine("\nPress any [green]Key[/] to go back to the menu...");
            Console.ReadKey(true);
        }
    }
}
