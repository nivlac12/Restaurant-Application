CREATE OR ALTER PROCEDURE Restaurants.CalcRestExp
@RestaurantID INT
AS
SELECT 
(
	SELECT SUM(SI.Quantity*F.RetailPrice) AS Expense
	FROM Restaurants.Restaurant R
		INNER JOIN Inventory.StockItems SI ON SI.RestaurantID = R.RestaurantID  AND R.IsOperational = 1
		INNER JOIN Food.Food F ON F.FoodID = SI.FoodID
	WHERE R.RestaurantID = @RestaurantID AND R.IsOperational = 1
	GROUP BY R.RestaurantID
) +
(
	SELECT SUM(J.Salary*40) AS Expense
	FROM Restaurants.Restaurant R
		INNER JOIN Employees.Employee E ON R.RestaurantID = E.RestaurantID  AND R.IsOperational = 1
		INNER JOIN Employees.Jobs J ON J.JobTitleID = E.JobTitleID
	WHERE R.RestaurantID = @RestaurantID
	GROUP BY R.RestaurantID
) AS RestExpense
GO


CREATE OR ALTER PROCEDURE Restaurants.CalcOrgExp
@OrganizationID INT
AS
SELECT 
(
	SELECT SUM(SI.Quantity*F.RetailPrice) AS Expense
	FROM Restaurants.Organization O
		INNER JOIN Restaurants.Restaurant R ON O.OrganizationID = R.OrganizationID AND R.IsOperational = 1
		INNER JOIN Inventory.StockItems SI ON SI.RestaurantID = R.RestaurantID
		INNER JOIN Food.Food F ON F.FoodID = SI.FoodID
	WHERE O.OrganizationID = @OrganizationID
	GROUP BY O.OrganizationID
) +
(
	SELECT SUM(J.Salary*40) AS Expense
	FROM Restaurants.Organization O
		INNER JOIN Restaurants.Restaurant R ON O.OrganizationID = R.OrganizationID AND R.IsOperational = 1
		INNER JOIN Employees.Employee E ON R.RestaurantID = E.RestaurantID
		INNER JOIN Employees.Jobs J ON J.JobTitleID = E.JobTitleID
	WHERE O.OrganizationID = @OrganizationID
	GROUP BY O.OrganizationID
) AS OrgExpense
GO


CREATE OR ALTER PROCEDURE Supplier.CalcSuppProfits
@SupplierID INT
AS
SELECT SUM((F.RetailPrice - F.SupplierPrice)*SI.Quantity) AS Profit
FROM Supplier.Suppliers S
	INNER JOIN Food.Food F ON F.SupplierID = S.SupplierID
	INNER JOIN Inventory.StockItems SI ON SI.FoodID = F.FoodID
WHERE S.SupplierID = @SupplierID
GROUP BY S.SupplierID
GO


CREATE OR ALTER PROCEDURE Restaurants.GetEmployeeInfo
@RestaurantID INT
AS
SELECT E.Name, J.JobName, J.Salary, E.Seniority
FROM Restaurants.Restaurant R
	INNER JOIN Employees.Employee E ON E.RestaurantID = R.RestaurantID AND R.IsOperational = 1
	INNER JOIN Employees.Jobs J ON J.JobTitleID = E.JobTitleID
WHERE R.RestaurantID = @RestaurantID
GO
