DROP TABLE IF EXISTS Restaurants.Restaurant
CREATE TABLE Restaurants.Restaurant
(
	RestaurantID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	OrganizationID INT NOT NULL FOREIGN KEY
		REFERENCES Restaurants.Organization(OrganizationID),
	RestaurantName NVARCHAR(128) NOT NULL,
	DateFounded DATETIMEOFFSET NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
	IsOperational BIT NOT NULL,
)