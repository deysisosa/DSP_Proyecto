create database inventario
go
use inventario
go

create table estado(
Id_Estado int,
Estado char(15),
constraint pk_estado primary key (Id_Estado)
)

create table Productos(
id_producto varchar(50)  not null,
Nombre_producto varchar(50), 
Existencias int,
Precio_venta float,
Precio_compra float,
Descripcion varchar(140),
id_categoria int,
N_pedido int,
id_proveedor int,
ubicacion int,
estado int,
constraint pk_producto primary key (id_producto)
);

create table Pedido(
id_pedido int identity(1,1),
Nombre_Usuario varchar(10),
fecha_corte date,
fecha_facturacion date,
constraint pk_pedido primary key (id_pedido)
);

create table detalle_productos(
id_detalle_producto int identity(1,1),
id_producto varchar(50),
Nombre_producto varchar(50),
Existencia int,
Precio_venta float,
Precio_compra float,
Descripcion varchar(140),
id_categoria int,
N_pedido int,
id_proveedor int,
constraint pk_detalle primary key (id_detalle_producto)
);

create table Tipo_Usuario(
id_tipo int,
Tipo_Usuario char(15),
constraint pk_tipo primary key (id_tipo)
)

create table Usuarios(
Nombre varchar(25),
Apellido varchar(25),
Nombre_Usuario varchar(10),
Contraseña varchar(50),
TipoU int,
constraint pk_usu primary key (Nombre_Usuario)
);

create table bodega(
id_bodega int,
Nombre_Bodega varchar(25),
id_estante int,
constraint pk_bodega primary key (id_bodega)
);

create table Categoria(
id_categoria int identity (1,1),
Nombre_categoria varchar(50),
constraint pk_categoria primary key (id_categoria)
);

create table estante(
id_estante int,
n_estante char(15),
id_categoria int,
constraint pk_estante primary key (id_estante)
);

create table Proveedores(
id_proveedor int identity (1,1),
Nombre_Proveedor varchar(140),
Nombre_Contacto varchar(140),
Pais varchar(75),
telefono1 varchar (50),
telefono2 varchar(50),
correo1 varchar (100),
correo2 varchar(100),
constraint pk_prov primary key (id_proveedor)
);
go

--RELACIONES
alter table productos
   add constraint FK_cate_prod foreign key (id_categoria)
      references categoria (id_categoria);

alter table detalle_productos
   add constraint FK_id_det_prod foreign key (N_Pedido)
      references pedido (id_pedido);

alter table detalle_productos
   add constraint FK_id_product foreign key (id_producto)
      references productos (id_producto);

 alter table productos
   add constraint FK_id_prov2 foreign key (id_proveedor)
      references Proveedores (id_proveedor);

alter table usuarios
   add constraint fk_tipo foreign key (TipoU)
      references Tipo_Usuario (id_tipo);

alter table bodega 
	 add constraint FK_Bodega foreign key (id_estante)
      references estante (id_estante);

alter table pedido 
	 add constraint FK_pedido foreign key (Nombre_Usuario)
      references Usuarios (Nombre_Usuario);

alter table Productos 
	 add constraint FK_ubicacion foreign key (Ubicacion)
      references Bodega (id_bodega);

alter table estante 
	 add constraint FK_estante_cate foreign key (id_categoria)
      references categoria (id_categoria);

alter table Productos 
	 add constraint FK_estado foreign key (estado)
      references estado (Id_Estado);
go

------------PROCEDIMIENTOS PARA INGRESAR DATOS
--productos
IF OBJECT_ID ( 'insertproductos' ) IS NOT NULL   
    DROP PROCEDURE insertproductos;  
GO
CREATE PROCEDURE insertproductos 
	@id_producto varchar(50),
	@Nombre_producto varchar(50),
	@Existencias int,
	@Precio float,
	@Precio2 float,
	@Descripcion varchar(140),
	@id_categoria int,
	@NPedido int, 
	@id_proveedor int,
	@Ubicacion int,
	@estado int
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO productos values (@id_producto,@Nombre_producto,@Existencias,@Precio,@precio2,@Descripcion,@id_categoria,@NPedido,@id_proveedor,@Ubicacion,@estado)
END
GO

--categoria
IF OBJECT_ID ( 'insertcategoria' ) IS NOT NULL   
    DROP PROCEDURE insertcategoria;  
GO
CREATE PROCEDURE insertcategoria
	@id_categoria varchar(50),
	@Nombre_Categoria varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO categoria values (@Nombre_Categoria)
END
GO

--Proveedores
IF OBJECT_ID ( 'insertProveedores' ) IS NOT NULL   
    DROP PROCEDURE insertProveedores;  
GO
CREATE PROCEDURE insertProveedores
	@id_proveedor int,
	@Nombre_Proveedor varchar(140),
	@Nombre_Contacto varchar(140),
	@Pais varchar(50),
	@telefono1 varchar(50),
	@telefono2 varchar(50),
	@correo1 varchar(100),
	@correo2 varchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Proveedores values (@Nombre_Proveedor,@Nombre_Contacto,@Pais,@telefono1,@telefono2,@correo1,@correo2)
