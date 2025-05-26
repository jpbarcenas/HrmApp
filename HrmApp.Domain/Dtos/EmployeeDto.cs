namespace HrmApp.Domain.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
    }
}
