CREATE OR ALTER PROCEDURE Food.UpdateFood
	@FoodID INT,
	@SupplierID INT,
	@FoodName NVARCHAR(64),
	@SupplierPrice DECIMAL,
	@RetailPrice DECIMAL

AS

UPDATE Food.Food
SET
	SupplierID = @SupplierID,
	FoodName = @FoodName,
	SupplierPrice = @SupplierPrice,
	RetailPrice = @RetailPrice
WHERE FoodID = @FoodID


	