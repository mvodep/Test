using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class Program
    {
        /*
         CREATE TABLE IF NOT EXISTS test
         (
           myDate TIMESTAMP WITHOUT TIME ZONE
         );
         INSERT INTO test (myDate) VALUES(CURRENT_TIMESTAMP);
        */

        static void Main(string[] args)
        {
            var connectionString = "Host=kandula.db.elephantsql.com;Username=zotydhpl;Password=XX;Database=zotydhpl";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT myDate FROM test", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(((DateTime)reader[0]).Millisecond);
                        }
                    }

                    var result = connection.QuerySingle<DateTime>("SELECT myDate FROM test");
                    
                    Console.WriteLine(result.Millisecond);

                    var dynamicResult = connection.Query("SELECT * FROM test");
                    var resultAsList = dynamicResult.Cast<IDictionary<string, object>>();

                    Console.WriteLine(((DateTime)resultAsList.Single().Values.Single()).Millisecond);
                }
            }

            Console.ReadKey();
        }
    }
}
