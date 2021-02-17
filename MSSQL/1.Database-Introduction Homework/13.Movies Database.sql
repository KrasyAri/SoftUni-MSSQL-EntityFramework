CREATE DATABASE Movies

USE Movies

CREATE TABLE Directors 
(
	Id INT PRIMARY KEY IDENTITY,
	DirectorName VARCHAR(200) NOT NULL,
	Notes VARCHAR(MAX)
)

INSERT INTO Directors(DirectorName, Notes) VALUES
('Stoyan Petrov', 'Audio'),
('Ivan Ivanow', NULL),
('Petya Mihova', 'Clothes Designer'),
('Vanya Jeleva', 'MakeUp'),
('Kiril Manchev', 'Operation')

CREATE TABLE Genres
(
	Id INT PRIMARY KEY IDENTITY,
	GenreName VARCHAR(50) NOT NULL,
	Notes VARCHAR(MAX)
)

INSERT INTO Genres(GenreName, Notes) VALUES
('Horror', 'Over 16'),
('Comedy', 'No age restrictions'),
('Fantasy', 'Over 12'),
('Animation', 'No age restrictions'),
('Biography', NULL)


CREATE TABLE Categories
(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName VARCHAR(50) NOT NULL,
	Notes VARCHAR(MAX)
)

INSERT INTO Categories (CategoryName, Notes) VALUES
('Best Actor', NULL),
('Best Soundtrack', NULL),
('Best Effects', NULL),
('Best Comedy Show', NULL),
('Movie Of The Year', NULL)

CREATE TABLE Movies
(
	Id INT PRIMARY KEY IDENTITY,
	Title VARCHAR(100) NOT NULL,
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id),
	CopyrightYear DATETIME2 NOT NULL,
	[Length] TIME NOT NULL,
	GenreId INT FOREIGN KEY REFERENCES Genres(Id),
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Rating DECIMAL(2,1),
	Notes VARCHAR(MAX)
)

INSERT INTO Movies(Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes) VALUES
('KungFu Panda', 1, '2015', '01:35', 4, 1, 9.6, NULL),
('Ordinary Love Story', 2, '2008', '01:24', 1, 2, 8.3, NULL),
('It', 3, '1994', '02:15', 1, 3, 9.0, NULL),
('Tesla', 4, '2014', '02:40', 4, 4, 9.9, NULL),
('Fantastic Four', 5, '2007', '01:56', 3, 5, 7.6, NULL)

