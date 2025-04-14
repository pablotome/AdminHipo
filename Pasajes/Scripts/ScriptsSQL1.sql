use epotecario

--DROP TABLE DBO.ExcelSucursal
--DROP TABLE DBO.ExcelAlianza
--DROP TABLE DBO.ExcelBeneficio
--DROP TABLE DBO.ErroresImportacion
--DROP TABLE DBO.ImportacionesExcel
--DROP PROCEDURE DBO.InsertarExcelBeneficio
--DROP PROCEDURE DBO.InsertarExcelAlianza
--DROP PROCEDURE DBO.InsertarExcelSucursal


CREATE TABLE DBO.ExcelBeneficio (
	CodBeneficio int identity(1, 1), 
    Titulo VARCHAR(255) NOT NULL,
    Clientes VARCHAR(255),
    Ahorros VARCHAR(10),
    Cuotas VARCHAR(10),
    MediosPago VARCHAR(255),
    Dias VARCHAR(100),
    CFT VARCHAR(10),
    BasesYCondiciones VARCHAR(MAX),
    TopeReintegro VARCHAR(50),
    BreveResumen VARCHAR(MAX),
    TipoDescuento VARCHAR(MAX),
    Prioridad VARCHAR(10),
    ID_BEN VARCHAR(10),
	IdImportacion INT, 
	RegistroValido bit, 
	DetalleError varchar(2000), 
	CONSTRAINT PK_ExcelBeneficio PRIMARY KEY (CodBeneficio)
)
GO

CREATE TABLE DBO.ExcelAlianza (
    CodAlianza int identity(1, 1), 
	Titulo VARCHAR(255) NOT NULL,
    Beneficios VARCHAR(255),
    Categorias VARCHAR(255),
    CategoriaPrincipal VARCHAR(255),
    TieneSucursales VARCHAR(10),
    Direccion VARCHAR(255),
    Latitud VARCHAR(30),
    Longitud VARCHAR(30),
    Highlight VARCHAR(200),
    Logo VARCHAR(255),
    SitioWeb VARCHAR(255),
    Orden VARCHAR(10),
    Activo VARCHAR(10),
    ID VARCHAR(10),
	IdImportacion INT, 
	RegistroValido bit, 
	DetalleError varchar(2000), 
	CONSTRAINT PK_ExcelAlianza PRIMARY KEY (CodAlianza)
)
GO

CREATE TABLE DBO.ExcelSucursal (
	CodSucursal int identity(1, 1), 
    Titulo VARCHAR(255) NOT NULL,
    Calle VARCHAR(255),
    Numero VARCHAR(10),
    Localidad VARCHAR(255),
    CodigoPostal VARCHAR(20),
    Provincia VARCHAR(255),
    Latitud VARCHAR(30),
    Longitud VARCHAR(30),
    InfoAdicional VARCHAR(MAX),
    IDAlianzas VARCHAR(MAX),
    ID VARCHAR(10),
	IdImportacion INT, 
	RegistroValido bit, 
	DetalleError varchar(2000), 
	CONSTRAINT PK_ExcelSucursal PRIMARY KEY (CodSucursal)
)
GO


/*CREATE TABLE DBO.Parametro
(
	CodParametro varchar(50) NOT NULL PRIMARY KEY, 
	Valor varchar(255)
)
GO*/


/*CREATE TABLE DBO.LogImportaciones
(
	CodLogImportaciones int identity(1, 1), 
	NombreArchivo varchar(100), 
	TipoArchivo varchar(100), 
	IDImportacion int, 
	Fecha datetime, 
	CantidadRegistros int, 
	CantidadErrores int, 
	Usuario varchar(10), 
	CONSTRAINT PK_LogImportaciones PRIMARY KEY (CodLogImportaciones)
)
GO*/

CREATE TABLE DBO.ErroresImportacion (
    CodError INT IDENTITY(1,1) PRIMARY KEY,
    CodLogImportaciones INT, -- FOREIGN KEY REFERENCES LogImportaciones(CodLogImportaciones),
    FilaExcel INT,
    DescripcionError VARCHAR(MAX),
    DatosOriginales VARCHAR(MAX)
);
GO

/*insert into Parametro(CodParametro, Valor) values 
('UltimoArchivoBeneficios', '0'), 
('UltimoArchivoAlianzas', '0'), 
('UltimoArchivoSucursales', '0')*/

-- Tabla para registro de importaciones
CREATE TABLE DBO.ImportacionesExcel (
    ImportacionID INT IDENTITY(1,1) PRIMARY KEY,
    NombreArchivo VARCHAR(255) NOT NULL,
    TipoImportacion VARCHAR(50) NOT NULL,
    FechaImportacion DATETIME DEFAULT GETDATE(),
    UsuarioImportacion VARCHAR(50),
    CantidadRegistros INT,
    CantidadErrores INT,
	ErrorImportacion VARCHAR(2000), 
    EstadoImportacion VARCHAR(50)
);
GO

