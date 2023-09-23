using FirstWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace ASPWebApi.Models
{
    public class RepositoryEmployee
    {
        private readonly NorthwindContext _context;
        public RepositoryEmployee(NorthwindContext context)  
        {
            _context = context;
        }



        public List<Employee> AllEmployees()
        {
            return _context.Employees.ToList();
        }



        public Employee FindEmployeeById(int id)
        {
            Employee employee = _context.Employees.Find(id);
            return employee;
        }



        //public void DeleteEmployee(int employeeId)
        //{
           
        //    var employeeToDelete = _context.Employees.Find(employeeId);
        //    if (employeeToDelete != null)
        //    {
               
        //        _context.Employees.Remove(employeeToDelete);
        //        _context.SaveChanges(); 
        //    }
        //    else
        //    {
        //        Console.WriteLine("Employee not Found to delete");
        //    }
        //}



        public int AddEmployee(Employee employee)
        {
            Employee? foundEmp = _context.Employees.Find(employee.EmployeeId);
                if(foundEmp != null)
            {
                throw new Exception("Failedto add employee.duplicate Id");
            }
            EntityState es=_context.Entry(employee).State;
            Console.WriteLine($"EntityState B4Add :{es.GetDisplayName()}");
            _context.Employees.Add(employee);
            es=_context.Entry(employee).State;
            Console.WriteLine($"EntityState After Add: {es.GetDisplayName()}");
           int result= _context.SaveChanges();
            es =_context.Entry(employee).State;
            Console.WriteLine($"Entitystate after savechanges:{es.GetDisplayName()}");
            return result;
        }



        public int UpdateEmployee(Employee updateEmployee)
        {
            EntityState es = _context.Entry(updateEmployee).State;
            Console.WriteLine($"EntityState b4Update :{es.GetDisplayName()}");
            _context.Employees.Update(updateEmployee);
            es=_context.Entry(updateEmployee).State;
            Console.WriteLine($"EntityState after update:{es.GetDisplayName()}");
           int result= _context.SaveChanges();
            es=_context.Entry(updateEmployee).State;
            Console.WriteLine($"Entity state after savechanges:{es.GetDisplayName()}");
            return result;
        }

        public int DeleteEmployee(int id) {
            Employee empToDelete = _context.Employees.Find(id);
            EntityState es = EntityState.Detached;
            int result = 0;
            if( empToDelete != null )
            {
                es =_context.Entry(empToDelete).State;
                Console.WriteLine($"EntityState b4update :{es.GetDisplayName()}");
                _context.Employees.Remove(empToDelete);
                es=_context.Entry(empToDelete).State;
                Console.WriteLine($"Entity state after update :{es.GetDisplayName()}");
                result=_context.SaveChanges();
                es=_context.Entry(empToDelete).State;
                Console.WriteLine($"Entity state after savechanges :{es.GetDisplayName()}");

            }
            return result;
        }



    }
}