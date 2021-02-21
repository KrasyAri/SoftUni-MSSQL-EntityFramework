using System;
using Microsoft.Data.SqlClient;

namespace _3._Minion_Names
{
    class Program
    {

        const string sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=MinionsDB";

        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();

                int id = int.Parse(Console.ReadLine());
              

                string villianNameQuery = @"SELECT Name FROM Villains WHERE Id = @Id";
                string minionsQuerry = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";

                using var command = new SqlCommand(villianNameQuery, connection);
                command.Parameters.AddWithValue("@Id", id);

                var result = command.ExecuteScalar();

                if (result == null)
                {
                    Console.WriteLine($"No villain with ID {id} exists in the database.");
                }
                else
                {
                    Console.WriteLine($"Villain: {result}");

                    using (var minionCommand = new SqlCommand(minionsQuerry, connection))
                    {
                        minionCommand.Parameters.AddWithValue("@Id", id);

                        using (var reader = minionCommand.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("(no minions)");
                            }

                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader[0]}. {reader[1]} {reader[2]}");
                            }
                        }
                    }

                }

            }
        }

        private static object ExecuteScalar(SqlConnection connection, string querry)
        {
            using var command = new SqlCommand(querry, connection);
            var result = command.ExecuteScalar();

            return result;
        }
    }
}
