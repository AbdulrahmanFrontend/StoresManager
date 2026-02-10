CREATE DATABASE StoresManagerDB;
USE StoresManagerDB;
CREATE TABLE [Users] (
  [UserID] int NOT NULL IDENTITY(1, 1),
  [UserName] nvarchar(500) NOT NULL,
  [PasswordHash] nvarchar(50) NOT NULL,
  [Permissions] int NOT NULL,
  [IsActive] bit NOT NULL
)
GO

ALTER TABLE Users
ADD CONSTRAINT PK_UserID PRIMARY KEY (UserID);

--------------------------------

-- Optional
ALTER TABLE Users
ADD CONSTRAINT CK_UserNameLength CHECK (LEN(UserName) >= 2);

ALTER TABLE Users
ADD CONSTRAINT CK_PasswordLength CHECK (LEN(PasswordHash) >= 3);

CREATE INDEX IDX_Name
ON Users(UserName);

--------------------------------

ALTER TABLE Users
ADD CONSTRAINT UQ_UserName UNIQUE (UserName);

ALTER TABLE Users
ADD CONSTRAINT DF_ActiveUser DEFAULT (1) FOR IsActive;



CREATE TABLE [Stores] (
  [StoreID] int NOT NULL IDENTITY(1, 1),
  [Store] nvarchar(500) NOT NULL,
  [Description] nvarchar(max),
  [IsActive] bit NOT NULL
)
GO

ALTER TABLE Stores
ADD CONSTRAINT PK_StoreID PRIMARY KEY (StoreID);

ALTER TABLE Stores
ADD CONSTRAINT UQ_Store UNIQUE (Store);

-----------------------------------------------

-- Optional
ALTER TABLE Stores
ADD CONSTRAINT CK_StoreNameLength CHECK (LEN(Store) >= 2);

-----------------------------------------------

ALTER TABLE Stores
ADD CONSTRAINT DF_ActiveStore DEFAULT (1) FOR IsActive;

CREATE TABLE [Categories] (
  [CategoryID] int NOT NULL IDENTITY(100, 1),
  [Category] nvarchar(500) NOT NULL,
  [UnitID] int NOT NULL,
  [CreatedBy] int NOT NULL,
  [CreatedIn] datetime NOT NULL,
  [IsActive] bit NOT NULL
)
GO

ALTER TABLE Categories
ADD CONSTRAINT PK_CategoryID PRIMARY KEY (CategoryID);

ALTER TABLE Categories
ADD CONSTRAINT FK_CreatedBy_Categories_Users FOREIGN KEY (CreatedBy)
REFERENCES Users(UserID);

ALTER TABLE Categories
ADD CONSTRAINT FK_UnitID_Categories_Units FOREIGN KEY (UnitID) REFERENCES Units(UnitID);

--------------------------

-- Optional
ALTER TABLE Categories
ADD CONSTRAINT CK_CategoryNameLength CHECK (LEN(Category) >= 2);

ALTER TABLE Categories
ADD CONSTRAINT CK_CreatedCategoryNow CHECK (CreatedIn = GETDATE());

--------------------------

ALTER TABLE Categories
ADD CONSTRAINT DF_CreatCategoryNow DEFAULT (GETDATE()) FOR CreatedIn;

ALTER TABLE Categories
ADD CONSTRAINT DF_ActiveCategory DEFAULT (1) FOR IsActive;


CREATE TABLE [Transactions] (
  [TransactionID] int NOT NULL IDENTITY(1, 1),
  [CreatedIn] datetime NOT NULL,
  [CategoryID] int NOT NULL,
  [Quantity] int NOT NULL,
  [UnitID] int NOT NULL,
  [FromStore] int,
  [ToStore] int,
  [CreatedBy] int NOT NULL,
  [TypeID] int NOT NULL,
  [IsCancelled] bit NOT NULL
)
GO

ALTER TABLE Transactions
ADD CONSTRAINT PK_TransactionID PRIMARY KEY (TransactionID);

ALTER TABLE Transactions
ADD CONSTRAINT DF_CreatedTransactionNow DEFAULT (GETDATE()) FOR CreatedIn;

ALTER TABLE Transactions
ADD CONSTRAINT FK_CategoryID_Transactions_Categories FOREIGN KEY (CategoryID) 
REFERENCES Categories(CategoryID);

ALTER TABLE Transactions
ADD CONSTRAINT FK_UnitID_Transactions_Categories FOREIGN KEY (UnitID)
REFERENCES Units(UnitID);

ALTER TABLE Transactions
ADD CONSTRAINT DF_QuantityEqualZero DEFAULT (0) FOR Quantity;

ALTER TABLE Transactions
ADD CONSTRAINT CK_PositiveQuantity CHECK (Quantity >= 0);

