using Microsoft.Data.Sqlite;
using ServerApp.Models;

namespace ServerApp.Database.Repositories
{
    public class UserRepository
    {
        private DatabaseManager databaseManager;

        public UserRepository()
        {
            databaseManager =
                new DatabaseManager();
        }

        public void AddUser(User user)
        {
            using (SqliteConnection connection = databaseManager.GetConnection())
            {
                connection.Open();

                string query = @"
                INSERT INTO Users
                (
                    Username,
                    Password,
                    Role
                )
                VALUES
                (
                    @Username,
                    @Password,
                    @Role
                )
                ";

                SqliteCommand command =
                    new SqliteCommand(
                        query,
                        connection);

                command.Parameters.AddWithValue(
                    "@Username",
                    user.Username);

                command.Parameters.AddWithValue(
                    "@Password",
                    user.Password);

                command.Parameters.AddWithValue(
                    "@Role",
                    user.Role);

                command.ExecuteNonQuery();
            }
        }

        public bool UserExists(string username)
        {
            using (SqliteConnection connection = databaseManager.GetConnection())
            {
                connection.Open();

                string query = @"
                SELECT 1
                FROM Users
                WHERE Username = @Username
                LIMIT 1
                ";

                SqliteCommand command =
                    new SqliteCommand(
                        query,
                        connection);

                command.Parameters.AddWithValue(
                    "@Username",
                    username);

                object? result = command.ExecuteScalar();
                return result != null;
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users =
                new List<User>();

            using (SqliteConnection connection =
                databaseManager.GetConnection())
            {
                connection.Open();

                string query =
                    "SELECT * FROM Users";

                SqliteCommand command =
                    new SqliteCommand(
                        query,
                        connection);

                SqliteDataReader reader =
                    command.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User();

                    user.Id =
                        reader.GetInt32(0);

                    user.Username =
                        reader.GetString(1);

                    user.Password =
                        reader.GetString(2);

                    user.Role =
                        reader.GetString(3);

                    user.IsLocked =
                        reader.GetInt32(4) == 1;

                    users.Add(user);
                }
            }

            return users;
        }
    }
}