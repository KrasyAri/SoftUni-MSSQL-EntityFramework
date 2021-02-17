SELECT MIN(a.AvarageSalary) AS [MinAverageSalary]
	FROM (SELECT AVG(e.Salary) AS [AvarageSalary] 
				FROM Employees AS e
				GROUP BY e.DepartmentID) AS a

ORDER BY MinAverageSalary