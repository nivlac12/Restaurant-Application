CREATE SCHEMA Inventory;

CREATE TABLE Inventory.StockItems
(
	InventoryID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	FoodID INT NOT NULL FOREIGN KEY
		REFERENCES Food.Food(FoodID),
	RestaurantID INT NOT NULL FOREIGN KEY
		REFERENCES Restaurants.Restaurant(RestaurantID),
	Quantity INT NOT NULL,

	UNIQUE
	(
		FoodID,
		RestaurantID
	)
)