CREATE OR ALTER PROCEDURE Supplier.GetSuppliers
   @SupplierID INT
AS

SELECT S.[Name]
FROM Supplier.Suppliers S
WHERE S.SupplierID = @SupplierID;
GO