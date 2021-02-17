SELECT c.CountryCode, COUNT(m.MountainRange)
	FROM Countries AS c

JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
JOIN Mountains AS m ON mc.MountainId = m.Id
	WHERE c.CountryCode IN ('BG', 'RU', 'US')

GROUP BY c.CountryCode
	