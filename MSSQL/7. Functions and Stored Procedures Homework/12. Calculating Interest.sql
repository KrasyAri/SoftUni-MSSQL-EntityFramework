CREATE PROC usp_CalculateFutureValueForAccount(@accountId INT, @interestRate FLOAT)
AS
 SELECT ah.Id, ah.FirstName, ah.LastName, a.Balance, dbo.ufn_CalculateFutureValue(a.Balance, @interestRate, 5) AS [Balance in 5 years]
  FROM Accounts AS a
   JOIN AccountHolders AS ah ON a.AccountHolderId = ah.Id
    WHERE a.Id = @accountId

EXEC usp_CalculateFutureValueForAccount 1, 0.1