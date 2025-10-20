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
    IdProducto INT CONSTRAINT PK_IdProducto PRIMARY KEY,
    CantidadActual INT NOT NULL CONSTRAINT CK_CantidadActual CHECK (CantidadActual >= 0),
    CONSTRAINT FK_StockProducto FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);

INSERT INTO Proveedores (NombreProveedor, Contacto) VALUES
('Bebidas la Constancia S.A', 'Juan Pérez - 7555-1001'),
('Panaderia El Buen Sabor', 'María López - 7980-0121'),
('Distribuidora Diana', 'Mariana Sosa- 2250-9988'),
('Distribuidora Lourdes','Marcos Roldan- 2240-2525'),
('Lácteos San Julián', 'Carlos Menjívar - 7722-3344'),
('Frutas Tropicales S.A. de C.V.', 'Ana Ramírez - 7890-1122');
GO

INSERT INTO Categoria (IdCategoria, NombreCategoria) VALUES
(1, 'Bebidas'),
(2, 'Pan dulce y Repostería'),
(3, 'Snacks'),
(4, 'Granos Básicos'),
(5, 'Verduras y frutas'),
(6, 'Lácteos'),
(7, 'Carnes y Embutidos'),
(8, 'Abarrotes');

INSERT INTO Productos (NombreProducto, DescripciónProd, PrecioProd, IdCategoria) VALUES
('Salva-Cola 3l', 'Bebida carbonatada', 2.50, 1),
('Jugo de Naranja 1l', 'Bebida refrescante', 1.30, 1),
('Agua Cristal 1.5l', 'Agua purificada embotellada', 0.60, 1),
('Refresco Kolashanpan 2l', 'Bebida gaseosa sabor original', 1.10, 1),
('Semita alta', 'Porción individual de pan dulce', 0.75, 2),
('Quesadilla Salvadoreña', 'Pan dulce tradicional con queso', 1.50, 2),
('Empanada de plátano', 'Plátano relleno de leche', 0.80, 2),
('Nachos Diana', 'Paquete de nachos con queso', 0.25, 3),
('Churritos Picantes', 'Botana de maíz con chile', 0.35, 3),
('Tortillitas con sal', 'Tortillas crujientes saladas', 0.30, 3),
('Arroz Blanco', 'Libra de arroz precocido', 0.90, 4),
('Frijol Rojo de Seda', 'Libra de frijol rojo', 1.10, 4),
('Maíz Blanco', 'Libra de maíz para tortillas', 0.85, 4),
('Papaya', 'Fruta fresca', 1.25, 5),
('Tomate', 'Libra de tomate fresco', 0.60, 5),
('Banano', 'Unidad de banano maduro', 0.20, 5),
('Leche Entera 1l', 'Leche pasteurizada entera', 1.00, 6),
('Queso Fresco', 'Queso artesanal por libra', 2.50, 6),
('Chorizo Criollo', 'Paquete de chorizo artesanal', 3.00, 7),
('Pollo entero', 'Pollo fresco entero', 5.50, 7),
('Sal yodada', 'Bolsa de sal de mesa', 0.40, 8),
('Aceite vegetal 500ml', 'Aceite comestible', 1.20, 8);

INSERT INTO Stock (IdProducto, CantidadActual) VALUES
(1, 75),
(2, 20),
(3, 50),
(4, 25),
(5, 10),
(6, 5),  
(7, 15),  
(8, 30),  
(9, 25),  
(10, 35), 
(11, 25), 
(12, 25), 
(13, 20), 
(14, 15), 
(15, 30), 
(16, 25), 
(17, 25), 
(18, 10), 
(19, 20), 
(20, 8),  
(21, 35), 
(22, 28);
GO

select * from Stock;