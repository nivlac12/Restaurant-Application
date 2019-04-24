CREATE OR ALTER PROCEDURE Restaurants.GetOrganization
   @JobTitleID INT
AS

SELECT  J.JobName, J.Salary
FROM Employee.Jobs J
WHERE J.JobTitleID = @JobTitleID;
GO