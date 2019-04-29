CREATE OR ALTER PROCEDURE Restaurants.GetRestaurant
   @RestaurantName NVARCHAR(128)
AS

SELECT R.RestaurantID, R.OrganizationID, R.DateFounded, R.IsOperational
FROM Restaurants.Restaurant R
WHERE R.RestaurantName = @RestaurantName;
GO