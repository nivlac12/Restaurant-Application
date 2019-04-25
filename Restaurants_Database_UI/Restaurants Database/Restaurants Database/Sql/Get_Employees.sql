CREATE OR ALTER PROCEDURE Employees.GetEmployee
   @PersonName NVARCHAR(128)
AS

SELECT E.PersonID, E.RestaurantID, E.JobTitleID, E.Seniority
FROM Employees.Employee E
WHERE E.PersonName = @PersonName;
GO