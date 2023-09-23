// See https://aka.ms/new-console-template for more information
using FirstWebApi.Models;
using WebApiClientConsole;
Console.WriteLine("API CLIENT!");
//EmployeeAPIClient.CallGetAllEmployee().Wait();
//Console.ReadLine();
//EmployeeAPIClient.GetAllEmployeeJson().Wait();
//Console.ReadLine();
//EmployeeAPIClient.AddnewEmployee().Wait();
//Console.ReadLine();
//EmpViewModel employeeToUpdate = new EmpViewModel()
//{
//    EmpId = 12,
//    FirstName = "Update",
//    LastName = "Updatename",
//    City = "Nyc",
//    BirthDate = new DateTime(1980, 01, 01),
//    HireDate = new DateTime(2000, 01, 01),
//    Title = "Manager"
//};

//EmployeeAPIClient.UpdateEmployee(employeeToUpdate).Wait();

EmployeeAPIClient.DeleteEmployee(12).Wait();