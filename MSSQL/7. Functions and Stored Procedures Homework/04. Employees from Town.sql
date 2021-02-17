CREATE PROC usp_GetEmployeesFromTown (@Town NVARCHAR(MAX))
AS
SELECT FirstName, LastName 
	FROM Employees AS e
JOIN Addresses AS a 
	ON e.AddressID = a.AddressID
JOIN Towns AS t ON t.TownID = a.TownID

 WHERE t.Name LIKE @Town

 EXEC usp_GetEmployeesFromTown 'Sofia'