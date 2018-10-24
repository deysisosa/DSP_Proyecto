create database inventario
go
use inventario
go
	
create table estado(
Id_Estado int,
Estado char(15),
constraint pk_estado primary key (Id_Estado)
)
insert estado values
(1,'Nuevo'),
(2,'Antiguo')

create table Productos(
id_producto int identity(1,1),
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
id_producto int,
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
insert Tipo_Usuario values
 (1,'Administrador'),
 (2,'Usuario')

create table Usuarios(
Nombre varchar(25),
Apellido varchar(25),
Nombre_Usuario varchar(10),
Contraseña varchar(50),
TipoU int,
constraint pk_usu primary key (Nombre_Usuario)
);

create table bodega(
id_bodega int identity(1,1),
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
id_estante int identity (1,1),
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
	@id_producto int,
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
	INSERT INTO productos values (@Nombre_producto,@Existencias,@Precio,@precio2,@Descripcion,@id_categoria,@NPedido,@id_proveedor,@Ubicacion,@estado)
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

