DROP TABLE IF EXISTS Employees.Employee
CREATE TABLE Employees.Employee
(
	PersonID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	RestaurantID INT NOT NULL FOREIGN KEY
		REFERENCES Restaurants.Restaurant(RestaurantID),
	JobTitleID INT NOT NULL FOREIGN KEY
		REFERENCES Employees.Jobs(JobTitleID),
	[Name] NVARCHAR(128) NOT NULL,
	Seniority INT NOT NULL
)
