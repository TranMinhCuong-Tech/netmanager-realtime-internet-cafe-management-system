using Microsoft.Data.Sqlite;
using ServerApp.Models;

namespace ServerApp.Database.Repositories
{
    public class MachineRepository
    {
        private DatabaseManager databaseManager;

        public MachineRepository()
        {
            databaseManager = new DatabaseManager();
        }

        public void AddMachine(Machine machine)
        {
            using (SqliteConnection connection = databaseManager.GetConnection())
            {
                connection.Open();

                string query = @"
                INSERT INTO Machines
                (
                    MachineName,
                    IpAddress,
                    Status
                )
                VALUES
                (
                    @MachineName,
                    @IpAddress,
                    @Status
                )
                ";

                SqliteCommand command =
                    new SqliteCommand(
                        query,
                        connection);

                command.Parameters.AddWithValue(
                    "@MachineName",
                    machine.MachineName);

                command.Parameters.AddWithValue(
                    "@IpAddress",
                    machine.IpAddress);

                command.Parameters.AddWithValue(
                    "@Status",
                    machine.Status);

                command.ExecuteNonQuery();
            }
        }

        public List<Machine> GetAllMachines()
        {
            List<Machine> machines = new List<Machine>();

            using (SqliteConnection connection = databaseManager.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Machines";

                SqliteCommand command = new SqliteCommand( query, connection);

                SqliteDataReader reader =
                    command.ExecuteReader();

                while (reader.Read())
                {
                    Machine machine =
                        new Machine();

                    machine.Id =
                        reader.GetInt32(0);

                    machine.MachineName =
                        reader.GetString(1);

                    machine.IpAddress =
                        reader.GetString(2);

                    machine.Status =
                        reader.GetString(3);

                    machines.Add(machine);
                }
            }

            return machines;
        }
    }
}