using ConsoleRunnerX.User;
using MyUser = ConsoleRunnerX.User.User;


namespace ConsoleRunnerX
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userService = new UserService();
            var authMenu = new AuthMenu(userService);

            
            MyUser loggedInUser = authMenu.Show();

            // Efter login startar du spelet
            //var game = new RunnerGame(loggedInUser, userService);
            //game.Start();
        }
    }
}
