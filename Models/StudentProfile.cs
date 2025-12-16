namespace asp.net_core_web_api_Day_1.Models
{
    public class StudentProfile
    {
        //Bonus:  implementare una nuova entità relazionata con Student chiamata StudentProfile:
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FiscalCode { get; set; }

        public DateTime BirthDate { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public static implicit operator StudentProfile(Student v)
        {
            throw new NotImplementedException();
        }
    }
}
