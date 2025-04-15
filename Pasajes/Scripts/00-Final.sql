/*SQL_SERVER*/
use epotecario
GO

set xact_abort on
begin transaction

if exists (select * from sysobjects where xtype='U' and name = 'BEN_ImportacionesExcel')
	drop table DBO.BEN_ImportacionesExcel

if exists (select * from sysobjects where xtype='U' and name = 'BEN_ExcelSucursal')
	drop table DBO.BEN_ExcelSucursal

if exists (select * from sysobjects where xtype='U' and name = 'BEN_ExcelAlianza')
	drop table DBO.BEN_ExcelAlianza

if exists (select * from sysobjects where xtype='U' and name = 'BEN_ExcelBeneficio')
	drop table DBO.BEN_ExcelBeneficio

if exists (select * from sysobjects where xtype='U' and name = 'BEN_BeneficioDiaSemana')
	drop table DBO.BEN_BeneficioDiaSemana

if exists (select * from sysobjects where xtype='U' and name = 'BEN_BeneficioTipoCliente')
	drop table DBO.BEN_BeneficioTipoCliente

if exists (select * from sysobjects where xtype='U' and name = 'BEN_Beneficio')
	drop table DBO.BEN_Beneficio

if exists (select * from sysobjects where xtype='U' and name = 'BEN_Ahorro')
	drop table DBO.BEN_Ahorro

if exists (select * from sysobjects where xtype='U' and name = 'BEN_Sucursal')
	drop table DBO.BEN_Sucursal

if exists (select * from sysobjects where xtype='U' and name = 'BEN_Cuota')
	drop table DBO.BEN_Cuota

if exists (select * from sysobjects where xtype='U' and name = 'BEN_MedioDePago')
	drop table DBO.BEN_MedioDePago

if exists (select * from sysobjects where xtype='U' and name = 'BEN_Categoria')
	drop table DBO.BEN_Categoria

if exists (select * from sysobjects where xtype='U' and name = 'BEN_DiaSemana')
	drop table DBO.BEN_DiaSemana

if exists (select * from sysobjects where xtype='U' and name = 'BEN_TipoCliente')
	drop table DBO.BEN_TipoCliente

if exists (select * from sysobjects where xtype='U' and name = 'BEN_Localidad')
	drop table DBO.BEN_Localidad

if exists (select * from sysobjects where xtype='U' and name = 'BEN_Provincia')
	drop table DBO.BEN_Provincia

if exists (select * from sysobjects where xtype='U' and name = 'BEN_Taxonomias')
	drop table DBO.BEN_Taxonomias

if exists (select * from sysobjects where xtype='U' and name = 'BEN_BasesYCondiciones')
	drop table DBO.BEN_BasesYCondiciones

if exists (select * from sysobjects where xtype='P' and name = 'BEN_ProcesarBeneficios')
	drop procedure DBO.BEN_ProcesarBeneficios

commit transaction
set xact_abort off
GO