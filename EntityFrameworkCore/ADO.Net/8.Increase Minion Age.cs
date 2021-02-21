using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _8.Increase_Minion_Age
{
    class Program
    {
        const string sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=MinionsDB";
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();

                int[] minionIds = Console.ReadLine().Split().Select(int.Parse).ToArray();

                string updateMinionsQuery = @" UPDATE Minions
                                      SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                                   WHERE Id = @Id";

                foreach (var id in minionIds)
                {
                    using var updateMinions = new SqlCommand(updateMinionsQuery, connection);
                    updateMinions.Parameters.AddWithValue("@Id", id);
                    updateMinions.ExecuteNonQuery();
                }

                var selectMinionsQuery = "SELECT Name, Age FROM Minions";
                using var selectMinions = new SqlCommand(selectMinionsQuery, connection);
                using var reader = selectMinions.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} {reader[1]}");
                }

            }
        }
    }
}
