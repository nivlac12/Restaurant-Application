CREATE OR ALTER PROCEDURE Restaurants.RetrieveRestaurants
AS

SELECT R.RestaurantID, R.OrganizationID, R.RestaurantName, R.DateFounded, R.IsOperational
FROM Restaurants.Restaurant R
GO