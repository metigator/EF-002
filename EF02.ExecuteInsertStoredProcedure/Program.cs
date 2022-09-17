using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace EF02.ExecuteInsertStoredProcedure
{
    class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            // read from user input
            var walletToInsert = new Wallet
            {
                Holder = "Abeer",
                Balance = 7500
            };

            SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);
         

            SqlParameter holderParameter = new SqlParameter
            {
                ParameterName = "@Holder",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = walletToInsert.Holder,
            };

            SqlParameter balanceParameter = new SqlParameter
            {
                ParameterName = "@Balance",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = walletToInsert.Balance,
            };


            SqlCommand command = new SqlCommand("AddWallet", conn);

            command.Parameters.Add(holderParameter);
            command.Parameters.Add(balanceParameter);

            command.CommandType = CommandType.StoredProcedure;

            conn.Open();

            if (command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"wallet for {walletToInsert.Holder} added successully");
            }
            else
            {
                Console.WriteLine($"ERROR: wallet for {walletToInsert.Holder} was not added");
            }

            conn.Close();

            Console.ReadKey();
        }
    }
}