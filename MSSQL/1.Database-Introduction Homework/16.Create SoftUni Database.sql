--CREATE DATABASE SoftUni

--USE SoftUni

--•	Towns (Id, Name)
CREATE TABLE Towns
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(50) NOT NULL
)

--•	Addresses (Id, AddressText, TownId)

CREATE TABLE Addresses
(
	Id INT PRIMARY KEY IDENTITY,
	AddressText NVARCHAR(100) NOT NULL,
	TownId INT FOREIGN KEY REFERENCES Towns(Id) NOT NULL
)

--•	Departments (Id, Name)
CREATE TABLE Departments 
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(50) NOT NULL
)

--•	Employees (Id, FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary, AddressId)

CREATE TABLE Employees
(
	Id INT PRIMARY KEY IDENTITY,
	FirstName  NVARCHAR(20) NOT NULL,
	MiddleName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	JobTitle NVARCHAR(50) NOT NULL,
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
	HireDate DATE NOT NULL,
	Salary DECIMAL(7,2) NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
)
