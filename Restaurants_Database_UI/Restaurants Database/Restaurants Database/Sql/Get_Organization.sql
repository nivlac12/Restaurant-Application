CREATE OR ALTER PROCEDURE Restaurants.GetOrganization
   @OrganizationID INT
AS

SELECT O.OrganizationName, O.DateFounded
FROM Restaurants.Organization O
WHERE O.OrganizationID = @OrganizationID;
GO