CREATE PROCEDURE DBO.InsertarExcelBeneficio
	@Titulo	VARCHAR(255), 
	@Clientes VARCHAR(255), 
	@Ahorros VARCHAR(10), 
	@Cuotas	VARCHAR(10), 
	@MediosPago VARCHAR(510), 
	@Dias VARCHAR(100), 
	@CFT VARCHAR(10), 
	@BasesYCondiciones VARCHAR(max), 
	@TopeReintegro VARCHAR(50), 
	@BreveResumen VARCHAR(max), 
	@TipoDescuento VARCHAR(max), 
	@Prioridad VARCHAR(10), 
	@ID_BEN VARCHAR(10), 
	@IDImportacion int, 
	@RegistroValido bit, 
	@DetalleError varchar(2000)
AS

set xact_abort on
begin transaction
set nocount on

	if (@ID_BEN = '')
		select @ID_BEN = isnull(max(id_ben), 0) + 1 from ExcelBeneficio

	insert into ExcelBeneficio (Titulo, Clientes, Ahorros, Cuotas, MediosPago, Dias, CFT, BasesYCondiciones, TopeReintegro, BreveResumen, TipoDescuento, Prioridad, ID_BEN, IDImportacion, RegistroValido, DetalleError)
	values (@Titulo, @Clientes, @Ahorros, @Cuotas, @MediosPago, @Dias, @CFT, @BasesYCondiciones, @TopeReintegro, @BreveResumen, @TipoDescuento, @Prioridad, @ID_BEN, @IDImportacion, @RegistroValido, @DetalleError)

	select cast(1 as bit) as Nuevo

set nocount off
commit transaction
set xact_abort off
GO

CREATE PROCEDURE DBO.InsertarExcelAlianza
	@Titulo varchar(255), 
	@Beneficios varchar(255), 
	@Categorias varchar(255), 
	@CategoriaPrincipal varchar(255), 
	@TieneSucursales varchar(10), 
	@Direccion varchar(255), 
	@Latitud varchar(30), 
	@Longitud varchar(30), 
	@Highlight varchar(200), 
	@Logo varchar(255), 
	@SitioWeb varchar(255), 
	@Orden varchar(10), 
	@Activo varchar(10), 
	@ID varchar(10), 
	@IdImportacion int, 
	@RegistroValido bit, 
	@DetalleError varchar(2000)
AS

set xact_abort on
begin transaction
set nocount on

	if (@ID = '')
		select @ID = isnull(max(id), 0) + 1 from ExcelAlianza

	insert into ExcelAlianza (Titulo, Beneficios, Categorias, CategoriaPrincipal, TieneSucursales, Direccion, Latitud, Longitud, Highlight, Logo, SitioWeb, Orden, Activo, ID, IdImportacion, RegistroValido, DetalleError)
	values (@Titulo, @Beneficios, @Categorias, @CategoriaPrincipal, @TieneSucursales, @Direccion, @Latitud, @Longitud, @Highlight, @Logo, @SitioWeb, @Orden, @Activo, @ID, @IdImportacion, @RegistroValido, @DetalleError)

	select cast(1 as bit) as Nuevo

set nocount off
commit transaction
set xact_abort off
GO



CREATE PROCEDURE DBO.InsertarExcelSucursal
	@Titulo	VARCHAR(255), 
	@Calle VARCHAR(255), 
	@Numero VARCHAR(10), 
	@Localidad VARCHAR(255), 
	@CodigoPostal VARCHAR(20), 
	@Provincia VARCHAR(255), 
	@Latitud VARCHAR(30), 
	@Longitud VARCHAR(30), 
	@InfoAdicional VARCHAR(max), 
	@IDAlianzas VARCHAR(max), 
	@ID VARCHAR(10), 
	@IDImportacion int, 
	@RegistroValido bit, 
	@DetalleError varchar(2000)
AS

set xact_abort on
begin transaction
set nocount on

	if (@ID = '')
		select @ID = isnull(max(id), 0) + 1 from ExcelSucursal

	insert into ExcelSucursal (Titulo, Calle, Numero, Localidad, CodigoPostal, Provincia, Latitud, Longitud, InfoAdicional, IDAlianzas, ID, IDImportacion, RegistroValido, DetalleError)
	values (@Titulo, @Calle, @Numero, @Localidad, @CodigoPostal, @Provincia, @Latitud, @Longitud, @InfoAdicional, @IDAlianzas, @ID, @IdImportacion, @RegistroValido, @DetalleError)

	select cast(1 as bit) as Nuevo

set nocount off
commit transaction
set xact_abort off
GO
