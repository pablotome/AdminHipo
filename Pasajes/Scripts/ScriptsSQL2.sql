use epotecario
select * from beneficio where IDImportacion = (select max(idimportacion) from beneficio)
select * from ErroresImportacion order by 1 desc
select * from ImportacionesExcel order by 1 desc
select * from Sucursal

Completado con errores

select * from Beneficio
select * from Alianza where Titulo like '%aerolin%'
select * from Alianza where id = 141558 order by Titulo

Clientes
	CodCliente
	Descripcion
		buho-emprendedor
		buho-jubilados
		buho-one
		buho-sueldo
		todos-los-clientes

Beneficios
	CodBeneficio
	Titulo
	Ahorros
	Cuotas
	MediosDePago
	CFT
	IdBases
	TopeReintegro
	BreveResumen
	Prioridad

Beneficios-Cliente
	CodBeneficio
	CodCliente

Beneficios-Dias
	CodBeneficio
	CodDia

BasesYCondiciones
	CodBasesYCondiciones
	TextoBases


Alianza
	CodAlianza
	Titulo
	TieneSucursales (se infiere)
	Highlight
	Logo
	SitioWeb
	Orden
	

AlianzaBeneficio
	CodBeneficio
	CodAlianza

Categoria
	CodCategoria
	DEscripcion
	CodCategoriaPadre

AlianzaCategoria
	CodAlianza
	CodCategoria




select * from ExcelBeneficio order by 1 desc
delete from ExcelBeneficio

select count(*) from ExcelBeneficio
select count(*) from ExcelAlianza
select count(*) from ExcelSucursal

select * from ErroresImportacion

select top 10 * from ExcelBeneficio

truncate table ExcelBeneficio
truncate table ExcelAlianza
truncate table ExcelSucursal
