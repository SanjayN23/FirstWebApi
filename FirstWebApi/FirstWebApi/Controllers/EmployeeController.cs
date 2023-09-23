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




        [HttpGet("/GetAllEmployees")]
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
                               ReportsTo = emp.ReportsTo > 0? emp.ReportsTo :null
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



        //[HttpPost]
        //public Employee PostEmployee([FromBody] Employee newEmployee)
        //{
        //    return _repositoryEmployee.AddEmployee(newEmployee);
        //}



        //[HttpPut("id")]
        [HttpPut("/UpdateEmployee")]
        public int UpdateEmployee([FromBody] EmpViewModel updateEmployee)
        {
            //updateEmployee.EmployeeId = id;
            Employee employee = new Employee() { 
            EmployeeId=updateEmployee.EmpId, FirstName=updateEmployee.FirstName, LastName=updateEmployee.LastName
            ,BirthDate = updateEmployee.BirthDate,HireDate = updateEmployee.HireDate,City=updateEmployee.City,ReportsTo=updateEmployee.ReportsTo, Title=updateEmployee.Title
            };
            int saveEmployee = _repositoryEmployee.UpdateEmployee(employee);
            return saveEmployee;
        }

   [HttpPost("/AddNewEmployees")]

public int CreateEmployee(EmpViewModel newEmployee)

        {

            /*_repositoryEmployee.AddEmployee(newEmployee); 

            return newEmployee;*/

            Employee emp = new Employee()

            {

                FirstName = newEmployee.FirstName,

                LastName = newEmployee.LastName,

                BirthDate = newEmployee.BirthDate,

                HireDate = newEmployee.HireDate,

                Title = newEmployee.Title,

                City = newEmployee.City,

                ReportsTo = newEmployee.ReportsTo > 0 ? newEmployee.ReportsTo : null

            };

            int result = _repositoryEmployee.AddEmployee(emp);

            return result;

        }

    }
}