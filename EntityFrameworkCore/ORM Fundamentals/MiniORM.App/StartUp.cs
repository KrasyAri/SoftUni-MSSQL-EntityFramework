using MiniORM.App.Data;
using MiniORM.App.Data.Entities;
using System;
using System.Linq;

namespace MiniORM.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=MiniORM;Integrated Security=True";

            var context = new SoftUniDbContextClass(connectionString);


            context.Employees.Add(new Employee
            {
                FirstName = "Gosho",
                LastName = "Inserted",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true

            });

            var employee = context.Employees.Last();
            employee.FirstName = "Krasi";

            context.SaveChanges();
        }
    }
}
