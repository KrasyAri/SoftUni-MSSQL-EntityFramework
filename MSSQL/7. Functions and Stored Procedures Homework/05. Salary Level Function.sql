CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS NVARCHAR(20)
AS
BEGIN
--DECLARE @salary
DECLARE @result NVARCHAR(20)

IF (@salary < 30000)
 SET @result = 'Low'
ELSE IF (@salary BETWEEN 30000 AND 50000)
 SET @result = 'Average'
ELSE SET @result = 'High'

RETURN @result
END

SELECT Salary, [dbo].ufn_GetSalaryLevel(Salary) AS SalaryLevel
	FROM Employees