use AHD09_AMS35_Common

CREATE TABLE tbl_hyundai_1334712_roles(
role_code INT PRIMARY KEY,
role_name VARCHAR(15) NOT NULL)

SELECT * FROM tbl_hyundai_1334712_roles

INSERT INTO tbl_hyundai_1334712_roles VALUES(2,'User')

CREATE TABLE tbl_hyundai_prod_category(
ProductCategoryID INT PRIMARY KEY,
ProductCategoryName VARCHAR(15) NOT NULL)

SELECT * FROM tbl_hyundai_prod_category

INSERT INTO tbl_hyundai_prod_category VALUES(1,'Hyundai')
INSERT INTO tbl_hyundai_prod_category VALUES(2,'Maruti')
INSERT INTO tbl_hyundai_prod_category VALUES(3,'Audi')
INSERT INTO tbl_hyundai_prod_category VALUES(4,'BMW')

CREATE TABLE tbl_hyundai_login_master(
LoginID INT IDENTITY(1111,17) PRIMARY KEY,
UserName VARCHAR(30) NOT NULL,
Password VARCHAR(30) NOT NULL,
UserRoleID INT CONSTRAINT FK_login REFERENCES tbl_hyundai_1334712_roles(role_code))

SELECT * FROM tbl_hyundai_login_master

INSERT INTO tbl_hyundai_login_master(UserName,Password,UserRoleID)
VALUES('admin','admin',1)
INSERT INTO tbl_hyundai_login_master(UserName,Password,UserRoleID)
VALUES('user','user',2)

CREATE TABLE tbl_hyundai_product_1334712(
ProductID INT IDENTITY(100,1) PRIMARY KEY,
ProductName VARCHAR(40) UNIQUE NOT NULL,
ProductCategoryID INT REFERENCES tbl_hyundai_prod_category(ProductCategoryID),
UnitPrice NUMERIC(8,2) NOT NULL,
TotalNumberAvailable INT NOT NULL,
Description VARCHAR(255))

--Stored Procedures
ALTER PROC usp_save_tbl_hyundai_product_1334712
@ProductID INT ,
@ProductName VARCHAR(40),
@ProductCategoryID INT,
@UnitPrice NUMERIC(8,2),
@TotalNumberAvailable INT,
@Description VARCHAR(255),
@ID_out INT OUT
AS
IF @ProductID=0
BEGIN
INSERT INTO tbl_hyundai_product_1334712(ProductName,
ProductCategoryID,
UnitPrice,
TotalNumberAvailable,
Description)
VALUES(@ProductName,
@ProductCategoryID,
@UnitPrice,
@TotalNumberAvailable,
@Description)

SET @ID_out=@ProductID
END
ELSE
BEGIN
UPDATE tbl_hyundai_product_1334712
SET ProductName= @ProductName,
ProductCategoryID=@ProductCategoryID,
UnitPrice=@UnitPrice,
TotalNumberAvailable=@TotalNumberAvailable,
Description=@Description

SELECT @ID_out=@ProductID
END

CREATE PROC usp_view_tbl_hyundai_product_1334712
AS
BEGIN
SELECT        ProductID, ProductName, ProductCategoryID, UnitPrice, TotalNumberAvailable, Description
FROM            tbl_hyundai_product_1334712
END

CREATE PROC usp_delete_tbl_hyundai_product_1334712
@ProductID INT
AS
BEGIN
DELETE FROM tbl_hyundai_product_1334712
WHERE ProductID=@ProductID
END

CREATE PROC usp_viewbyid_tbl_hyundai_product_1334712
@ProductID INT
AS
BEGIN
SELECT        ProductID, ProductName, ProductCategoryID, UnitPrice, TotalNumberAvailable, Description
FROM            tbl_hyundai_product_1334712
WHERE ProductID=@ProductID
END

