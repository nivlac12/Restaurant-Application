﻿CREATE OR ALTER PROCEDURE Food.Food
   @OFoodID INT
AS

SELECT F.FoodID, F.SupplierID, F.[FoodName], F.SupplierPrice, F.RetailPrice
FROM Food.Food F
WHERE F.FoodID = @FoodID;
GO