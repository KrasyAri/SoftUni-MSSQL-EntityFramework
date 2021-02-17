CREATE PROC usp_GetEmployeesSalaryAboveNumber(@Money DECIMAL(15,2))
AS
SELECT FirstName, LastName
	FROM Employees
 WHERE Salary >= @Money

 EXEC usp_GetEmployeesSalaryAboveNumber 35000