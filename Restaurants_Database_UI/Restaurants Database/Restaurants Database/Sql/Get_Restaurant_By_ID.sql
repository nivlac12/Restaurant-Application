CREATE OR ALTER PROCEDURE Restaurants.GetRestaurantByID
   @RestaurantID INT
AS

SELECT R.RestaurantName, R.OrganizationID, R.DateFounded, R.IsOperational
FROM Restaurants.Restaurant R
WHERE R.RestaurantID = @RestaurantID;
GO