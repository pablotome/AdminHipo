/*SQL_SERVER*/
use epotecario
GO

set xact_abort on
begin transaction

CREATE TABLE DBO.BEN_Taxonomias
(
	Taxonomia varchar(50), 
	Termino varchar(50), 
	Slug varchar(50), 
	TaxonomiaPadre int, 
	TaxonomiaId int, 
	TerminoId int
)
GO

CREATE TABLE DBO.BEN_Provincia
(
	CodProvincia int identity(1, 1), 
	Nombre varchar(50), 
	Slug  varchar(60), 
	CONSTRAINT PK_Provincia PRIMARY KEY (CodProvincia), 
	CONSTRAINT UK_SlugProvincia UNIQUE(Slug)
)
GO

CREATE TABLE DBO.BEN_Localidad
(
	CodLocalidad int identity(1, 1), 
	Nombre varchar(50), 
	Slug  varchar(60), 
	CodProvincia int, 
	CONSTRAINT PK_Localidad PRIMARY KEY (CodLocalidad), 
	CONSTRAINT FK_Localidad_Provincia FOREIGN KEY (CodProvincia) REFERENCES BEN_Provincia(CodProvincia), 
	CONSTRAINT UK_SlugLocalidad UNIQUE(Slug)
)
GO

CREATE TABLE DBO.BEN_TipoCliente
(
	CodTipoCliente int identity(1, 1), 
	Nombre varchar(50), 
	Slug  varchar(60), 
	CONSTRAINT PK_TipoCliente PRIMARY KEY (CodTipoCliente), 
	CONSTRAINT UK_SlugCliente UNIQUE(Slug)
)
GO

CREATE TABLE DBO.BEN_DiaSemana
(
	CodDiaSemana int identity(1, 1), 
	Nombre varchar(50), 
	Slug  varchar(60), 
	CONSTRAINT PK_DiaSemana PRIMARY KEY (CodDiaSemana), 
	CONSTRAINT UK_SlugDiaSemana UNIQUE(Slug)
)
GO

CREATE TABLE DBO.BEN_Categoria
(
	CodCategoria int identity(1, 1), 
	Nombre varchar(50), 
	Slug  varchar(60), 
	CodCategoriaPadre int, 
	CONSTRAINT PK_Categoria PRIMARY KEY (CodCategoria), 
	CONSTRAINT FK_Categoria_Categoria FOREIGN KEY (CodCategoriaPadre) REFERENCES BEN_Categoria(CodCategoria), 
	CONSTRAINT UK_SlugCategoria UNIQUE(Slug)
)
GO

CREATE TABLE DBO.BEN_MedioDePago
(
	CodMedioDePago int identity(1, 1), 
	Nombre varchar(50), 
	Slug  varchar(60), 
	CONSTRAINT PK_MedioDePago PRIMARY KEY (CodMedioDePago), 
	CONSTRAINT UK_SlugMedioDePago UNIQUE(Slug)
)
GO

CREATE TABLE DBO.BEN_Cuota
(
	CodCuota int identity(1, 1), 
	Nombre varchar(50), 
	Slug  varchar(60), 
	CONSTRAINT PK_Cuota PRIMARY KEY (CodCuota), 
	CONSTRAINT UK_SlugCuota UNIQUE(Slug)
)
GO

CREATE TABLE DBO.BEN_Sucursal
(
	CodSucursal int identity(1, 1), 
	Nombre varchar(50), 
	Slug  varchar(60), 
	CONSTRAINT PK_Sucursal PRIMARY KEY (CodSucursal), 
	CONSTRAINT UK_SlugSucursal UNIQUE(Slug)
)
GO

CREATE TABLE DBO.BEN_Ahorro
(
	CodAhorro int identity(1, 1), 
	Nombre varchar(50), 
	Slug  varchar(60), 
	CONSTRAINT PK_Ahorro PRIMARY KEY (CodAhorro), 
	CONSTRAINT UK_SlugAhorro UNIQUE(Slug)
)

commit transaction
set xact_abort off
GO