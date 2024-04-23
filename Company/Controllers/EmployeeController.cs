using Company.DTO;
using Company.Interfaces;
using Company.Models;
using Company.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        [Route("/getAllEmployees")]
        [ProducesResponseType(200, Type = typeof(ICollection<Employee>))]
        public IActionResult GetEmployees()
        {
            try
            {
                var employees = _employeeRepository.GetEmployees();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(employees);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error fetching data");
            }
        }
        [HttpGet]
        [Route("/getEmployee/{id}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        public IActionResult GetEmployee(int id)
        {
            try
            {
                var employee = _employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error fetching data");
            }
           
        }

        [HttpPost]
        [Route("/createEmployee")]
        public IActionResult CreateEmployee([FromBody] EmployeeDto obj)
        {
            try
            {
                int newId = _employeeRepository.GetEmployees().LastOrDefault().Id + 1;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Employee emp = new Employee()
                {
                    Id = newId,
                    Name = obj.Name,
                    Email = obj.Email,
                };
                var employee = _employeeRepository.GetEmployeeById(emp.Id);
                if (employee != null)
                {
                    ModelState.AddModelError("", "Employee already exists");
                    return StatusCode(422, ModelState);
                }

                if (!_employeeRepository.CreateEmployee(emp))
                {
                    ModelState.AddModelError("", "Something went wrong while posting data");
                    return StatusCode(500, ModelState);
                }
                return Ok("Successfully created employee");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating data");
            }
            
        }

        [HttpDelete]
        [Route("/deleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                Employee emp = _employeeRepository.GetEmployeeById(id);
                if (emp == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }
                if (!_employeeRepository.DeleteEmployee(id))
                {
                    ModelState.AddModelError("", "Employee could not be deleted");
                    return StatusCode(500, ModelState);
                }
                return Ok("Employee deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [HttpPut]
        [Route("/updateEmployee/{id}")]
        public IActionResult UpdateEmployee(int id,Employee emp) {
            try
            {
                if (id != emp.Id)
                    return BadRequest("Employee ID mismatch");

                Employee EmployeeToUpdate = _employeeRepository.GetEmployeeById(id);

                if (EmployeeToUpdate == null)
                    return NotFound($"Employee with Id = {id} not found");

                EmployeeToUpdate.Email = emp.Email;
                EmployeeToUpdate.Name=emp.Name;
                return Ok(EmployeeToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

       

    }
}
