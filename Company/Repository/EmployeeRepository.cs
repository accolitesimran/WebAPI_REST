using Company.Data;
using Company.Interfaces;
using Company.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Web.Mvc;

namespace Company.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SampleData _sampleData;
        public EmployeeRepository(SampleData sampleData)
        {
            _sampleData = sampleData;
        }
        public bool CreateEmployee(Employee obj)
        {
            if (obj == null)
            {
                return false;
            }
             _sampleData.employees.Add(obj);
            return true;
        }

        public bool DeleteEmployee(int id)
        {
            var employee=_sampleData.employees.Where(e=>e.Id == id).FirstOrDefault();
            if (employee==null)
            {
                return false;
            }
            return _sampleData.employees.Remove(employee);
        }

        public Employee GetEmployeeById(int id)
        {
            return _sampleData.employees.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Employee> GetEmployees()
        {
            return _sampleData.employees.OrderBy(p => p.Id).ToList();
        }

        public bool UpdateEmployeee()
        {
            throw new NotImplementedException();
        }
    }
}
