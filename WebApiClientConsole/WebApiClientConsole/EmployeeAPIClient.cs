﻿using FirstWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApiClientConsole
{
    internal class EmployeeAPIClient
    {
        static Uri uri = new Uri("http://localhost:5124/");
        public static async Task CallGetAllEmployee()
        {
            using(var client =new HttpClient())
            {
                client.BaseAddress = uri;
                
                HttpResponseMessage response = await client.GetAsync("GetAllEmployees");
                response.EnsureSuccessStatusCode();
                if(response.IsSuccessStatusCode)
                {
                    String x=await response.Content.ReadAsStringAsync();
                    await Console.Out.WriteAsync(x);
                }
            }
           
            }

        public static async Task GetAllEmployeeJson()
        { 

            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                List<EmpViewModel> employees = new List<EmpViewModel>();
                client.DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTPGET:
                HttpResponseMessage response = await client.GetAsync("GetAllEmployees");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    employees = JsonConvert.DeserializeObject<List<EmpViewModel>>(json);
                    foreach (EmpViewModel emp in employees)
                    {
                        await Console.Out.WriteLineAsync($"{emp.EmpId},{emp.FirstName}:{emp.LastName}");
                    }
                }
            }
        }
        public static async Task AddnewEmployee()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                EmpViewModel employee = new EmpViewModel()
                {
                    FirstName = "William",
                    LastName = "John",
                    City ="Nyc",
                    BirthDate =new DateTime(1980,01,01),
                    HireDate=new DateTime(2000,01,01),
                    Title="Manager"
                };
                var myContent =JsonConvert.SerializeObject(employee);
                var buffer=Encoding.UTF8.GetBytes(myContent);
                var byteContent=new ByteArrayContent(buffer);
                byteContent.Headers.ContentType=new MediaTypeHeaderValue("application/json");
                //HttpPost:
                HttpResponseMessage response = await client.PostAsync("AddNewEmployees", byteContent);
                response.EnsureSuccessStatusCode();
                if(response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                }

            }
        }
        public static async Task UpdateEmployee(EmpViewModel employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));



                var myContent = JsonConvert.SerializeObject(employee);  // Serializing object to JSON
                var buffer = Encoding.UTF8.GetBytes(myContent);         // Convert JSON string to byte array
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");



                // HttpPut:
                HttpResponseMessage response = await client.PutAsync($"UpdateEmployee", byteContent);
                //response.EnsureSuccessStatusCode();



                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Employee updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to update the employee. Status Code: {response.StatusCode}. Reason: {response.ReasonPhrase}");
                }



            }
        }
        public static async Task DeleteEmployee(int employeeId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string deleteUrl = $"DeleteEmployee/{employeeId}";
                HttpResponseMessage response = await client.DeleteAsync(deleteUrl);



                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync("Employee deleted successfully.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    await Console.Out.WriteLineAsync("Employee not found.");
                }
                else
                {
                    await Console.Out.WriteLineAsync($"Error: {response.StatusCode}");
                }
            }
        }

    }
}
