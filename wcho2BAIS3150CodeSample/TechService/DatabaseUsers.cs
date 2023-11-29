using wcho2BAIS3150CodeSample.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace wcho2BAIS3150CodeSample.TechService
{
    public class DatabaseUsers
    {
        private string? _connectionString; //nullable reference type

        // private string _connectionString;

        public DatabaseUsers()
        {
            // Constructor Logic
            ConfigurationBuilder databaseUserBuilder = new ConfigurationBuilder();
            databaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            databaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration databaseUsersConfiguration = databaseUserBuilder.Build();
            _connectionString = databaseUsersConfiguration.GetConnectionString("BAIST3150");
            //  _connectionString = databaseUsersConfiguration.GetConnectionString("BAIST3150")!; //null forgiving operator




        }

        public DatabaseUser GetDatabaseUser()
        {
            DatabaseUser currentDBUser = new DatabaseUser();


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("GetDatabaseUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                reader.Read();
                                currentDBUser.CurrentUser = (string)reader["CurrentUser"];
                                currentDBUser.SystemUser = (string)reader["SystemUser"];
                                currentDBUser.SessionUser = (string)reader["SessionUser"];


                            }

                            reader.Close();

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();

                }

            }
            return currentDBUser;



        }

    }
}
