using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var softUniContext = new SoftUniContext();

            var result = RemoveTown(softUniContext);
            Console.WriteLine(result);
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(x => new 
                { 
                    x.EmployeeId,
                    x.FirstName,
                    x.LastName,
                    x.MiddleName,
                    x.JobTitle,
                    x.Salary
                })
                .OrderBy(e => e.EmployeeId)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {

            var employees = context.Employees
                .Select(x => new
                {
                    x.FirstName,
                    x.Salary
                })
                .Where(s => s.Salary > 50000)
                .OrderBy(n => n.FirstName);

            var sb = new StringBuilder();

            foreach (var item in employees)
            {
                sb.AppendLine($"{item.FirstName} - {item.Salary:F2}");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Department.Name,
                    x.Salary
                })
                .Where(d => d.Name == "Research and Development")
                .OrderBy(n => n.Salary)
                .ThenByDescending(x => x.FirstName);

            var sb = new StringBuilder();

            foreach (var item in employees)
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} from {item.Name} - ${item.Salary:F2}");
            }

            var result = sb.ToString().TrimEnd();

            return result;

        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.Addresses.Add(address);
            context.SaveChanges();

            var nakov = context.Employees
                .FirstOrDefault(x => x.LastName == "Nakov");

            nakov.AddressId = address.AddressId;

            context.SaveChanges();

            var addresses = context.Employees
                .Select(x => new 
                { 
                    x.Address.AddressText,
                    x.Address.AddressId
                })
                .OrderByDescending(a => a.AddressId)
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var item in addresses)
            {
                sb.AppendLine(item.AddressText);
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees
                .Include(x => x.EmployeesProjects)
                .ThenInclude(x => x.Project)
                .Where(x => x.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Select(x => new
                {
                    employyeFirstName = x.FirstName,
                    employeeLastName = x.LastName,
                    managerFirstName = x.Manager.FirstName,
                    managerLastName = x.Manager.LastName,
                    projects = x.EmployeesProjects.Select(p => new
                    {
                        projectName =  p.Project.Name,
                        StartDate = p.Project.StartDate,
                        EndDate = p.Project.EndDate

                    })
                })
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var empl in employees)
            {
                sb.AppendLine($"{empl.employyeFirstName} {empl.employeeLastName} - Manager: {empl.managerFirstName} {empl.managerLastName}");

                foreach (var empProj in empl.projects)
                {
                    if (empProj.EndDate.HasValue)
                    {
                        sb.AppendLine($"--{empProj.projectName} - {empProj.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)} - {empProj.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}");
                    }
                    else
                    {
                        sb.AppendLine($"--{empProj.projectName} - {empProj.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)} - not finished");
                    }
                   
                }
            }

            var result = sb.ToString().TrimEnd();

            return result;

        }


        public static string GetAddressesByTown(SoftUniContext context)
        {

            var addresses = context.Addresses
                .Select(x => new
                {
                    x.AddressText,
                    x.Town.Name,
                    x.Employees.Count
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.Name)
                .ThenBy(x => x.AddressText)
                .Take(10);

            var sb = new StringBuilder();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}, {address.Name} - {address.Count} employees");
            }

            var result = sb.ToString().TrimEnd();

            return result;

        }


        public static string GetEmployee147(SoftUniContext context)
        {

            var employee147 = context.Employees
                .Select(x => new
                {
                    x.EmployeeId,
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    projects = x.EmployeesProjects.Select(p => new
                    {
                        p.Project.Name
                    })
                    .OrderBy(x => x.Name)
                    .ToList()

                })
                .FirstOrDefault(x => x.EmployeeId == 147);
                

            var sb = new StringBuilder();

             sb.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");

                foreach (var projects in employee147.projects)
                {
                    sb.AppendLine($"{projects.Name}");
                }
            var result = sb.ToString().TrimEnd();

            return result;
        }


        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {

            var departments = context.Departments
                .Where(x => x.Employees.Count > 5)
                .OrderBy(x => x.Employees.Count)
                .ThenBy(x => x.Name)
                .Select(x => new
                {

                    x.Name,
                    x.Manager.FirstName,
                    x.Manager.LastName,
                    employees = x.Employees.Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                    .ToList()
                })
               
                .ToList();

            var sb = new StringBuilder();

            foreach (var dept in departments)
            {
                sb.AppendLine($"{dept.Name} - {dept.FirstName} {dept.LastName}");

                foreach (var employee in dept.employees)
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects
                .OrderByDescending(x => x.StartDate)
                .Take(10)
                .Select(x => new
                {
                    Name = x.Name,
                    Description = x.Description,
                    StartDate = x.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                })
                .OrderBy(x => x.Name)
                .ToList();
              

            var sb = new StringBuilder();

            foreach (var proj in projects)
            {
                sb.AppendLine($"{proj.Name}" +
                    $"{Environment.NewLine}" +
                    $"{proj.Description}" +
                    $"{Environment.NewLine}" +
                    $"{proj.StartDate}");
            }

            var result = sb.ToString().TrimEnd();

            return result;

        }


        public static string IncreaseSalaries(SoftUniContext context)
        {
            var departments = new string[] { "Engineering", "Tool Design", "Marketing", "Information Services" };

            var employees = context.Employees
                .Where(x => departments.Contains(x.Department.Name))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                employee.Salary *= 1.12M;
            }

            foreach (var empl in employees)
            {
                sb.AppendLine($"{empl.FirstName} {empl.LastName} (${empl.Salary:F2})");
            }

            return sb.ToString().TrimEnd();
        }


        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(x => x.FirstName.StartsWith("Sa"))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var empl in employees)
            {
                sb.AppendLine($"{empl.FirstName} {empl.LastName} - {empl.JobTitle} - (${empl.Salary:F2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string DeleteProjectById(SoftUniContext context) 
        {
            var projectDel = context.Projects
                .FirstOrDefault(p => p.ProjectId == 2);

            var employeeProjDel = context.EmployeesProjects
                .Where(p => p.ProjectId == 2)
                .ToList();

            foreach (var emplProj in employeeProjDel)
            {
                context.EmployeesProjects.Remove(emplProj);
            }

            context.Remove(projectDel);

            var projects = context.Projects
                .Select(p => p.Name)
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var proj in projects)
            {
                sb.AppendLine($"{proj}");
            }

            return sb.ToString().TrimEnd();
        }


        public static string RemoveTown(SoftUniContext context) 
        {
            var townToDel = context.Towns
                .Include(x => x.Addresses)
                .FirstOrDefault(t => t.Name == "Seattle");

            var addressToDel = townToDel.Addresses
                .Select(a => a.AddressId)
                .ToList();

            var employees = context.Employees
                .Where(x => x.AddressId.HasValue && addressToDel.Contains(x.AddressId.Value))
                .ToList();


            var addressesCount = addressToDel.Count();

            foreach (var empl in employees)
            {
                empl.AddressId = null; 
            }

            foreach (var item in addressToDel)
            {
                var address = context.Addresses.FirstOrDefault(x => x.AddressId == item);
                context.Addresses.Remove(address);
            }

            context.Towns.Remove(townToDel);

            context.SaveChanges();

            var result = $"{addressesCount} addresses in Seattle were deleted";

            return result;

        }


    }

}
