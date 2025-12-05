using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRunnerX.User
{
    public class AuthMenu
    {
        private readonly UserService _userService;
        public AuthMenu(UserService userService)
        {
            _userService = userService;
        }
        public User Show()
        {
            while (true)
            {
                Console.Clear();

                AnsiConsole.MarkupLine("[bold green]=== Console Runner X ===[/]");
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Välj ett alternativ:")
                        .AddChoices(new[] { "Login", "Register", "Exit" })
                );
                if (choice == "Login")
                {
                    var user = Login();
                    if (user != null)
                        return user;
                }
                else if (choice == "Register")
                {
                    Register();
                }
                else if (choice == "Exit")
                {
                    Environment.Exit(0);
                }
            }
        }
        private User? Login()
        {
            var username = AnsiConsole.Ask<string>("Username:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Password:")
                    .PromptStyle("red")
                    .Secret()
            );
            var user = _userService.Login(username, password);
            if (user == null)
            {
                AnsiConsole.MarkupLine("[red]Invalid login![/]");
                AnsiConsole.MarkupLine("Press Enter to continue");
                Console.ReadKey();
            }
            return user;
        }
        private void Register()
        {
            var username = AnsiConsole.Ask<string>("Choose username:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Choose password:")
                    .PromptStyle("red")
                    .Secret()
            );
            if (_userService.Register(username, password))
            {
                AnsiConsole.MarkupLine("[green]User registered successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Username already exists![/]");
            }
            AnsiConsole.Prompt(new TextPrompt<string>("Press [green]Enter[/] to continue").AllowEmpty());
        }
    }
}
