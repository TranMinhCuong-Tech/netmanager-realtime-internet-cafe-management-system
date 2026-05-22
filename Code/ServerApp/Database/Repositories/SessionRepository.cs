using Microsoft.Data.Sqlite;
using ServerApp.Models;

namespace ServerApp.Database.Repositories
{
    public class SessionRepository
    {
        private DatabaseManager databaseManager;

        public SessionRepository()
        {
            databaseManager = new DatabaseManager();
        }

        public void StartSession(Session session)
        {
            using (SqliteConnection connection =
                databaseManager.GetConnection())
            {
                connection.Open();

                string query = @"
                INSERT INTO Sessions
                (
                    UserId,
                    MachineId,
                    StartTime,
                    IsActive
                )
                VALUES
                (
                    @UserId,
                    @MachineId,
                    @StartTime,
                    1
                )
                ";

                SqliteCommand command =
                    new SqliteCommand(
                        query,
                        connection);

                command.Parameters.AddWithValue(
                    "@UserId",
                    session.UserId);

                command.Parameters.AddWithValue(
                    "@MachineId",
                    session.MachineId);

                command.Parameters.AddWithValue(
                    "@StartTime",
                    session.StartTime);

                command.ExecuteNonQuery();
            }
        }

        public List<Session> GetAllSessions()
        {
            List<Session> sessions = new List<Session>();
            using (SqliteConnection connection = databaseManager.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM Sessions";

                SqliteCommand command = new SqliteCommand( query, connection);
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Session session =
                        new Session();

                    session.Id =
                        reader.GetInt32(0);

                    session.UserId =
                        reader.GetInt32(1);

                    session.MachineId =
                        reader.GetInt32(2);

                    session.StartTime =
                        reader.GetString(3);

                    sessions.Add(session);
                }
            }

            return sessions;
        }
    }
}