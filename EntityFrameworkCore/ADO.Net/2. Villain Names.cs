using System;
using Microsoft.Data.SqlClient;

namespace _2._Villain_Names
{
    class Program
    {
        const string sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=MinionsDB";

        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();

                string query = @"  SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                        FROM Villains AS v 
                                       JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                                       GROUP BY v.Id, v.Name 
                                             HAVING COUNT(mv.VillainId) > 3 
                                            ORDER BY COUNT(mv.VillainId)";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var name = reader[0];
                            var count = reader[1];

                            Console.WriteLine($"{name} - {count}");
                        }
                    }
                }
            }
        }
    }
}
