using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _7.Print_All_Minion_Names
{
    class Program
    {

        const string sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=MinionsDB";

        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();

                var selectMinionsQuery = "SELECT Name FROM Minions";
                using var selectMinions = new SqlCommand(selectMinionsQuery, connection);

                var minions = new List<string>();

                using (var reader = selectMinions.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        minions.Add((string)reader[0]);
                    }

                    var counter = 0;
                    for (int i = 0; i < minions.Count/2; i++)
                    {
                        Console.WriteLine(minions[0 + counter]);
                        Console.WriteLine(minions[minions.Count-1 - counter]);
                        counter++; 
                    }

                    if (minions.Count % 2 != 0)
                    {
                        Console.WriteLine(minions[minions.Count/2]);
                    }
                }

            }
        }
    }
}
