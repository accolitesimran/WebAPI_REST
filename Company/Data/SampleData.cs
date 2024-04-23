using Company.Models;

namespace Company.Data
{
    public class SampleData
    {
        public ICollection<Employee> employees { get; set; } = new List<Employee>()
        {
            new Employee
            {
                Id=1,
                Name="Kavya",
                Email="kavya@gmail.com"
            },
            new Employee
            {
                Id=2,
                Name="Alok",
                Email="alok@gmail.com"
            }
        };
    }
}
