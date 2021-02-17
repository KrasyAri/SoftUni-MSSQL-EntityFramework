CREATE PROC usp_EmployeesBySalaryLevel(@salaryLevel NVARCHAR(20))
AS
	SELECT FirstName, LastName FROM Employees
 WHERE @salaryLevel = dbo.ufn_GetSalaryLevel(Salary)

 EXEC usp_EmployeesBySalaryLevel 'High'