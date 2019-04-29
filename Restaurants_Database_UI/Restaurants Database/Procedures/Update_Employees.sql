CREATE OR ALTER PROCEDURE Employees.UpdateEmployee
   @PersonID INT,
   @RestaurantID INT,
   @JobTitleID INT,
   @PersonName NVARCHAR(128),
   @Seniority INT

AS

UPDATE Employees.Employee
SET 
	RestaurantID = @RestaurantID,
	JobTitleID =  @JobTitleID,
	[Name] = @PersonName,
	Seniority = @Seniority

WHERE PersonID = @PersonID;