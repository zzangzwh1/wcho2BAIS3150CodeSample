using System.Data;
using wcho2BAIS3150CodeSample.Models;
using Microsoft.Data.SqlClient;



namespace wcho2BAIS3150CodeSample.TechService
{
    public class Programs
    {
        private static string? _connectionString = @"Persist Security Info=False; Server=dev1.baist.ca; Database=wcho2; ";
        // private string? connectionString = @"Persist Security Info=False; Server=dev1.baist.ca; Database=wcho2; User Id=wcho2; password=Whdnjsgur1!; ";
        //public static string connectionString = @"Persist Security Info=false; Integrated Security= true;  database= DataConnect; server=(localdb)\Local; ";

        public Programs()
        {
            // Constructor Logic
            ConfigurationBuilder databaseUserBuilder = new ConfigurationBuilder();
            databaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            databaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration databaseUsersConfiguration = databaseUserBuilder.Build();
            _connectionString = databaseUsersConfiguration.GetConnectionString("BAIST3150");
            //  _connectionString = databaseUsersConfiguration.GetConnectionString("BAIST3150")!; //null forgiving operator

        }

        #region AddProgram
        public bool AddProgram(string? programCode, string? description)
        {
            if (string.IsNullOrEmpty(programCode))
            {
                programCode = null;
            }
            if (string.IsNullOrEmpty(description))
            {
                description = null;
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("AddProgram", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        command.Parameters.AddWithValue("@ProgramCode", programCode);
                        command.Parameters.AddWithValue("@Description", description);
                        command.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error-Occurreed - {ex.Message}");
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }


                }
            }
            return true;
        }
        #endregion

        #region GetProgram
        public Models.Program GetProgram(string programCode)
        {
            /*TestStudent students = new TestStudent();*/
            Models.Program enrollStudents = new Models.Program();
            Students students = new Students();
            Models.Program activeProgram = new Models.Program();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("GetProgram", conn))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProgramCode", programCode).Size = 10;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {

                                    Console.Write($"{reader.GetName(i)}\t");
                                }
                                Console.WriteLine();

                              
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {

                                        enrollStudents.ProgramCode = reader[0].ToString();
                                        enrollStudents.Description = reader[1].ToString();

                                        Console.Write($"{reader[i]}\t\t");

                                    }

                                }
                                Console.WriteLine();
                                Console.WriteLine();
                                activeProgram = students.GetStudents(programCode);



                            }
                            else
                            {
                                Console.WriteLine("No data Found.");

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Occurred - GetProgram : {ex.Message}");
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
        
            return activeProgram;

        }

        #endregion

        #region GetProgramCode for the DropList

        public List<string> GetProgramCode()
        {
            List<string> values = new List<string>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("GetProgramCode", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {


                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {

                                    values.Add((string)reader[i]);


                                }

                            }


                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Occurred - GetProgram : {ex.Message}");
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }

            return values;
        }

        #endregion
    }
}