END
GO



--Tipo de Usuario
INSERT INTO Tipo_Usuario(id_tipo, Tipo_Usuario)
VALUES (2,'Administrador');
INSERT INTO Tipo_Usuario(id_tipo, Tipo_Usuario)
VALUES (3,'Empleado');
--Usuario
INSERT INTO Usuarios(Nombre, Apellido, Nombre_Usuario, Contraseña, TipoU)
VALUES('Juan', 'García','JuanGarcia','1554', 3);
INSERT INTO Usuarios(Nombre, Apellido, Nombre_Usuario, Contraseña, TipoU)
VALUES('Veronica', 'Fuentes','VeroFuen','9684', 2);
INSERT INTO Usuarios(Nombre, Apellido, Nombre_Usuario, Contraseña, TipoU)
VALUES('Luis', 'Castillo','LuisCasti','5160', 3);
INSERT INTO Usuarios(Nombre, Apellido, Nombre_Usuario, Contraseña, TipoU)
VALUES('Miguel', 'Hernandez','MiguelHer','9678', 3);
INSERT INTO Usuarios(Nombre, Apellido, Nombre_Usuario, Contraseña, TipoU)
VALUES('Rosa', 'Cortes','RosaCortes','4086', 3);
INSERT INTO Usuarios(Nombre, Apellido, Nombre_Usuario, Contraseña, TipoU)
VALUES('Monica', 'Cañas','MoniCañas','6504', 3);
--Proveedores
INSERT INTO Proveedores(Nombre_Proveedor, Nombre_Contacto, Pais, telefono1, telefono2, correo1, correo2)
VALUES('Inversiones Apparel','Josselin','El Salvador','2513-2843', '2274-4231','Inversionesapparel@apparel.net','Inversionesapparel@gmail.com');
INSERT INTO Proveedores(Nombre_Proveedor, Nombre_Contacto, Pais, telefono1, telefono2,correo1)
VALUES('incalsa','Mariana','El Salvador','2445-7000','2445-7010','info@incalsa.com.sv');
--Categorias--
INSERT INTO Categoria(Nombre_categoria)
VALUES('Camisas');
INSERT INTO Categoria(Nombre_categoria)
VALUES('Pantalones');
INSERT INTO Categoria(Nombre_categoria)
VALUES('Sacos');
--Estado--
INSERT INTO	estado(Id_Estado, Estado)
VALUES(1,'Activo');
INSERT INTO	estado(Id_Estado, Estado)
VALUES(2,'inactivo');
--Estantes
INSERT INTO	estante(id_estante, n_estante, id_categoria)
VALUES(1,'A1', 1);
INSERT INTO	estante(id_estante, n_estante, id_categoria)
VALUES(2,'B2', 2);
INSERT INTO	estante(id_estante, n_estante, id_categoria)
VALUES(3,'C1', 3);
--Bodega
INSERT INTO	bodega(id_bodega, Nombre_Bodega, id_estante)
VALUES(1,'Bodega Principal 1', 2);
INSERT INTO	bodega(id_bodega, Nombre_Bodega, id_estante)
VALUES(2,'Bodega Secundaria 2', 3);
INSERT INTO	bodega(id_bodega, Nombre_Bodega, id_estante)
VALUES(3,'Bodega 3',1);
--Pedido
INSERT INTO	Pedido(Nombre_Usuario, fecha_corte, fecha_facturacion)
VALUES('JuanGarcia', '2018-11-13','2018-11-13');
INSERT INTO	Pedido(Nombre_Usuario, fecha_corte, fecha_facturacion)
VALUES('VeroFuen', '2018-11-13','2018-11-10');
INSERT INTO	Pedido( Nombre_Usuario, fecha_corte, fecha_facturacion)
VALUES('MiguelHer' ,'2018-11-13','2018-11-9');
--productos
INSERT INTO	Productos(id_producto,Nombre_producto, Existencias, Precio_venta, Precio_compra, Descripcion, id_categoria, N_pedido,id_proveedor,ubicacion,estado)
VALUES(1,'Camisa Manga Larga', 3,10.50,11.50,'Talla L ,color azul',3,1,1,2,1 );
INSERT INTO	Productos(id_producto,Nombre_producto, Existencias, Precio_venta, Precio_compra, Descripcion, id_categoria, N_pedido,id_proveedor,ubicacion,estado)
VALUES(2,'Pantalon ', 3,19.50,22.56,'color Negro ',2,1,1,2,1 );
--detalle de Producto
INSERT INTO	detalle_productos(id_producto,Nombre_producto, Existencia, Precio_venta, Precio_compra, Descripcion, id_categoria, N_pedido,id_proveedor)
VALUES(1,'Camisa Manga Larga', 3,10.50,11.50,'Talla L ,color azul',3,3,1 );
INSERT INTO	detalle_productos(id_producto,Nombre_producto, Existencia, Precio_venta, Precio_compra, Descripcion, id_categoria, N_pedido,id_proveedor)
VALUES(2,'Pantalon ', 3,19.50,22.56,'color Negro ',2,4,1);
select * from detalle_productos

