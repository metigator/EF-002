using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace EF02.ExecuteTransaction
{
    class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        

            SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);
            
       

            SqlCommand command = conn.CreateCommand();

            command.CommandType = CommandType.Text;

            conn.Open();

            SqlTransaction transaction = conn.BeginTransaction();

            command.Transaction = transaction;
            
            try
            {
                command.CommandText = "UPDATE Wallets Set Balance = Balance - 1000 Where Id = 2";
                command.ExecuteNonQuery();


                command.CommandText = "UPDATE Wallets Set Balance = Balance + 1000 Where Id = 3";
                command.ExecuteNonQuery();

                transaction.Commit();

                Console.WriteLine("Transaction of transfer completed successfully");

            }
            catch
            {
                try
                {
                    transaction.Rollback();
                }
                catch
                {
                    // log errors
                }
            }
            finally
            {

                try
                {
                    conn.Close();
                }
                catch
                {
                    // log errors

                }
            }
            

           

            Console.ReadKey();
        }
    }
}