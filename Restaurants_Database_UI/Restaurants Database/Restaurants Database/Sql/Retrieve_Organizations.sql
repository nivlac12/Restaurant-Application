CREATE OR ALTER PROCEDURE Restaurants.RetrieveOrganizations
AS

SELECT O.OrganizationID, O.OrganizationName, O.DateFounded
FROM Restaurants.Organization;
GO
