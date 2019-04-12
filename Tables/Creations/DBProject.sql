CREATE SCHEMA Food;
CREATE SCHEMA Supplier;
CREATE SCHEMA Restaurants;
CREATE SCHEMA Inventory;
CREATE SCHEMA Employees;

CREATE TABLE Supplier.Suppliers
(
	SupplierID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(64) 
)

CREATE TABLE Food.Food
(
	FoodID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	SupplierID INT NOT NULL FOREIGN KEY
		REFERENCES Supplier.Suppliers(SupplierID),
	[FoodName] NVARCHAR(64) NOT NULL,
	SupplierPrice DECIMAL NOT NULL,
	RetailPrice DECIMAL NOT NULL
)

CREATE TABLE Restaurants.Organization
(
	OrganizationID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	OrganizationName NVARCHAR(128) NOT NULL,
	DateFounded DATETIMEOFFSET NOT NULL DEFAULT (SYSDATETIMEOFFSET())
)

CREATE TABLE Restaurants.Restaurant
(
	RestaurantID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	OrganizationID INT NOT NULL FOREIGN KEY
		REFERENCES Restaurants.Organization(OrganizationID),
	RestaurantName NVARCHAR(128) NOT NULL,
	DateFounded DATETIMEOFFSET NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
	IsOperational BIT NOT NULL,
)

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

CREATE TABLE Employees.Jobs
(
	JobTitleID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	JobName NVARCHAR(64) NOT NULL,
	Salary DECIMAL NOT NULL
)

CREATE TABLE Employees.Employee
(
	PersonID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	RestaurantID INT NOT NULL FOREIGN KEY
		REFERENCES Restaurants.Restaurant(RestaurantID),
	JobTitleID INT NOT NULL FOREIGN KEY
		REFERENCES Employees.Jobs(JobTitleID),
	[Name] NVARCHAR(128) NOT NULL,
	Seniority NVARCHAR(128) NOT NULL
)