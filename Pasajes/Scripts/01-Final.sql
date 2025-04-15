/*SQL_SERVER*/
use epotecario
GO

set xact_abort on
begin transaction

CREATE TABLE DBO.BEN_ExcelBeneficio
(
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

CREATE INDEX ind__BEN_ExcelBeneficio__ID_BEN ON BEN_ExcelBeneficio(ID_BEN)

CREATE TABLE DBO.BEN_ExcelAlianza
(
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

CREATE TABLE DBO.BEN_ExcelSucursal
(
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

-- Tabla para registro de importaciones
CREATE TABLE DBO.BEN_ImportacionesExcel
(
    ImportacionID INT IDENTITY(1,1) PRIMARY KEY,
    NombreArchivo VARCHAR(255) NOT NULL,
    TipoImportacion VARCHAR(50) NOT NULL,
    FechaImportacion DATETIME DEFAULT GETDATE(),
    UsuarioImportacion VARCHAR(50),
    CantidadRegistros INT,
    CantidadErrores INT,
	ErrorImportacion VARCHAR(2000), 
    EstadoImportacion VARCHAR(50)
)

commit transaction
set xact_abort off
GO