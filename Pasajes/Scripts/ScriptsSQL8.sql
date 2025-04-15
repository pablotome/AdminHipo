use epotecario

select * from ben_ExcelBeneficio

select * from ben_Beneficio

ben_Beneficio
ben_Beneficio.CodBeneficio				=>	ben_ExcelBeneficio.ID_BEN
ben_Beneficio.Titulo					=>	ben_ExcelBeneficio.Titulo
ben_Beneficio.CodAhorro					=>	ben_Ahorra -> ben_ExcelBeneficio.Ahorros
ben_Beneficio.CodCuota					=>	ben_cuota -> ben_ExcelBeneficio.Cuotas
ben_Beneficio.MediosDePago				=>	ben_MediosPago -> ben_ExcelBeneficio.MediosPago
ben_Beneficio.CFT						=>	ben_ExcelBeneficio.CFT
ben_Beneficio.CodBasesYCondiciones		=>	ben_BasesYCondiciones -> ben_ExcelBeneficio.BasesYCondiciones
ben_Beneficio.TopeReintegro				=>	ben_ExcelBeneficio.TopeReintegro
ben_Beneficio.BreveResumen				=>	ben_ExcelBeneficio.BreveResumen
ben_Beneficio.Prioridad					=>	ben_ExcelBeneficio.Prioridad


BEN_BeneficioTipoCliente
BEN_BeneficioTipoCliente.CodBeneficioTipoCliente	=>	identity
BEN_BeneficioTipoCliente.CodBeneficio				=>	ben_Beneficio.CodBeneficio
BEN_BeneficioTipoCliente.CodTipoCliente				=>	ben_ExcelBeneficio.Clientes -> BEN_TipoCliente.CodTipoCliente

BEN_BeneficioDiaSemana
BEN_BeneficioDiaSemana.CodBeneficioDiaSemana		=>	identity
BEN_BeneficioDiaSemana.CodBeneficio					=>	ben_Beneficio.CodBeneficio
BEN_BeneficioDiaSemana.CodDiaSemana					=>	ben_ExcelBeneficio.Dias -> BEN_DiaSemana.CodDiaSemana



select cast(id_ben as int) from ben_ExcelBeneficio

select distinct topereintegro from ben_ExcelBeneficio
update ben_ExcelBeneficio set topereintegro = replace(topereintegro, ',', '')


select * from ben_Beneficio 

exec DBO.BEN_ProcesarBeneficios @IdImportacion = 3