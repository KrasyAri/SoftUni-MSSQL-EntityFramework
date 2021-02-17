SELECT c.CountryCode, m.MountainRange, p.PeakName, p.Elevation
	FROM Countries AS c

JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
JOIN Mountains AS m ON mc.MountainId = m.Id
JOIN Peaks AS p ON m.Id = p.MountainID

	WHERE c.CountryCode = 'BG' AND Elevation > 2835

	ORDER BY p.Elevation DESC