ALTER TABLE Transactions
ADD CONSTRAINT FK_FromStore_Transactions_Stores FOREIGN KEY (FromStore)
REFERENCES Stores(StoreID);

ALTER TABLE Transactions
ADD CONSTRAINT FK_ToStore_Transactions_Stores FOREIGN KEY (ToStore)
REFERENCES Stores(StoreID);

ALTER TABLE Transactions
ADD CONSTRAINT FK_CreatedBy_Transactions_Users FOREIGN KEY (CreatedBy)
REFERENCES Users(UserID);

ALTER TABLE Transactions
ADD CONSTRAINT FK_TypeID_Transactions_Users FOREIGN KEY (TypeID)
REFERENCES TransactionTypes(TypeID);

ALTER TABLE Transactions
ADD CONSTRAINT DF_NotCancelled DEFAULT (0) FOR IsCancelled;

-- Optional
CREATE INDEX IDX_CreatedIn
ON Transactions(CreatedIn);

CREATE INDEX IDX_StorID
ON Transactions(FromStore, ToStore);

CREATE INDEX IDX_CreatedBy
ON Transactions(CreatedBy);

CREATE INDEX IDX_TypeID
ON Transactions(TypeID);

CREATE TABLE [Units] (
  [UnitID] int NOT NULL IDENTITY(1, 1),
  [Unit] nvarchar(500) NOT NULL
)
GO

ALTER TABLE Units
ADD CONSTRAINT PK_UnitID PRIMARY KEY (UnitID);

ALTER TABLE Units
ADD CONSTRAINT UQ_Unit UNIQUE (Unit);

-- Optional
ALTER TABLE Units
ADD CONSTRAINT CK_UnitNameLength CHECK (LEN(Unit) >= 2);

CREATE INDEX IDX_Unit
ON Units(Unit);

CREATE TABLE [TransactionTypes] (
  [TypeID] int NOT NULL IDENTITY(1, 1),
  [TransactionType] nvarchar(500) NOT NULL
)
GO

ALTER TABLE TransactionTypes
ADD CONSTRAINT PK_TypeID PRIMARY KEY (TypeID);

ALTER TABLE TransactionTypes
ADD CONSTRAINT UQ_Type UNIQUE (TransactionType);

-- Optional
ALTER TABLE TransactionTypes
ADD CONSTRAINT CK_TypeNameLength CHECK (LEN(TransactionType) >= 2);

CREATE INDEX IDX_TransactionType
ON TransactionTypes(TransactionType);

CREATE TABLE [StoreStocks] (
  [StoreStockID] int NOT NULL IDENTITY(1, 1),
  [StoreID] int NOT NULL,
  [CategoryID] int NOT NULL,
  [Quantity] int NOT NULL
)
GO

ALTER TABLE StoreStocks
ADD CONSTRAINT PK_StoreStockID PRIMARY KEY (StoreStockID);

ALTER TABLE StoreStocks
ADD CONSTRAINT FK_StoreID_StoreStocks_Stores FOREIGN KEY (StoreID) REFERENCES Stores(StoreID);

ALTER TABLE StoreStocks
ADD CONSTRAINT FK_CategoryID_StoreStocks_Categories FOREIGN KEY (CategoryID)
REFERENCES Categories(CategoryID);

ALTER TABLE StoreStocks
ADD CONSTRAINT DF_Quantity_0 DEFAULT (0) FOR Quantity;

ALTER TABLE StoreStocks
ADD CONSTRAINT CK_QuantityLargerThanOrEqualZero CHECK (Quantity >= 0);

-- Optional
CREATE INDEX IDX_StoreStocks
ON StoreStocks(StoreID, CategoryID);

CREATE VIEW CategoryDetails AS
SELECT Categories.Category, Categories.CategoryID, Units.Unit, Categories.CreatedBy,
Categories.CreatedIn, Categories.IsActive
FROM Categories JOIN Units ON Categories.UnitID = Units.Unit;

CREATE VIEW TransactionDetails AS
SELECT Transactions.TransactionID, Transactions.CategoryID, Transactions.Quantity,
Units.Unit, Transactions.FromStore, Transactions.ToStore, Users.UserName AS CreatedByUser,
Transactions.CreatedIn, TransactionTypes.TransactionType, Transactions.IsCancelled
FROM Transactions
JOIN Units ON Transactions.UnitID = Units.UnitID
JOIN Users ON Transactions.CreatedBy = Users.UserID
JOIN TransactionTypes ON Transactions.TypeID = TransactionTypes.TypeID;

BACKUP DATABASE StoresManagerDB
TO DISK = 'D:\Abdulrahman\19 - Full Real Project\StoresManager\Database\StoresManagerDB.bak'
WITH DIFFERENTIAL;