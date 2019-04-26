CREATE OR ALTER PROCEDURE Employees.UodateEmployees
   @PersonID INT,
   @RestaurantID INT,
   @JobTitleID INT,
   @Seniority NVARCHAR(128)

AS

UPDATE Employees.Employee
SET 
	RestaurantID = @RestaurantID,
	JobTitleID =  @JobTitleID,
	Seniority =@Seniority

WHERE PersonID = @PersonID;