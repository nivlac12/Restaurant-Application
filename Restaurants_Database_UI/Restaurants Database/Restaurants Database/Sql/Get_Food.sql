﻿CREATE OR ALTER PROCEDURE Food.GetFood
   @FoodName NVARCHAR(128)
AS

SELECT F.FoodID, F.SupplierID, F.SupplierPrice, F.RetailPrice
FROM Food.Food F
WHERE F.FoodName = @FoodName;
GO