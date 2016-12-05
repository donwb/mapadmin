using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace NancyApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();

        }
    }
}
