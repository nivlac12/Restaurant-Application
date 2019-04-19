CREATE OR ALTER PROCEDURE Restaurants.CreateRestaurant
   @JobName NVARCHAR(64),
   @Salary DECIMAL
AS

INSERT Employees.Jobs(JobName, Salary)
VALUES(@JobName, @Salary);

SET @JobTitleID = SCOPE_IDENTITY();
GO
