using Microsoft.Data.SqlClient;
using System;

namespace _4.Add_Minion
{
    class Program
    {
        const string sqlConnectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=MinionsDB";

        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();

                string[] input = Console.ReadLine().Split(' ');
                string[] villainInfo = Console.ReadLine().Split(' ');

                string villainName = villainInfo[1];

                string minionName = input[1];
                int age = int.Parse(input[2]);
                string town = input[3];

                int? townId = GetTownId(connection, town);

                if (townId == null)
                {
                    string createTownQuerry = "INSERT INTO Towns (Name) VALUES (@townName)";
                    using var sqlCommand = new SqlCommand(createTownQuerry, connection);
                    sqlCommand.Parameters.AddWithValue("@townName", town);
                    sqlCommand.ExecuteNonQuery();
                    townId = GetTownId(connection, town);

                    Console.WriteLine($"Town {town} was added to the database.");
                }

                int? villainId = GetVillainId(connection, villainName);

                if (villainId == null)
                {
                    string createVillainQuerry = "INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
                    using var sqlCommand = new SqlCommand(createVillainQuerry, connection);
                    sqlCommand.Parameters.AddWithValue("@villainName", villainName);
                    sqlCommand.ExecuteNonQuery();
                    villainId = GetVillainId(connection, villainName);

                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }

                CreateMinion(connection, minionName, age, townId);

                var minionId = GetMinionId(connection, minionName); 

                InsertMinionVillain(connection, villainId, minionId);
                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }
        }

        private static void InsertMinionVillain(SqlConnection connection, int? villainId, int? minionId)
        {
            var insertIntoVillainMinnionQuery = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";
            var sqlCommand = new SqlCommand(insertIntoVillainMinnionQuery, connection);
            sqlCommand.Parameters.AddWithValue("@villainId", villainId);
            sqlCommand.Parameters.AddWithValue("@minionId", minionId);
            sqlCommand.ExecuteNonQuery();
          
        }

        private static int? GetMinionId(SqlConnection connection, string minionName)
        {
            var minionIdQuerry = "SELECT Id FROM Minions WHERE Name = @Name";
            var sqlCommand = new SqlCommand(minionIdQuerry, connection);
            sqlCommand.Parameters.AddWithValue("@Name", minionName);
            var minionId = sqlCommand.ExecuteScalar();

            return (int?)minionId;
        }

        private static void CreateMinion(SqlConnection connection, string minionName, int age, int? townId)
        {
            string createMinion = "INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";
            using var sqlCommand = new SqlCommand(createMinion, connection);
            sqlCommand.Parameters.AddWithValue("@name", minionName);
            sqlCommand.Parameters.AddWithValue("@age", age);
            sqlCommand.Parameters.AddWithValue("@townId", townId);
            sqlCommand.ExecuteNonQuery();
        }

        private static int? GetVillainId(SqlConnection connection, string villainName)
        {
            string villainNameQuery = "SELECT Id FROM Villains WHERE Name = @Name";
            var sqlCommand = new SqlCommand(villainNameQuery, connection);
            sqlCommand.Parameters.AddWithValue("@Name", villainName);
            var villainId = sqlCommand.ExecuteScalar();

            return (int?)villainId;
        }

        private static int? GetTownId(SqlConnection connection, string town)
        {
            string townIdQuerry = "SELECT Id FROM Towns WHERE Name = @townName";
            var sqlCommand = new SqlCommand(townIdQuerry, connection);
            sqlCommand.Parameters.AddWithValue("@townName", town);
            var townId = sqlCommand.ExecuteScalar();

            return (int?)townId;
        }
    }
}
