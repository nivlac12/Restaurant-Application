CREATE OR ALTER PROCEDURE Supplier.UpdateSuppliers
  @SupplierID INT,
  @Name NVARCHAR(64) 

AS

UPDATE Supplier.Suppliers
SET
	Name = @Name
WHERE SupplierID = @SupplierID