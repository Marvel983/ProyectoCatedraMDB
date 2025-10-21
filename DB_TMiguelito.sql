CREATE DATABASE Tienda;
GO
USE Tienda;
GO

CREATE TABLE Proveedores (
    NombreProveedor NVARCHAR(100) CONSTRAINT PK_Proveedores PRIMARY KEY,
    Contacto NVARCHAR(100)
);

CREATE TABLE Categoria (
    IdCategoria INT CONSTRAINT PK_Categoria PRIMARY KEY,
    NombreCategoria NVARCHAR(60)
);

CREATE TABLE Productos (
    IdProducto INT IDENTITY(1,1) CONSTRAINT PK_Productos PRIMARY KEY,
    NombreProducto NVARCHAR(120),
    DescripciónProd NVARCHAR(200) NOT NULL,
    PrecioProd DECIMAL(10,2) NOT NULL CONSTRAINT CK_PrecioProd CHECK (PrecioProd >= 0),
    IdCategoria INT CONSTRAINT FK_IdCategoria FOREIGN KEY REFERENCES Categoria(IdCategoria)
);

CREATE TABLE Stock (
    IdStock INT IDENTITY(1,1) CONSTRAINT PK_Stock PRIMARY KEY,
    IdProducto INT CONSTRAINT FK_StockProducto UNIQUE FOREIGN KEY REFERENCES Productos(IdProducto),
    CantidadActual INT NOT NULL CONSTRAINT CK_CantidadActual CHECK (CantidadActual >= 0),
    FechaActualizacion DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) CONSTRAINT PK_Usuarios PRIMARY KEY,
    NombreUsuario NVARCHAR(50) UNIQUE NOT NULL,
    CargoUsuario NVARCHAR(50),
    Contrasena NVARCHAR(256) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE IngresoProductos (
    IdIngreso INT IDENTITY(1,1) CONSTRAINT PK_IngresoProductos PRIMARY KEY,
    IdProducto INT NOT NULL CONSTRAINT FK_IngresoProducto FOREIGN KEY REFERENCES Productos(IdProducto),
    NombreProveedor NVARCHAR(100) NOT NULL CONSTRAINT FK_IngresoProveedor FOREIGN KEY REFERENCES Proveedores(NombreProveedor), 
    CantidadIngreso INT NOT NULL CONSTRAINT CK_CantidadIngreso CHECK (CantidadIngreso > 0),
    IdUsuario INT NOT NULL CONSTRAINT FK_IngresoUsuario FOREIGN KEY REFERENCES Usuarios(IdUsuario),
    FechaIngreso DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE AuditoriaPrecioProductos (
    IdAuditoria INT IDENTITY(1,1) PRIMARY KEY,
    IdProducto INT NOT NULL 
        CONSTRAINT FK_AuditoriaProducto FOREIGN KEY REFERENCES Productos(IdProducto), 
    NombreProducto NVARCHAR(120),
    PrecioAnterior DECIMAL(10,2) NOT NULL,
    PrecioNuevo DECIMAL(10,2) NOT NULL,
    FechaCambio DATETIME DEFAULT GETDATE(),
    UsuarioCambio NVARCHAR(100) DEFAULT SUSER_SNAME()
);

/*DROP TABLE IngresoProductos;
DROP TABLE AuditoriaPrecioProductos;
DROP TABLE Stock;
DROP TABLE Productos;
DROP TABLE Usuarios;
DROP TABLE Categoria;
DROP TABLE Proveedores;
*/

INSERT INTO Proveedores (NombreProveedor, Contacto) VALUES
('Bebidas la Constancia S.A', 'Juan Pérez - 7555-1001'),
('Panaderia El Buen Sabor', 'María López - 7980-0121'),
('Distribuidora Diana', 'Mariana Sosa- 2250-9988'),
('Distribuidora Lourdes','Marcos Roldan- 2240-2525'),
('Lácteos San Julián', 'Carlos Menjívar - 7722-3344'),
('Frutas Tropicales S.A. de C.V.', 'Ana Ramírez - 7890-1122');
GO

INSERT INTO Categoria (IdCategoria, NombreCategoria) VALUES
(1, 'Bebidas'), (2, 'Pan dulce y Repostería'), (3, 'Snacks'), (4, 'Granos Básicos'),
(5, 'Verduras y frutas'), (6, 'Lácteos'), (7, 'Carnes y Embutidos'), (8, 'Abarrotes');

INSERT INTO Productos (NombreProducto, DescripciónProd, PrecioProd, IdCategoria) VALUES
('Salva-Cola 3l', 'Bebida carbonatada', 2.50, 1), ('Jugo de Naranja 1l', 'Bebida refrescante', 1.30, 1),
('Agua Cristal 1.5l', 'Agua purificada embotellada', 0.60, 1), ('Refresco Kolashanpan 2l', 'Bebida gaseosa sabor original', 1.10, 1),
('Semita alta', 'Porción individual de pan dulce', 0.75, 2), ('Quesadilla Salvadoreña', 'Pan dulce tradicional con queso', 1.50, 2),
('Empanada de plátano', 'Plátano relleno de leche', 0.80, 2), ('Nachos Diana', 'Paquete de nachos con queso', 0.25, 3),
('Churritos Picantes', 'Botana de maíz con chile', 0.35, 3), ('Tortillitas con sal', 'Tortillas crujientes saladas', 0.30, 3),
('Arroz Blanco', 'Libra de arroz precocido', 0.90, 4), ('Frijol Rojo de Seda', 'Libra de frijol rojo', 1.10, 4),
('Maíz Blanco', 'Libra de maíz para tortillas', 0.85, 4), ('Papaya', 'Fruta fresca', 1.25, 5),
('Tomate', 'Libra de tomate fresco', 0.60, 5), ('Banano', 'Unidad de banano maduro', 0.20, 5),
('Leche Entera 1l', 'Leche pasteurizada entera', 1.00, 6), ('Queso Fresco', 'Queso artesanal por libra', 2.50, 6),
('Chorizo Criollo', 'Paquete de chorizo artesanal', 3.00, 7), ('Pollo entero', 'Pollo fresco entero', 5.50, 7),
('Sal yodada', 'Bolsa de sal de mesa', 0.40, 8), ('Aceite vegetal 500ml', 'Aceite comestible', 1.20, 8);

INSERT INTO Stock (IdProducto, CantidadActual) VALUES
(1, 75), (2, 20), (3, 50), (4, 25), (5, 10), (6, 5), (7, 15), (8, 30), (9, 25),
(10, 35), (11, 25), (12, 25), (13, 20), (14, 15), (15, 30), (16, 25), (17, 25),
(18, 10), (19, 20), (20, 8), (21, 35), (22, 28);
GO

INSERT INTO Usuarios (NombreUsuario, CargoUsuario, Contrasena, Activo) VALUES
('admin.tienda', 'Administrador', 'Admin1234#_hash', 1),
('ana.ventas', 'Empleado', 'Empleado1234#_hash', 1);
GO

CREATE TRIGGER TRG_ActualizarStockPorIngreso
ON IngresoProductos
AFTER INSERT
AS
BEGIN
    UPDATE S
    SET S.CantidadActual = S.CantidadActual + I.CantidadIngreso,
        S.FechaActualizacion = GETDATE()
    FROM Stock S
    INNER JOIN inserted I ON S.IdProducto = I.IdProducto;
END;
GO

CREATE TRIGGER TRG_EliminarReferenciasProducto
ON Productos
INSTEAD OF DELETE
AS
BEGIN
    DELETE FROM IngresoProductos
    WHERE IdProducto IN (SELECT IdProducto FROM deleted);

    DELETE FROM Stock
    WHERE IdProducto IN (SELECT IdProducto FROM deleted);

    DELETE FROM Productos
    WHERE IdProducto IN (SELECT IdProducto FROM deleted);
END;
GO

CREATE TRIGGER TRG_AuditPrecioProducto
ON Productos
AFTER UPDATE
AS
BEGIN
    IF UPDATE(PrecioProd)
    BEGIN
        INSERT INTO AuditoriaPrecioProductos (
            IdProducto, NombreProducto, PrecioAnterior, PrecioNuevo
        )
        SELECT
            i.IdProducto, i.NombreProducto, d.PrecioProd, i.PrecioProd
        FROM inserted i
        INNER JOIN deleted d ON i.IdProducto = d.IdProducto
        WHERE i.PrecioProd <> d.PrecioProd;
    END
END;
GO

CREATE LOGIN admin1 WITH PASSWORD = 'Admin1234#';
CREATE USER admin1 FOR LOGIN admin1;
EXEC sp_addrolemember 'db_datareader', 'admin1';
EXEC sp_addrolemember 'db_datawriter', 'admin1';

CREATE LOGIN empleado WITH PASSWORD = 'Empleado1234#';
CREATE USER empleado FOR LOGIN empleado;
EXEC sp_addrolemember 'db_datareader', 'empleado';
EXEC sp_addrolemember 'db_datawriter', 'empleado';
GO