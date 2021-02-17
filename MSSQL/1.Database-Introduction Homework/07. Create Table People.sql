CREATE TABLE People
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(200) NOT NULL,
	Picture IMAGE,
	Height DECIMAL(5, 2),
	[Weight] DECIMAL(5,2),
	Gender CHAR(1) NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX)
)

INSERT INTO People(Name, Picture, Height, [Weight], Gender, Birthdate, Biography) VALUES
('Krasimira', NULL, 160, 58, 'f', '1987-02-22', 'Spa Therapist'),
('Ivan', NULL, 175, 75, 'm', '1987-01-25', 'Shop Manager'),
('Pavel', NULL, 170, 65, 'm', '1982-02-05', 'Driver'),
('Anita', NULL, 162, 55, 'f', '1984-04-24', 'Spa Owner'),
('Margarita', NULL, 158, 63, 'f', '1964-10-16', 'Spa Manager')