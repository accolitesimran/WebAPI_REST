using Company.Models;

namespace Company.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
        bool CreateEmployee(Employee obj);
        bool DeleteEmployee(int id);
        bool UpdateEmployeee();
    }
}
