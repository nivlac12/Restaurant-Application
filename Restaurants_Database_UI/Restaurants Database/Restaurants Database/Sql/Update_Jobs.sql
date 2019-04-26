CREATE OR ALTER PROCEDURE Employees.UpdateJobs
	@JobTitleID INT,
    @JobName NVARCHAR(64),
	@Salary DECIMAL
AS

UPDATE Employees.Employee
SET
	JobName = @JobName,
	Salary = @Salary
WHERE JobTitleID = @JobTitleID