CREATE OR ALTER PROCEDURE Employees.CreateEmployee
   @RestaurantID INT,
   @JobTitleID INT,
   @Name NVARCHAR(128),
   @Seniority NVARCHAR(128),
   @PersonID INT OUTPUT
AS

INSERT Employees.Employee(RestaurantID, JobTitleID, [Name], Seniority)
VALUES(@RestaurantID, @JobTitleID, @Name, @Seniority);

SET @PersonID = SCOPE_IDENTITY();
GO
