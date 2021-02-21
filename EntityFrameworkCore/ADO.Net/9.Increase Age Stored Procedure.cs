using Microsoft.Data.SqlClient;
using System;

namespace _9.Increase_Age_Stored_Procedure
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

                string query = @"EXEC usp_GetOlder @id";

                using var command = new SqlCommand(query,connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                string selectQuery = "SELECT Name, Age FROM Minions WHERE Id = @Id";
                using var select = new SqlCommand(selectQuery, connection);
                select.Parameters.AddWithValue("@id", id);
                using var reader = select.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]} years old");
                }

            }
        }
    }
}
