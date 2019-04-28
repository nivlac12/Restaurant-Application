CREATE OR ALTER PROCEDURE Restaurants.UpdateOrganization
	@OrganizationID INT,
	@OrganizationName NVARCHAR(128)
AS

UPDATE Restaurants.Organization
SET
	OrganizationName = @OrganizationName
WHERE OrganizationID = @OrganizationID