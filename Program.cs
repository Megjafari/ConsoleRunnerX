using ConsoleRunnerX.Menus; 
using ConsoleRunnerX.Services;
using MyUser = ConsoleRunnerX.Models.User;
using ConsoleRunnerX.Engine;


namespace ConsoleRunnerX
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userService = new UserService();
            var authMenu = new AuthMenu(userService);

            
            while (true)
            {
               
                MyUser? loggedInUser = authMenu.Show(userService);

                if (loggedInUser != null)
                {
                    
                    MainMenu.Show(loggedInUser);
                }

                
            }
        }
    }
}
