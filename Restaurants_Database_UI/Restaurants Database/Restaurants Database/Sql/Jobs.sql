CREATE TABLE Employees.Jobs
(
	JobTitleID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	JobName NVARCHAR(64) NOT NULL,
	Salary DECIMAL NOT NULL
)