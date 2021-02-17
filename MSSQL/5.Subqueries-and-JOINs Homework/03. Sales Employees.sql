SELECT e.EmployeeID, e.FirstName, e.LastName, d.Name

FROM Employees AS e

JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
WHERE Name = 'Sales'

ORDER BY EmployeeID