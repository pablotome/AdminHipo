/*SQL_SERVER*/
use epotecario
GO

CREATE PROCEDURE DBO.BEN_ProcesarBeneficios @IdImportacion int AS
/*
	Procesa los beneficios cargados desde el excel (tabla ben_ExcelBeneficio) hacia tablas relacionales (tablas ben_Beneficio y relacionadas)
*/

set xact_abort on
begin transaction

	--Cargo los nuevos bases y conficiones
	insert into ben_BasesYCondiciones (TextoBases)
	select distinct eb.BasesYCondiciones
	from ben_ExcelBeneficio eb
	where 
		eb.IdImportacion = @IdImportacion
		and not exists (
			select *
			from ben_BasesYCondiciones byc
			where byc.TextoBases = eb.BasesYCondiciones
		)


	--Cargo en una tabla temporal los datos que vienen desde el excel y los IDs de las tablas relacionadas: ahorro, cuotas, medios de pago y bases y condiciones
	select eb.*, a.CodAhorro, c.CodCuota, mp.CodMedioDePago, byc.CodBasesYCondiciones
	into #tmpExcelBeneficios
	from ben_ExcelBeneficio eb
		left join ben_Ahorro a on (eb.Ahorros = a.Nombre)
		left join ben_Cuota c on (eb.Cuotas = c.Nombre)
		left join ben_MedioDePago mp on (eb.MediosPago = mp.Slug)
		left join ben_BasesYCondiciones byc on (byc.TextoBases = eb.BasesYCondiciones)
	where 
		IdImportacion = @IdImportacion


	--Actualizo los beneficios que ya existen según ID_BEN
	update b
	set 
		b.Titulo = eb.Titulo, 
		b.CodAhorro = eb.CodAhorro, 
		b.CodCuota = eb.CodCuota, 
		b.CodMedioDePago = eb.CodMedioDePago, 
		b.CFT = eb.CFT, 
		b.CodBasesYCondiciones = eb.CodBasesYCondiciones, 
		b.TopeReintegro = eb.TopeReintegro, 
		b.BreveResumen = eb.BreveResumen, 
		b.Prioridad = eb.Prioridad
	from ben_Beneficio b inner join #tmpExcelBeneficios eb 
		on (b.CodBeneficio = cast(eb.ID_BEN as int))
	where
		eb.IdImportacion = @IdImportacion



	--Inserto los nuevos beneficios
	insert into ben_Beneficio (CodBeneficio, Titulo, CodAhorro, CodCuota, CodMedioDePago, CFT, CodBasesYCondiciones, TopeReintegro, BreveResumen, Prioridad)
	select cast(eb.ID_BEN as int), eb.Titulo, eb.CodAhorro, eb.CodCuota, eb.CodMedioDePago, eb.CFT, eb.CodBasesYCondiciones, eb.TopeReintegro, eb.BreveResumen, eb.Prioridad
	from #tmpExcelBeneficios eb
	where 
		eb.IdImportacion = @IdImportacion
		and not exists (
			select *
			from ben_Beneficio b
			where b.CodBeneficio = cast(eb.ID_BEN as int)
		)

	--Elimino la tabla temporal
	drop table #tmpExcelBeneficios

commit transaction
set xact_abort off
GO