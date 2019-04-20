CREATE OR ALTER PROCEDURE Restaurants.CreateRestaurant
   @SupplierID INT,
   @FoodName NVARCHAR(64),
   @SupplierPrice DECIMAL,
   @RetailPrice DECIMAL,
   @FoodID INT OUTPUT
AS

INSERT Food.Food(SupplierID, FoodName, SupplierPrice, RetailPrice)
VALUES(@SupplierID, @FoodName, @SupplierPrice, @RetailPrice);

SET @FoodID = SCOPE_IDENTITY();
GO
