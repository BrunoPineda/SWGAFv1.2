
/******Créditos BRUCORP******/
USE [SWGAFv1.2]
GO
/****** Object:  Table [dbo].[category]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[valor] [varchar](250) NOT NULL,
	[descripcion] [varchar](250) NOT NULL,
	[estado] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[factura]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[factura](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](250) NOT NULL,
	[cantidad] [float] NOT NULL,
	[tipoPago] [varchar](250) NOT NULL,
	[totaPrecio] [float] NOT NULL,
	[fecha] [date] NOT NULL,
	[idUsuario] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kardex]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kardex](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[valor] [varchar](250) NOT NULL,
	[fechaSalida] [date] NOT NULL,
	[idFactura] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producto]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo]  AS ('SKU-'+right('00'+CONVERT([varchar],[ID]),(4))),
	[descripcion] [varchar](250) NOT NULL,
	[pVenta] [float] NOT NULL,
	[pCompra] [float] NOT NULL,
	[laboratorio] [varchar](250) NOT NULL,
	[fechaVencimiento] [date] NULL,
	[marca] [varchar](250) NOT NULL,
	[stock] [int] NULL,
	[tipoProducto] [varchar](250) NOT NULL,
	[idStatus] [int] NOT NULL,
	[idCategory] [int] NOT NULL,
	[idRack] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productoHasFactura]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productoHasFactura](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [float] NOT NULL,
	[idFactura] [int] NOT NULL,
	[idproducto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productoStatus]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productoStatus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[valor] [varchar](250) NOT NULL,
	[descripcion] [varchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rack]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rack](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[valor] [varchar](250) NOT NULL,
	[descripcion] [varchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[solicitud]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[solicitud](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](250) NOT NULL,
	[cantProductos] [int] NOT NULL,
	[tipoDePago] [varchar](250) NOT NULL,
	[tipoDeMoneda] [varchar](250) NOT NULL,
	[totaPrecio] [float] NOT NULL,
	[fecha] [date] NULL,
	[aceptado] [bit] NULL,
	[idUsuario] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[solicitudHasProducto]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[solicitudHasProducto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cantidad] [float] NOT NULL,
	[precio] [float] NOT NULL,
	[idSolicitud] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](250) NULL,
	[passsword] [varbinary](max) NULL,
	[nombre] [varchar](250) NULL,
	[apellido] [varchar](250) NULL,
	[docNumber] [int] NOT NULL,
	[tipoDoc] [varchar](250) NULL,
	[tipoUsuario] [varchar](250) NULL,
	[idStatus] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarioStatus]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarioStatus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[valor] [varchar](250) NOT NULL,
	[descripcion] [varchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[factura]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[kardex]  WITH CHECK ADD FOREIGN KEY([idFactura])
REFERENCES [dbo].[factura] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD FOREIGN KEY([idCategory])
REFERENCES [dbo].[category] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD FOREIGN KEY([idRack])
REFERENCES [dbo].[rack] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD FOREIGN KEY([idStatus])
REFERENCES [dbo].[productoStatus] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[productoHasFactura]  WITH CHECK ADD FOREIGN KEY([idFactura])
REFERENCES [dbo].[factura] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[productoHasFactura]  WITH CHECK ADD FOREIGN KEY([idproducto])
REFERENCES [dbo].[producto] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[solicitud]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[solicitudHasProducto]  WITH CHECK ADD FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[solicitudHasProducto]  WITH CHECK ADD FOREIGN KEY([idSolicitud])
REFERENCES [dbo].[solicitud] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD FOREIGN KEY([idStatus])
REFERENCES [dbo].[usuarioStatus] ([id])
ON DELETE CASCADE
GO
/****** Object:  StoredProcedure [dbo].[insertarHasProductos]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[insertarHasProductos]
@idFactura int,
@min int,
@max int
as 
declare @stock int = (select top 1 stock from producto where id = @min);
declare @numeroRandom int = (SELECT FLOOR(RAND()*(@stock-1)+1));
declare @precio float = (select top 1 pVenta from producto) 
declare @precioTotal float = (@numeroRandom*@precio)

WHILE ( @min <= @max)
BEGIN
    set @numeroRandom  = (SELECT FLOOR(RAND()*(@stock-1)+1));
	set @precioTotal  = (@numeroRandom*@precio)
    insert into productoHasFactura values (@numeroRandom,@precioTotal,@idFactura,@min)
    SET @min  = @min  + 1
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarUsuario]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_ActualizarUsuario] 
@id int,
@email  VARCHAR(100),
@nombre VARCHAR(100),
@apellido VARCHAR(100),
@docNumber int,
@tipoDoc   VARCHAR(100),
@tipoUsuario VARCHAR(100)
AS
declare @existId int
declare @existemail varchar(100)
declare @existdocnumber varchar(100)
declare @existidStatus varchar(100)
set @existemail = (select top 1 email from usuario where email = @email and not (id = @id))
set @existdocnumber = (select top 1 docNumber from usuario where docNumber = @docNumber  and not (id = @id))
-- @idStatus = 3, @existidStatus = ''


 if(@email=@existemail)
   begin
    print 'Este email ya existe use otro'
   end
  ELSE IF(@docNumber=@existdocnumber  )
   begin
    print 'Este documento ya existe use otro'
   end 
  else
    begin
     print 'Usuario actualizado correctamente'
    update usuario set email = @email, nombre = @nombre, apellido = @apellido, docNumber =@docNumber, tipoDoc= @tipoDoc,tipoUsuario = @tipoUsuario where id = @id
    end
GO
/****** Object:  StoredProcedure [dbo].[sp_comprarProducto]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_comprarProducto] 
@id int,
@cantidad int
AS
declare @stock int = (select top 1 stock  from producto where id = @id)
declare @precio float = (select top 1  pVenta from producto where id = @id)
declare @precioTotal float = @cantidad * @precio

if(@stock<@cantidad)
begin
 print 'Te pasaste del limite maximo, para este producto solo hay '+CAST(@stock as varchar(100))+' unidades'
end
else
 begin
 print 'Total : s/.'+CAST(@precioTotal as varchar(100))
    update producto set stock = @stock-@cantidad where id = @id
	declare @stocknew int = (select stock from producto where id = @id)
	if(@stocknew = 0)
	begin
	  update producto set idStatus = 2 where id = @id
	end
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarUsuario]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_EliminarUsuario]
@Id int
as
declare @existId int
set @existId=(select Id from usuario where id=@Id)
if (@Id = @existId)
begin
 print 'El usuario fue inhabilitado correctamente' 
 --delete usuario where id=@Id
 update usuario set IdStatus = 2 where id=@Id
end
else
begin
 print 'Nose encontro el ususario'
end
GO
/****** Object:  StoredProcedure [dbo].[sp_HabilitarUsuario]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_HabilitarUsuario]
@Id int
as
declare @existId int
set @existId=(select Id from usuario where id=@Id)
if (@Id = @existId)
begin
 print 'El usuario fue habilitado correctamente' 
 --delete usuario where id=@Id
 update usuario set IdStatus = 1 where id=@Id
end
else
begin
 print 'Nose encontro el ususario'
end
GO
/****** Object:  StoredProcedure [dbo].[sp_insertarProducto]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_insertarProducto]
@descripcion varchar(50),
@pVenta float,
@pCompra float,
@laboratorio varchar(50),
@fechaVencimiento date,
@marca varchar(50),
@stock int,
@tipoProducto varchar(50),
@idStatus int,
@idCategory int,
@idRack int
AS
insert into producto values (@descripcion,@pVenta ,@pCompra,@laboratorio ,@fechaVencimiento,@marca,@stock,@tipoProducto,@idStatus,@idCategory,@idRack)
GO
/****** Object:  StoredProcedure [dbo].[sp_login]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_login]
@email varchar(100),
@passsword varchar(100)
as
declare @existUser varchar(100) -- email
declare @existPassword varchar(200) --password

set @existPassword = (select convert(Varchar(100), DECRYPTBYPASSPHRASE('SWGAF',passsword)) from usuario where email = @email)
set @existUser = (select top 1 email from usuario where email=@email)

declare @name varchar(100)
declare @lastname varchar(100)

if(@email=@existUser and @passsword=@existPassword)
 begin
 set @name = (select nombre from usuario where email=@email)
 set @lastname = (select apellido from usuario where email=@email)
  print 'Bienvenido '+@name+' '+@lastname
 end
else
 begin
  print 'No se encontre ese usuario'  
 end
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarUsuario]    Script Date: 21/11/2022 11:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_registrarUsuario]
@email  VARCHAR(100),
@password  VARCHAR(100),
@nombre VARCHAR(100),
@apellido VARCHAR(100),
@docNumber int,
@tipoDoc   VARCHAR(100),
@tipoUsuario VARCHAR(100),
@idStatus int
AS
declare @existemail varchar(100)
declare @existdocnumber varchar(100)
declare @existidStatus varchar(100)
set @existemail = (select top 1 email from usuario where email = @email)
set @existdocnumber = (select top 1 docNumber from usuario where docNumber = @docNumber)
-- @idStatus = 3, @existidStatus = ''
set @existidStatus = (select top 1 idStatus from usuario where idStatus = @idStatus)

 if(@email=@existemail)
   begin
    print 'Este email ya existe use otro'
   end
  ELSE IF(@docNumber=@existdocnumber)
   begin
    print 'Este documento ya existe use otro'
   end 
   ELSE IF(@idStatus<>@existidStatus)
   begin
    print 'Este id no existe'
   end 
  else
    begin
     print 'Usuario registrado correctamente'
    insert into usuario values(@email,
    ENCRYPTBYPASSPHRASE('SWGAF',@password)
    ,@nombre,@apellido,@docNumber,@tipoDoc,@tipoUsuario,@idStatus)
    end
GO
