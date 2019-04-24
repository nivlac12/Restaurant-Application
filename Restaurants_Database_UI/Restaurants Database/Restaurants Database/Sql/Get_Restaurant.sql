CREATE OR ALTER PROCEDURE Restaurants.GetRestaurant
   @RestuarantID INT
AS

SELECT R.OrganizationID, R.RestaurantName, R.DateFounded, R.IsOperational
FROM Restaurants.Restaurant R
WHERE R.RestaurantID = @RestuarantID;
GO