SELECT e.FirstName, e.LastName, e.HireDate, d.Name
	FROM Employees AS e
JOIN Departments AS d ON e.DepartmentID = d.DepartmentID

WHERE d.Name = 'Sales' OR d.Name = 'Finance'
AND e.HireDAte > 1999-01-01

ORDER BY HireDate