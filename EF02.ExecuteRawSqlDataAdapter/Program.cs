using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Reflection.PortableExecutable;

namespace EF02.ExecuteRawSqlDataAdapter
{
    class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);
            
            var sql = "SELECT * FROM WALLETS";
            
            conn.Open();
            
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            conn.Close();

            foreach(DataRow dr in dt.Rows)
            {

                var wallet = new Wallet
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Holder = Convert.ToString(dr["Holder"]),
                    Balance = Convert.ToDecimal(dr["Balance"]),
                };

                Console.WriteLine(wallet);
            }



            Console.ReadKey();
        }
    }
}