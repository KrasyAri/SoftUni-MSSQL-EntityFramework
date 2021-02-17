CREATE PROC usp_GetTownsStartingWith(@Letter VARCHAR(MAX))
AS
SELECT Name AS Town
	FROM Towns
 WHERE Name LIKE @Letter + '%'

 EXEC usp_GetTownsStartingWith 'b'