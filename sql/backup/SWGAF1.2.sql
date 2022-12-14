USE [SWGAFv1.1]
GO
/****** Object:  Table [dbo].[category]    Script Date: 01/11/2022 0:18:34 ******/
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
/****** Object:  Table [dbo].[factura]    Script Date: 01/11/2022 0:18:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[factura](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NOT NULL,
	[cantidad] [float] NOT NULL,
	[tipoPago] [varchar](250) NOT NULL,
	[total] [float] NOT NULL,
	[fecha] [date] NOT NULL,
	[idUsuario] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kardex]    Script Date: 01/11/2022 0:18:34 ******/
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
/****** Object:  Table [dbo].[producto]    Script Date: 01/11/2022 0:18:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NOT NULL,
	[pVenta] [float] NOT NULL,
	[pCompra] [float] NOT NULL,
	[laboratorio] [varchar](250) NOT NULL,
	[fechaVencimiento] [date] NULL,
	[marca] [varchar](250) NOT NULL,
	[idStatus] [int] NOT NULL,
	[idCategory] [int] NOT NULL,
	[idRack] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productoHasFactura]    Script Date: 01/11/2022 0:18:34 ******/
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
/****** Object:  Table [dbo].[productoStatus]    Script Date: 01/11/2022 0:18:34 ******/
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
/****** Object:  Table [dbo].[rack]    Script Date: 01/11/2022 0:18:34 ******/
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
/****** Object:  Table [dbo].[solicitud]    Script Date: 01/11/2022 0:18:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[solicitud](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NOT NULL,
	[cantidad] [float] NOT NULL,
	[tipoDePago] [varchar](250) NOT NULL,
	[total] [float] NOT NULL,
	[fecha] [date] NULL,
	[aceptado] [int] NULL,
	[idUsuario] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[solicitudHasProducto]    Script Date: 01/11/2022 0:18:34 ******/
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
/****** Object:  Table [dbo].[usuario]    Script Date: 01/11/2022 0:18:34 ******/
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
/****** Object:  Table [dbo].[usuarioStatus]    Script Date: 01/11/2022 0:18:34 ******/
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
REFERENCES [dbo].[productoStatus] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD FOREIGN KEY([idStatus])
REFERENCES [dbo].[rack] ([id])
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
/****** Object:  StoredProcedure [dbo].[sp_AceptarSolicitud]    Script Date: 01/11/2022 0:18:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_AceptarSolicitud]
@Id int,
@Aceptado int
as
declare @existId int 
set @existId=(select Id From solicitud where id=@Id)
if(@Id=@existId)


begin
 if (@Aceptado=1)
begin
update solicitud set aceptado = 1 where id=@Id
 print'La solicitud fue acepta correctamente'
end
 
else if ( @Aceptado=2)
begin
 print 'La solicitud fue rechazada'
  update solicitud set aceptado = 2 where id=@Id
 end
 else if ( @Aceptado=3)
begin
 print 'La solicitud esta pendiente'
  update solicitud set aceptado = 3 where id=@Id
 end
 

end
else 
 begin
   print 'No se encontro el id'
 end
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarSolicitud]    Script Date: 01/11/2022 0:18:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_ActualizarSolicitud]
@id int,
@nombre VARCHAR(100),
@Cantidad int,
@tipoDePago VARCHAR(100),
@total float,
@fecha date,
@aceptado int,
@idUsuario int
AS
print 'se actulizo solicitud correctamente '
update solicitud SET nombre = @nombre,cantidad=@Cantidad,tipoDePago=@tipoDePago,total=@total,fecha=@fecha,aceptado=@aceptado,idUsuario=@idUsuario where id=@id
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarUsuario]    Script Date: 01/11/2022 0:18:34 ******/
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
@tipoUsuario VARCHAR(100),
@idStatus int
AS
declare @existId int
declare @existemail varchar(100)
declare @existdocnumber varchar(100)
declare @existidStatus varchar(100)
set @existemail = (select top 1 email from usuario where email = @email and not (id = @id))
set @existdocnumber = (select top 1 docNumber from usuario where docNumber = @docNumber  and not (id = @id))
-- @idStatus = 3, @existidStatus = ''
set @existidStatus = (select top 1 idStatus from usuario where idStatus = @idStatus)

 if(@email=@existemail)
   begin
    print 'Este email ya existe use otro'
   end
  ELSE IF(@docNumber=@existdocnumber  )
   begin
    print 'Este documento ya existe use otro'
   end 
   ELSE IF(@idStatus<>@existidStatus)
   begin
    print 'Este id no existe'
   end 
  else
    begin
     print 'Usuario actualizado correctamente'
    update usuario set email = @email, nombre = @nombre, apellido = @apellido, docNumber =@docNumber, tipoDoc= @tipoDoc,tipoUsuario = @tipoUsuario,idStatus =@idStatus where id = @id
    end
GO
/****** Object:  StoredProcedure [dbo].[sp_CambiarContrasena]    Script Date: 01/11/2022 0:18:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_CambiarContrasena]
@email varchar(100),
@passsword varchar(100),
@newpasssword varchar(100)
as
declare @user varchar(100)
declare @lastname varchar(100)

set @user = (select nombre from usuario where email = @email)
set @lastname = (select apellido from usuario where email = @email)

declare @existEmail varchar(100)
declare @existPassword varchar(50)
set @existEmail  = (select email from usuario where email = @email)
set @existPassword  = (select convert(Varchar(100), DECRYPTBYPASSPHRASE('SWGAF',passsword))from usuario where email = @email )

if(@existEmail=@email and @passsword = @existPassword)
begin
 print 'Se cambio la nueva contraseña usuario: '+@user+' '+@lastname
 Update usuario set passsword = ENCRYPTBYPASSPHRASE('SWGAF',@newpasssword) where  email = @email
 --set 
end
else
begin
 print 'email o password incorretos, intentelo de nuevo' 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarUsuario]    Script Date: 01/11/2022 0:18:34 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_HabilitarUsuario]    Script Date: 01/11/2022 0:18:34 ******/
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
 print 'El usuario fue Habilitado correctamente' 
 --delete usuario where id=@Id
 update usuario set IdStatus = 1 where id=@Id
end
else
begin
 print 'Nose encontro el ususario'
end
GO
/****** Object:  StoredProcedure [dbo].[sp_login]    Script Date: 01/11/2022 0:18:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_login]
@email varchar(100),
@passsword varchar(100)
as
declare @existUser varchar(100)
set @existUser = (select top 1 email from usuario where email=@email and passsword=@passsword)
declare @name varchar(100)
declare @lastname varchar(100)
if(@email=@existUser)
 begin
 set @name = (select nombre from usuario where email=@email and passsword=@passsword)
 set @lastname = (select apellido from usuario where email=@email and passsword=@passsword)
  print 'Bienvenido '+@name+' '+@lastname
 end
else
 begin
  print 'No se encontre ese usuario' 
 end
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarSolicitud]    Script Date: 01/11/2022 0:18:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_registrarSolicitud]
@nombre VARCHAR(100),
@Cantidad int,
@tipoDePago VARCHAR(100),
@total float,
@fecha date,
@aceptado int,
@idUsuario int
AS
print 'Se registro la solicitud correctamente'
insert into solicitud values (@nombre,@Cantidad,@tipoDePago,@total,@fecha,@aceptado,@idUsuario)
GO
/****** Object:  StoredProcedure [dbo].[sp_registrarUsuario]    Script Date: 01/11/2022 0:18:34 ******/
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
