CREATE OR ALTER PROCEDURE Restaurants.GetOrganization
   @OrganizationName NVARCHAR(128)
AS

SELECT O.OrganizationID, O.DateFounded
FROM Restaurants.Organization O
WHERE O.OrganizationName = @OrganizationName;
GO