--DROP TABLE DBO.Beneficio
--DROP TABLE DBO.Alianza



CREATE TABLE DBO.Alianza
(
	CodAlianza int, 
	Titulo varchar(255), 
	Highlight varchar(255), 
	Logo varchar(255), 
	SitioWeb varchar(255), 
	Orden int
)

CREATE TABLE DBO.Sucursal
(
	CodAlianza int, 
	Titulo varchar(255), 
	Highlight varchar(255), 
	Logo varchar(255), 
	SitioWeb varchar(255), 
	Orden int
)
