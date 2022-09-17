using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace EF02.ExecuteDeleteRawSql
{
    class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        

            SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);
            
            var sql = "DELETE FROM Wallets " +
                $"WHERE Id = @Id";

            SqlParameter idParameter = new SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = 1,
            };
         

            SqlCommand command = new SqlCommand(sql, conn);

            command.Parameters.Add(idParameter);

            command.CommandType = CommandType.Text;

            conn.Open();

            if (command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"wallet deleted successully");
            }
            

            conn.Close();

            Console.ReadKey();
        }
    }
}