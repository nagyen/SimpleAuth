using System;

namespace SimpleAuth
{
    public class Program
    {
        public string CurrentUser { get; set; }

        public static void Main(string[] args)
        {
            var app = new Program();
            app.Register();
            app.Login();
            app.Logout();
            Console.Read();
        }

        public void Register()
        {
            Console.WriteLine("**Register**");
            Console.Write("Enter your username: ");
            var username = Console.ReadLine();
            string pass = "";
            Console.Write("Enter your password: ");
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);

            var user = new Auth
            {
                Username = username,
                Password = pass
            };

            using (var auth = new AuthService())
            {
                auth.Register(user);
            }

            Console.WriteLine();
            Console.WriteLine("Registered : " + username);
        }

        public void Login()
        {
            Console.WriteLine("**Login**");
            Console.Write("Enter your username: ");
            var username = Console.ReadLine();
            string pass = "";
            Console.Write("Enter your password: ");
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }

            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);

            var user = new Auth
            {
                Username = username,
                Password = pass
            };

            using (var auth = new AuthService())
            {
                var valid = auth.Login(user);
                Console.WriteLine();
                if (valid)
                {
                    CurrentUser = user.Username;
                    Console.WriteLine("Login success");
                }
                else
                {
                    Console.WriteLine("Invalid Username/Password");
                    Login();
                }
            }

            Console.WriteLine();
        }

        public void Logout()
        {
            if(string.IsNullOrEmpty(CurrentUser)) return;
            using (var auth = new AuthService())
            {
                auth.Logout(CurrentUser);
                Console.WriteLine($"Logged out {CurrentUser}");
            }
        }
    }
}