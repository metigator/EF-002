using Microsoft.Extensions.Configuration;
using System;

namespace EF02.ExecuteRawSql
{
    class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();


            Console.WriteLine(configuration.GetSection("constr").Value);

            Console.ReadKey();
        }
    }
}