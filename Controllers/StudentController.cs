using asp.net_core_web_api_Day_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace asp.net_core_web_api_Day_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly string _connectionString;

        public StudentController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerDb")!;
        }

        // CREATE STUDENT
        [HttpPost]
        public IActionResult CreateStudent(StudentDto studentDto)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                string sql =
                    "INSERT INTO Student (Nome, Cognome, Email) VALUES (@Nome, @Cognome, @Email)";

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", studentDto.Nome);
                command.Parameters.AddWithValue("@Cognome", studentDto.Cognome);
                command.Parameters.AddWithValue("@Email", studentDto.Email);

                command.ExecuteNonQuery();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET STUDENTS
        [Authorize]
        [HttpGet]
        public IActionResult GetStudent()
        {
            List<Student> students = new();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                using var command = new SqlCommand("SELECT * FROM Student", connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Cognome = reader.GetString(2),
                        Email = reader.GetString(3)
                    });
                }

                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // UPDATE STUDENT
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, StudentDto studentDto)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                string sql =
                    "UPDATE Student SET Nome=@Nome, Cognome=@Cognome, Email=@Email WHERE Id=@Id";

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", studentDto.Nome);
                command.Parameters.AddWithValue("@Cognome", studentDto.Cognome);
                command.Parameters.AddWithValue("@Email", studentDto.Email);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE STUDENT
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                using var command =
                    new SqlCommand("DELETE FROM Student WHERE Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // CREATE STUDENT PROFILE
        [Authorize]
        [HttpPost("profile")]
        public IActionResult CreateStudentProfile(StudentProfileDto dto)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                string sql =
                    "INSERT INTO StudentProfile (FirstName, LastName, FiscalCode, BirthDate, StudentId) " +
                    "VALUES (@FirstName, @LastName, @FiscalCode, @BirthDate, @StudentId)";

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FirstName", dto.FirstName);
                command.Parameters.AddWithValue("@LastName", dto.LastName);
                command.Parameters.AddWithValue("@FiscalCode", dto.FiscalCode);
                command.Parameters.AddWithValue("@BirthDate", dto.BirthDate);
                command.Parameters.AddWithValue("@StudentId", dto.StudentId);

                command.ExecuteNonQuery();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET STUDENT PROFILES
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetStudentProfile()
        {
            List<StudentProfile> profiles = new();

            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();

                using var command =
                    new SqlCommand("SELECT * FROM StudentProfile", connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    profiles.Add(new StudentProfile
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        FiscalCode = reader.GetString(3),
                        BirthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                        StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"))
                    });
                }

                return Ok(profiles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
