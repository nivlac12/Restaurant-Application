CREATE OR ALTER PROCEDURE Employees.GetEmployee
   @PersonName NVARCHAR(128)
AS

SELECT E.PersonID, E.RestaurantID, E.JobTitleID, E.Seniority
FROM Employees.Employee E
WHERE E.[Name] = @PersonName;
GO


CREATE OR ALTER PROCEDURE Employees.GetEmployeeByID
   @PersonID INT
AS

SELECT E.[Name], E.RestaurantID, E.JobTitleID, E.Seniority
FROM Employees.Employee E
WHERE E.PersonID = @PersonID;
GO