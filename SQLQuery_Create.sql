Create DataBase JooleDatabase;
GO

USE JooleDatabase;
GO

Create Table Users(
	UserID int Primary Key Identity,
	UserName varchar(50) not null,
	UserEmail varchar(50) not null,
	UserImage varchar(255),
	UserPassword varchar(50) not null
	);
	GO

Create Table Manufacturers(
	ManufacturerID int Primary Key,
	ManufacturerName varchar(50) not null,
	ManufacturerWeb varchar(50),
	);
	GO

Create Table Category(
	CategoryID int Primary Key,
	CategoryName varchar(50) not null unique
	);

	GO

Create Table SubCategory(
	SubCategoryID int Primary Key,
	CategoryID int,
	SubCategoryName varchar(50) not null unique,
	Constraint FK_CategoryID Foreign Key (CategoryID) References Category(CategoryID)
	);

	GO

Create Table Property(
	PropertyID int Primary Key,
	PropertyName varchar(50) not null unique,
	IsType bit,
	IsTechSpec bit,
	);

	GO



Create Table Products(
	ProductID int Primary Key,
	ManufacturerID int,
	SubCategoryID int not null,
	ProductName varchar(50) unique not null,
	ProductImage varchar(50),
	Series varchar(50),
	Model varchar(50),
	ModelYear int,
	SeriesInfo varchar(50),
	Constraint FK_SubCategoryID Foreign Key (SubCategoryID) References SubCategory(SubCategoryID),
	Constraint FK_ManufacturerID Foreign Key (ManufacturerID) References Manufacturers(ManufacturerID)

	);

	GO



Create Table PropertyValue(
	ProductID int not null,
	PropertyID int not null,
	PropertyValue varchar(50),
	Constraint FK_ProductID Foreign Key (ProductID) References Products(ProductID),
	Constraint FK_PropertyID Foreign Key (PropertyID) References Property(PropertyID),
	Constraint CK_Product_Property PRIMARY KEY (ProductID,PropertyID)
	);

	go

Create Table TypeFilter(
	PropertyID int not null,
	SubCategoryID int not null,
	TypeName varchar(50) not null,
	Constraint CK_TypeFilter Primary Key (PropertyID, SubCategoryID),
	Constraint FK_P_TypeFilter Foreign Key (PropertyID) References Property(PropertyID),
	Constraint FK_SC_TypeFilter Foreign Key (SubCategoryID) References SubCategory(SubCategoryID)
	);
	
	GO

Create Table TechSpecFilter(
	PropertyID int not null,
	SubCategoryID int not null,
	MinValue int not null,
	MaxValue int not null,
	Constraint CK_TypeSpec Primary Key(PropertyID, SubCategoryID),
	Constraint FK_p_TypeSpec Foreign Key (PropertyID) References Property(PropertyID),
	Constraint FK_SC_TypeSpec Foreign Key (SubCategoryID) References SubCategory(SubCategoryID)
	);
	GO