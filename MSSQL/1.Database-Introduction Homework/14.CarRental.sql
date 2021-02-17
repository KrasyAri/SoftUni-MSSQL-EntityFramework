CREATE DATABASE CarRental

--•	Categories (Id, CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
CREATE TABLE Categories
(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(50) NOT NULL,
	DailyRate DECIMAL(4,2),
	WeeklyRate DECIMAL(4,2),
	MonthlyRate DECIMAL(4,2),
	WeekendRate DECIMAL(4,2)
)

INSERT INTO Categories(CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate) VALUES
('Jeep', 5.25, 15.20, 20.40, 65.20),
('SUV', 3.25, 10.20, 15.40, 25.20),
('Sport', 7.25, 25.20, 45.40, 75.20)

--•	Cars (Id, PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
CREATE TABLE Cars
(
	Id INT PRIMARY KEY IDENTITY,
	PlateNumber NVARCHAR(30) NOT NULL,
	Manufacturer NVARCHAR(50) NOT NULL,
	Model NVARCHAR(50) NOT NULL,
	CarYear DATETIME2 NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Doors INT, 
	Picture NVARCHAR(MAX),
	Condition NVARCHAR(20),
	Available BIT NOT NULL
)

INSERT INTO Cars(PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available) VALUES
('1', 'Audi', 'Q7', '2015', 2, 5, NULL, 'New', 1),
('2', 'BMW', 'X5', '2020', 1, 5, NULL, 'New', 0),
('1', 'Porsche', 'Cayenne', '2020', 3, 2, NULL, 'New', 1)

--•	Employees (Id, FirstName, LastName, Title, Notes)

CREATE TABLE Employees
(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	Title NVARCHAR(50),
	Notes NVARCHAR(MAX)
)

INSERT INTO Employees(FirstName, LastName, Title, Notes) VALUES
('Ivan', 'Kirilov', 'TechicalSupport', NULL),
('Pavel', 'Krushovenski', 'Boss', NULL),
('Maria', 'Shopova', 'CallCenter', NULL)


--•	Customers (Id, DriverLicenceNumber, FullName, Address, City, ZIPCode, Notes)

CREATE TABLE Customers
(
	Id INT PRIMARY KEY IDENTITY,
	DriverLicenceNumber INT NOT NULL,
	FullName NVARCHAR(100) NOT NULL,
	[Address] NVARCHAR(200),
	City NVARCHAR(50) NOT NULL,
	ZIPCode INT NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Customers(DriverLicenceNumber, FullName, [Address], City, ZIPCode, Notes) VALUES
(206350245, 'Rumen Popov', NULL, 'Sofia', 1000, NULL),
(365021365, 'Martin Ivanov', 'Vinitsa str, 15', 'Plovdiv', 2000, NULL),
(956458325, 'Ivan Mihov', NULL, 'Bourgas', 5000, NULL)


CREATE TABLE RentalOrders
(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
	CarId INT FOREIGN KEY REFERENCES Cars(Id),
	TankLevel INT NOT NULL,
	KilometrageStart INT NOT NULL,
	KilometrageEnd INT NOT NULL,
	TotalKilometrage AS KilometrageEnd - KilometrageStart,
	StartDate DATETIME2 NOT NULL,
	EndDate DATETIME2 NOT NULL,
	TotalDays AS DATEDIFF(DAY, StartDate, EndDate),
	RateApplied DECIMAL(4,2),
	TaxRate DECIMAL(7,2),
	OrderStatus NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)


INSERT INTO RentalOrders(EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, 
			StartDate, EndDate, RateApplied, TaxRate, OrderStatus, Notes) VALUES
(1, 1, 2, 50, 150, 250, '2020-09-15', '2020-09-20', 8.9, 65.00, 'Complete', NULL),
(2, 2, 1, 75, 102, 132, '2020-08-17', '2020-09-15', 9.9, 75.00, 'Complete', NULL),
(3, 3, 3, 45, 120, 180, '2020-07-07', '2020-08-07', 6.5, 25.00, 'Complete', NULL)