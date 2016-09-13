﻿namespace SerialNumber.Service.App
{
    using System.IO;

    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseUrls("http://+:8888")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}