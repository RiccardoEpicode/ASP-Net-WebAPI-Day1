using asp.net_core_web_api_Day_1.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static System.Net.WebRequestMethods;

namespace asp.net_core_web_api_Day_1.Controllers
{
    [Route("api/[controller]")] // Questa riga definisce l’URL.
    [ApiController] // ATTRIBUTO CHE DA ISTRUZIONI A .NET E DEFINISCE QUESTO CONTROLLER COME WEB API

                    /* Attiva automaticamente:
                     
                       - validazione automatica dei DTO
                       - binding automatico del JSON
                       - risposte HTTP corrette(400, 200, ecc.)
                    */

    public class StudentController : ControllerBase /* GRAZIE A ControllerBase EREDITIAMO 

                                                      - ottieni metodi come Ok(), BadRequest(), etc.

                                                      - puoi rispondere HTTP
                                                    */
    {
        //CONNESSIONE AL DATABASE
        private readonly string connectionString;

        //COSTRUTTORE
        public StudentController(IConfiguration configuration) // IConfiguration è un oggetto fornito da
                                                               // ASP.NET Core.che ci permette con
                                                               // la DI di leggere appsettings.json
        {
            connectionString = configuration.GetConnectionString("SqlServerDb")!;
        }

        //CREATE-POST

        [HttpPost]
        public IActionResult CreateStudent(StudentDto studentDto)
        {
            try
            {
                //creiamo una connessione al DB
                using (var connection = new SqlConnection(connectionString))
                {
                    // apriamo la connessione al database
                    connection.Open();

                    //facciamo una QUERY SQL
                    string sql =
                        "INSERT INTO Student (Nome, Cognome, Email) VALUES (@Nome, @Cognome, @Email)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", studentDto.Nome);
                        command.Parameters.AddWithValue("@Cognome", studentDto.Cognome);
                        command.Parameters.AddWithValue("@Email", studentDto.Email);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        //GET

        [HttpGet]
        public IActionResult GetStudent() 
        {
            List<Student> students = new List<Student>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql =
                        "SELECT * FROM Student";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            { 
                                Student student = new Student();
                                student.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                                student.Nome = reader.GetString(1);
                                student.Cognome = reader.GetString(2);
                                student.Email = reader.GetString(3);

                                students.Add(student);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(students);
        }

        //UPDATE
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, StudentDto studentDto) 
        {
            try
            {
                using (var connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();

                    string sql = "UPDATE Student SET Nome=@Nome, Cognome=@cognome, Email=@Email WHERE Id=@Id";

                    using (var command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@Nome", studentDto.Nome);
                        command.Parameters.AddWithValue("@Cognome", studentDto.Cognome);
                        command.Parameters.AddWithValue("@Email", studentDto.Email);
                        command.Parameters.AddWithValue("@Id", id);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id) 
        {
            try 
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Student WHERE Id=@Id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        command.ExecuteNonQuery();
                    }
                }

            } 
            catch (Exception ex) 
            {
               return BadRequest(ex.Message);
            }

            return Ok();
        }

        //STUDENT PROFILE

        [HttpPost("profile")]
        public IActionResult CreateStudentProfile(StudentProfileDto studentProfileDto)
        {
            try
            {
                //creiamo una connessione al DB
                using (var connection = new SqlConnection(connectionString))
                {
                    // apriamo la connessione al database
                    connection.Open();

                    //facciamo una QUERY SQL
                    string sql =
                        "INSERT INTO StudentProfile (FirstName, LastName, FiscalCode, BirthDate, StudentId) " +
                        "VALUES (@FirstName, @LastName, @FiscalCode, @BirthDate, @StudentId)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", studentProfileDto.FirstName);
                        command.Parameters.AddWithValue("@LastName", studentProfileDto.LastName);
                        command.Parameters.AddWithValue("@FiscalCode", studentProfileDto.FiscalCode);
                        command.Parameters.AddWithValue("@BirthDate", studentProfileDto.BirthDate);
                        command.Parameters.AddWithValue("@StudentId", studentProfileDto.StudentId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        //GET

        [HttpGet("profile")]
        public IActionResult GetStudentProfile()
        {
            List<StudentProfile> studentProfiles = new List<StudentProfile>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql =
                        "SELECT * FROM StudentProfile";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentProfile studentProfile = new StudentProfile();
                                studentProfile.StudentId = reader.GetInt32(reader.GetOrdinal("Id"));
                                studentProfile.FirstName = reader.GetString(1);
                                studentProfile.LastName = reader.GetString(2);
                                studentProfile.FiscalCode = reader.GetString(3);
                                studentProfile.BirthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate"));

                                studentProfiles.Add(studentProfile);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(studentProfiles);
        }
    }
}
