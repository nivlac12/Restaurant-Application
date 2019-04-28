CREATE OR ALTER PROCEDURE Food.CreateFood
   @SupplierID INT,
   @FoodName NVARCHAR(64),
   @SupplierPrice FLOAT,
   @RetailPrice FLOAT,
   @FoodID INT OUTPUT
AS

INSERT Food.Food(SupplierID, FoodName, SupplierPrice, RetailPrice)
VALUES(@SupplierID, @FoodName, @SupplierPrice, @RetailPrice);

SET @FoodID = SCOPE_IDENTITY();
GO
