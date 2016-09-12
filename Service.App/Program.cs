namespace SerialNumber.Service.App
{
    using System;
    using System.Configuration;

    using Mono.Unix;
    using Mono.Unix.Native;

    using Nancy.Hosting.Self;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Loading...");

            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];

            var host = new NancyHost(new Bootstrapper(), new Uri(baseUrl));
            host.Start();

            Console.WriteLine($"Running on {baseUrl}");
            Console.WriteLine("Press Ctrl-C to exit");

            // check if we're running on mono

            if (Type.GetType("Mono.Runtime") != null)
            {
                // on mono, processes will usually run as daemons - this allows you to listen
                // for termination signals (ctrl+c, shutdown, etc) and finalize correctly
                UnixSignal.WaitAny(
                    new[]
                        {
                            new UnixSignal(Signum.SIGINT), new UnixSignal(Signum.SIGTERM), new UnixSignal(Signum.SIGQUIT),
                            new UnixSignal(Signum.SIGHUP)
                        });
            }
            else
            {
                Console.ReadLine();
            }

            Console.WriteLine("Closed");
        }
    }
}
