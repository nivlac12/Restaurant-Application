CREATE OR ALTER PROCEDURE Supplier.GetSupplier
   @SupplierName NVARCHAR(128)
AS

SELECT S.SupplierID
FROM Supplier.Suppliers S
WHERE S.Name = @SupplierName;
GO