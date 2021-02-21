using Microsoft.Data.SqlClient;
using System;

namespace _6.Remove_Villain
{
    class Program
    {
        const string sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=MinionsDB";
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();

                int input = int.Parse(Console.ReadLine());

                string evilName = "SELECT Name FROM Villains WHERE Id = @villainId";

                using var selectEvilName = new SqlCommand(evilName, connection);
                selectEvilName.Parameters.AddWithValue("@villainId", input);
                var name = (string)selectEvilName.ExecuteScalar();

                if (name == null)
                {
                    Console.WriteLine("No such villain was found.");
                    return;
                }


                var deleteMinionsVillainsQuery = @"DELETE FROM MinionsVillains 
                             WHERE VillainId = @villainId";

                using var deleteMinionVillain = new SqlCommand(deleteMinionsVillainsQuery, connection);
                deleteMinionVillain.Parameters.AddWithValue("@villainId", input);
                var affectedRows = deleteMinionVillain.ExecuteNonQuery();

                var deleteVillainsQuery = @"DELETE FROM Villains
                     WHERE Id = @villainId";

                using var deleteVillain = new SqlCommand(deleteVillainsQuery, connection);
                deleteVillain.Parameters.AddWithValue("@villainId", input);
                deleteVillain.ExecuteNonQuery();

                Console.WriteLine($"{name} was deleted.");
                Console.WriteLine($"{affectedRows} minions were released.");
            }
        }
    }
}
