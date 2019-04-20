CREATE OR ALTER PROCEDURE Restaurants.CreateRestaurant
   @Name NVARCHAR(64),
   @SupplierID INT OUTPUT
AS

INSERT Supplier.Suppliers(Name)
VALUES(@Name);

SET @SupplierID = SCOPE_IDENTITY();
GO
