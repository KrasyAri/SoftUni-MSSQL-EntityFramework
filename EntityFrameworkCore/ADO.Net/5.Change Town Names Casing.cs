using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _5.Change_Town_Names_Casing
{
    class Program
    {

        const string sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=MinionsDB";

        static void Main(string[] args)
        {

            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();

                string countryName = Console.ReadLine();

                string updateTownNameQuery = @"UPDATE Towns
                                     SET Name = UPPER(Name)
                            WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

                string selectTownNameQuery = @"SELECT t.Name 
                                FROM Towns as t
                            JOIN Countries AS c ON c.Id = t.CountryCode
                        WHERE c.Name = @countryName";

                using var updateCommand = new SqlCommand(updateTownNameQuery, connection);
                updateCommand.Parameters.AddWithValue("@countryName", countryName);
                var affectedRows = updateCommand.ExecuteNonQuery();

                if (affectedRows == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    Console.WriteLine($"{affectedRows} town names were affected.");

                    using var selectCommand = new SqlCommand(selectTownNameQuery, connection);
                    selectCommand.Parameters.AddWithValue("@countryName", countryName);

                    using (var readed = selectCommand.ExecuteReader())
                    {
                        var towns = new List<string>();
                        while (readed.Read())
                        {
                            towns.Add((string)readed[0]);
                        }

                        Console.WriteLine($"[{string.Join(", ", towns)}]");
                    }
                }
            }

        }
    }
}
