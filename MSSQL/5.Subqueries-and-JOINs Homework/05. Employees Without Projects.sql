SELECT TOP(3) e.EmployeeID, FirstName
FROM Employees AS e

LEFT JOIN EmployeesProjects AS p ON e.EmployeeID = p.EmployeeID

WHERE ProjectID IS NULL
ORDER BY EmployeeID