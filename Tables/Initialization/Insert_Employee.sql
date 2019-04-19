CREATE OR ALTER PROCEDURE Restaurants.CreateRestaurant
   @RestaurantID INT,
   @JobTitleID INT,
   [Name] NVARCHAR(128),
   Seniority NVARCHAR(128)
AS

INSERT Employees.Employee(RestaurantID, JobTitleID, [Name], Seniority)
VALUES(@RestaurantID, @JobTitleID, [Name], @Seniority);

SET @PersonID = SCOPE_IDENTITY();
GO
