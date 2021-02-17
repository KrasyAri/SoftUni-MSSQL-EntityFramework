SELECT TOP(2) DepositGroup
	FROM
	(SELECT DepositGroup, AVG(MagicWandSize) AS [AvgMagicSize]
		FROM WizzardDeposits
		GROUP BY DepositGroup) AS AvarageSizeQuery

		ORDER BY AvgMagicSize