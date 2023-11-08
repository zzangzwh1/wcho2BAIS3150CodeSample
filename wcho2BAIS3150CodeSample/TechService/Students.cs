using Microsoft.Data.SqlClient;
using System.Data;
using wcho2BAIS3150CodeSample.Models;

namespace wcho2BAIS3150CodeSample.TechService
{
    public class Students
    {
          public static string connectionString = @"Persist Security Info=False; Server=dev1.baist.ca; Database=wcho2; User Id=wcho2; password=Whdnjsgur1!; ";
        //public static string connectionString = @"Persist Security Info=false; Integrated Security= true;  database= DataConnect; server=(localdb)\Local; ";

        public string? ProgramCode { set; get; } = "";
      //  public List<string> StudentInfoField = new List<string>(); 

        #region StudentID
        public List<string> GetSutdentID(string query) 
        {

            List<string> studentIDs = new List<string>();
           

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                  
                    try
                    {                       

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                               
                                while (reader.Read())
                                {
                                    //string? programCodes = "";
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        studentIDs.Add((string)reader[i]);

                                    }

                                }
                            }
                            else
                            {
                                Console.WriteLine($"There are No Student exists with that student ID try other Student ID");
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Occurred - {ex.Message}");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return studentIDs;
        }


        #endregion

        #region AddStudent
        public bool AddStudent(Student acceptedStudent, string? programCode)
        {
            if (string.IsNullOrEmpty(acceptedStudent.StudentId))
                acceptedStudent.StudentId = null;
            if (string.IsNullOrEmpty(acceptedStudent.FirstName))
                acceptedStudent.FirstName = null;
            if (string.IsNullOrEmpty(acceptedStudent.lastName))
                acceptedStudent.lastName = null;
            if (string.IsNullOrEmpty(acceptedStudent.Email))
                acceptedStudent.Email = null;
            if (string.IsNullOrEmpty(programCode))
                programCode = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("AddStudent", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        command.Parameters.AddWithValue("@StudentID", acceptedStudent.StudentId).Size = 10;
                        command.Parameters.AddWithValue("@FirstName", acceptedStudent.FirstName).Size = 25;
                        command.Parameters.AddWithValue("@LastName", acceptedStudent.lastName).Size = 25;
                        command.Parameters.AddWithValue("@Email", acceptedStudent.Email).Size = 50;
                        command.Parameters.AddWithValue("@ProgramCode", programCode).Size = 10;

                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error - Occurred - {ex.Message}");
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

        #region GetStudent
        public Student GetStudent(string? studentId)
        {
            if (studentId == string.Empty)
                studentId = null;

            Student student = new Student();


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("GetStudent", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {


                        command.Parameters.AddWithValue("@StudentID", studentId).Size = 10;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                for (int i = 0; i < reader.FieldCount-1; i++)
                                {
                                    //if condition added so result output present equally
                                    if (i == reader.FieldCount - 2)
                                    {
                                        Console.Write($"{reader.GetName(i)}\t\t\t");
                                    }
                                    else
                                    {

                                        Console.Write($"{reader.GetName(i)}\t");
                                    }
                                 //   StudentInfoField.Add((string)reader[i]);
                                }
                                while (reader.Read())
                                {
                                    //string? programCodes = "";
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        string columnValue = reader.IsDBNull(i) ? "NULL" : reader.GetString(i);

                                        switch (i)
                                        {
                                            case 0:
                                                student.StudentId = columnValue;
                                                break;
                                            case 1:
                                                student.FirstName = columnValue;
                                                break;
                                            case 2:
                                                student.lastName = columnValue;
                                                break;
                                            case 3:
                                                student.Email = columnValue;
                                                break;
                                            case 4:
                                                ProgramCode = columnValue;
                                                break;

                                        }

                                    }

                                }
                            }
                            else
                            {
                                Console.WriteLine($"There are No Student exists with that student ID try other Student ID");
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Occurred - {ex.Message}");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return student;
        }

        #endregion

        #region UpdateStudent
        public bool UpdateStudent(Student students)
        {


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("UpdateStudent", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.Parameters.AddWithValue("@StudentID", students.StudentId).Size = 10;
                        command.Parameters.AddWithValue("@FirstName", students.FirstName).Size = 25;
                        command.Parameters.AddWithValue("@LastName", students.lastName).Size = 25;

                        command.Parameters.AddWithValue("@Email", students.Email).Size = 50;
                        command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Occurred {ex.Message}");
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

        #region DeleteStudent

        public bool DeleteStudent(string? studentId)
        {
            if (studentId == "")
                studentId = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("DeleteStudent", conn))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StudentID", studentId).Size = 10;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Occurred {ex.Message}");
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

        #region GetStudents

        public Models.Program GetStudents(string programCode)
        {

            // Student studentsInfo = new Student();
            Models.Program enrolledStudent = new Models.Program();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("GetStudentsByProgram", conn))
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
                                    if (reader.FieldCount - 2 == i)
                                        Console.Write($"{reader.GetName(i)}\t\t\t\t");
                                    else
                                        Console.Write($"{reader.GetName(i)}\t\t");
                                }
                                Console.WriteLine();

                                while (reader.Read())
                                {
                                    Student studentsInfo = new Student(); // Create a new object for each row
                                    string programCodes = string.Empty;
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {

                                        string columnValue = reader[i].ToString();
                                        //string result =

                                        // Check if the value is DBNull.Value or an empty string, and replace it with "NULL"
                                        if (DBNull.Value.Equals(reader[i]) || string.IsNullOrEmpty(columnValue))
                                        {
                                            columnValue = "NULL";
                                        }

                                        // Depending on the column index, assign the values to the properties of studentsInfo
                                        switch (i)
                                        {
                                            case 0:
                                                studentsInfo.StudentId = columnValue;
                                                break;
                                            case 1:
                                                studentsInfo.FirstName = columnValue;
                                                break;
                                            case 2:
                                                studentsInfo.lastName = columnValue;
                                                break;
                                            case 3:
                                                studentsInfo.Email = columnValue;
                                                break;
                                            case 4:
                                                programCodes = columnValue;
                                                break;

                                        }


                                    }
                                    enrolledStudent.EnrolledStudents.Add(studentsInfo);
                                    enrolledStudent.ProgramCode = programCodes;




                                }


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

            return enrolledStudent;



        }

        #endregion
    }
}
