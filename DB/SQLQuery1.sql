-- CREATION OF THE DATBASE
CREATE DATABASE AMPMTest
GO

-- ACTIVE DB
USE AMPMTest
GO

-- PRODUCT TABLE
CREATE TABLE Productos(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Codigo VARCHAR(50) NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	Existencias VARCHAR(50) NOT NULL
);
GO

-- ACTIVE PRODUCTS TABLE
CREATE TABLE ProductosActivos(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Producto_Id INT NOT NULL,
	Opcion VARCHAR(50),
	CONSTRAINT fk_ProductosActivos_Producto_Id FOREIGN KEY(Producto_Id) REFERENCES Productos(Id)
);
GO

-- INACTIVE PRODUCTS TABLE
CREATE TABLE ProductosInactivos(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Producto_Id INT NOT NULL,
	Opcion VARCHAR(50),
	CONSTRAINT fk_ProductosInactivos_Producto_Id FOREIGN KEY(Producto_Id) REFERENCES Productos(Id)
);
GO

-- USERS TABLE
CREATE TABLE Usuarios(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Usuario VARCHAR(50),
	Contrasena VARBINARY(225),
	Nombre VARCHAR(50),
	Apellido VARCHAR(50),
	Correo VARCHAR(50),
	Telefono VARCHAR(50),
	Creado_en DATETIMEOFFSET DEFAULT SYSDATETIMEOFFSET()
);
GO

/* ================================================================================= */
/* ==================================== INSERTS ==================================== */
/* ================================================================================= */

-- PRODUCTS INSERTS
INSERT INTO Productos(Codigo, Nombre, Existencias)
VALUES 
('RF001', 'Coca-Cola 600ml', '35'),
('RF002', 'Pepsi 600ml', '25'),
('GL001', 'Galletas Choco Chips', '40'),
('GL002', 'Snickers', '20'),
('GL003', 'M&M Chocolate', '30'),
('RF003', 'Agua Pura 1L', '50'),
('RF004', 'Powerade Azul 500ml', '15'),
('GL004', 'Chicles Clorets', '100'),
('GL005', 'Doritos Queso 45g', '18'),
('RF005', 'Red Bull 250ml', '10');
GO

-- ACTIVE PRODUCTS INSERT
INSERT INTO ProductosActivos(Producto_Id, Opcion)
VALUES 
(1, 'Descartable'), 
(2, 'Descartable'), 
(3, 'Unidades'), 
(4, 'Unidades'), 
(5, 'Unidades'), 
(6, 'Descartable'), 
(7, 'Descartable');
GO

-- INACTIVE PRODUCTS INSERT
INSERT INTO ProductosInactivos(Producto_Id, Opcion)
VALUES 
(8, 'Unidades'), 
(9, 'Promocion 2x1'), 
(10, 'Lata');
GO

-- DECLARE VARIABLES PHRASE AND PASSWORD BY PHRASE ENCRYPT

DECLARE @SecretPhrase VARCHAR(100) = 'HalaMadrid';
DECLARE @Passwd NVARCHAR(50) = 'HIRED';

-- INSERT USER
INSERT INTO Usuarios(Usuario, Contrasena, Nombre, Apellido, Correo, Telefono)
VALUES (
    'Jade',
    EncryptByPassPhrase(@SecretPhrase, @Passwd),
    'Carlos',
    'Ulloa',
    'carlos.ulloa@ampm.com.ni',
    '88638578'
);


DECLARE @SecretPhrase VARCHAR(100) = 'HalaMadrid';
DECLARE @Passwd NVARCHAR(50) = 'HIRED';
DECLARE @usuario NVARCHAR(50) = 'Jade';

SELECT 
    Usuario,
    CONVERT(NVARCHAR(100), DECRYPTBYPASSPHRASE(@SecretPhrase, Contrasena)) AS DecryptedPassword
FROM Usuarios WHERE Usuario = @usuario;

select * from Usuarios

DELETE FROM Usuarios WHERE Id IN (2, 3);


select * from Productos
select * from ProductosActivos
select * from ProductosInactivos

-- active product
SELECT
	p.Codigo AS Codigo,
	p.Nombre AS Nombre,
	p.Existencias AS Existencias,
	pa.Opcion AS Opcion
FROM ProductosActivos pa
INNER JOIN Productos p ON pa.Producto_Id = p.Id 

-- inactive product
SELECT
	p.Codigo AS Codigo,
	p.Nombre AS Nombre,
	p.Existencias AS Existencias,
	pii.Opcion AS Opcion
FROM ProductosInactivos pii
INNER JOIN Productos p ON pii.Producto_Id = p.Id 

