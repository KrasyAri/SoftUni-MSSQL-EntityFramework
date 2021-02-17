CREATE PROC usp_GetHoldersWithBalanceHigherThan(@money DECIMAL(15,2))
AS
SELECT ah.FirstName AS [First Name], ah.LastName AS [Last Name]
	FROM AccountHolders AS ah
JOIN Accounts As a ON ah.Id = a.AccountHolderId
	GROUP BY FirstName, LastName
 HAVING SUM(a.Balance) > @money

 ORDER BY FirstName, LastName

 EXEC usp_GetHoldersWithBalanceHigherThan 5000