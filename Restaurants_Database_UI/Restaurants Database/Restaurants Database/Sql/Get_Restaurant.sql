CREATE OR ALTER PROCEDURE Restaurants.GetRestaurant
   @RestuarantName INT
AS

SELECT R.RestaurantID, R.OrganizationID, R.DateFounded, R.IsOperational
FROM Restaurants.Restaurant R
WHERE R.RestaurantName = @ResturantName;
GO