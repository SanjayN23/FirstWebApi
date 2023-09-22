using ASPWebApi.Models;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace ASPWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private RepositoryEmployee _repositoryEmployee;



        public EmployeeController(RepositoryEmployee employeeRepository)
        {
            _repositoryEmployee = employeeRepository;
        }




        [HttpGet]
        public IEnumerable<EmpViewModel> AllEmployees()
        {
            List<Employee> employees = _repositoryEmployee.AllEmployees();
            var emplist = (from emp in employees
                           select new EmpViewModel()
                           {
                               EmpId = emp.EmployeeId,
                               FirstName = emp.FirstName,
                               LastName = emp.LastName,
                               BirthDate = emp.BirthDate,
                               HireDate = emp.HireDate,
                               Title = emp.Title,
                               City = emp.City,
                               ReportsTo = emp.ReportsTo
                           }
                ).ToList();
            return emplist;



        }



        [HttpGet("id")]
        public Employee EmployeeDetails(int id)
        {
           
            Employee employees = _repositoryEmployee.FindEmployeeById(id);
            return employees;
        }



        [HttpDelete("id")]
        public int DeleteEmployee(int id)
        {
            
            _repositoryEmployee.DeleteEmployee(id);



           
            return id; 
        }



        [HttpPost]
        public Employee PostEmployee([FromBody] Employee newEmployee)
        {
            return _repositoryEmployee.AddEmployee(newEmployee);
        }



        [HttpPut("id")]
        public Employee UpdateEmployee(int id, [FromBody] Employee updateEmployee)
        {
            updateEmployee.EmployeeId = id;
            Employee saveEmployee = _repositoryEmployee.UpdateEmployee(updateEmployee);
            return saveEmployee;
        }



    }
}