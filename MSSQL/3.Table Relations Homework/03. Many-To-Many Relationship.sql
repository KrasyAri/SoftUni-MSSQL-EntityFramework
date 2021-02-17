CREATE TABLE Students
(
	StudentID INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(10)
)

CREATE TABLE Exams
(
	ExamID INT IDENTITY(101,1) PRIMARY KEY,
	Name NVARCHAR(50),
)

INSERT INTO Students(Name) VALUES
('Mila'),
('Toni'),
('Ron')

INSERT INTO Exams(Name) VALUES
('SpringMVC'),
('Neo4j'),
('Oracle 11g')

CREATE TABLE StudentsExams
(
	StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
	ExamID INT FOREIGN KEY REFERENCES Exams(ExamID)

	CONSTRAINT PK_StudentsExams PRIMARY KEY(StudentID, ExamID)
)

INSERT INTO StudentsExams VALUES
(1,101),
(1,102),
(2,101),
(3,103),
(2,102),
(2,103)