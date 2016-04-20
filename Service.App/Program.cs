namespace SerialNumber.Service.App
{
    using System;
    using System.Configuration;

    using Microsoft.Owin.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Loading...");
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            using (WebApp.Start<Startup>(baseUrl))
            {
                Console.WriteLine($"Running on {baseUrl}");

                Console.WriteLine("Press X to close");

                while (true)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.X)
                    {
                        Console.WriteLine("Closing...");
                        break;
                    }
                }
            }
            Console.WriteLine("Closed");
        }
    }
}
