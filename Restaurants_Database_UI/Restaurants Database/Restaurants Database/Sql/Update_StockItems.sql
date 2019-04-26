CREATE OR ALTER PROCEDURE Inventory.UpdateStockItems
	@InventoryID INT,
    @FoodID INT,
	@RestaurantID INT,
	@Quantity INT

AS

UPDATE Inventory.StockItems
SET
	@FoodID = @FoodID,
	@RestaurantID = @RestaurantID,
	@Quantity = @Quantity
WHERE InventoryID = @InventoryID