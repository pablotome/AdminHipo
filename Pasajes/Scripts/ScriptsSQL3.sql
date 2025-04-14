use epotecario
UPDATE ImportacionesExcel 
SET CantidadRegistros = 4972, 
CantidadErrores = 189, 
EstadoImportacion = 'Completado con errores'
WHERE ImportacionID = 5

SELECT * FROM Sucursal where IDImportacion = (select max(idimportacion) from Sucursal) ORDER BY Titulo


declare @ww decimal(20, 17) = -25.125951183979666
select @ww
declare @ww2 decimal(20, 17) = -125.125951183979666
select @ww2

select * from ExcelBeneficio
select * from ExcelAlianza
select * from ExcelSucursal
select * from ErroresImportacion
select * from ImportacionesExcel

delete from ExcelBeneficio
delete from ExcelAlianza
delete from ExcelSucursal
delete from ErroresImportacion
delete from ImportacionesExcel

select * from ExcelBeneficio where IDImportacion = (select max(idimportacion) from ExcelBeneficio) ORDER BY Titulo

select LEN(longitud), longitud, cast(longitud as decimal(20, 17)), LEN(latitud), latitud, cast(latitud as decimal(20, 17))
from ExcelSucursal
order by 1 desc

select idalianzas, len(idalianzas) from ExcelSucursal order by 2 desc
select beneficios, len(beneficios) from ExcelAlianza order by 2 desc
select * from ExcelBeneficio where ID_BEN in ('93761', '93763', '93757', '93759', '93765', '135404')
select * from ExcelAlianza where beneficios = '93761, 93763, 93757, 93759, 93765, 135404'

select distinct ID_BEN, beneficios from ExcelBeneficio b, ExcelAlianza a where beneficios like '%'+id_ben+'%'


select distinct cft from ExcelBeneficio



select * from ExcelAlianza where ID = 97695
select * from ExcelAlianza order by 1
select * from ExcelAlianza where RegistroValido = 0 and Beneficios <> '' order by 1
truncate table ExcelAlianza

select * from Beneficio

select * from ExcelBeneficio where IdImportacion in (select max(IdImportacion) from ExcelBeneficio ) and RegistroValido = 0 order by DetalleError
select * from ExcelAlianza where IdImportacion in (select max(IdImportacion) from ExcelAlianza ) and RegistroValido = 0 order by DetalleError
select * from ExcelAlianza where categorias like 'consum%'


select * from ExcelBeneficio where IdImportacion in (select max(IdImportacion) from ExcelBeneficio ) and RegistroValido = 0 order by DetalleError
select * from ExcelAlianza where IdImportacion in (select max(IdImportacion) from ExcelAlianza ) and RegistroValido = 0 order by DetalleError
select * from ExcelSucursal where categorias like 'consum%'

select * from DiaSemana
select * from TipoCliente
select * from Ahorro
select * from Cuota
select * from MedioDePago
select * from Beneficio 
select * from ExcelBeneficio 
