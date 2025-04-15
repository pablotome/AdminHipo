/*SQL_SERVER*/
use epotecario
GO

set xact_abort on
begin transaction

CREATE TABLE DBO.BEN_BasesYCondiciones
(
	CodBasesYCondiciones int identity(1, 1), 
	TextoBases varchar(max), 
	CONSTRAINT PK_BasesYCondiciones PRIMARY KEY (CodBasesYCondiciones)
)
GO

CREATE TABLE DBO.BEN_Beneficio
(
	CodBeneficio int, 
	Titulo varchar(255), 
	CodAhorro int, 
	CodCuota int, 
	CodMedioDePago int, 
	CFT varchar(10), 
	CodBasesYCondiciones int, 
	TopeReintegro int, 
	BreveResumen varchar(max), 
	Prioridad int, 
	CONSTRAINT PK_Beneficio PRIMARY KEY (CodBeneficio), 
	CONSTRAINT FK_Beneficio_Ahorro FOREIGN KEY (CodAhorro) REFERENCES DBO.BEN_Ahorro(CodAhorro), 
	CONSTRAINT FK_Beneficio_Cuota FOREIGN KEY (CodCuota) REFERENCES DBO.BEN_Cuota(CodCuota), 
	CONSTRAINT FK_Beneficio_MedioDePago FOREIGN KEY (CodMedioDePago) REFERENCES DBO.BEN_MedioDePago(CodMedioDePago), 
	CONSTRAINT FK_Beneficio_BasesYCondiciones FOREIGN KEY (CodBasesYCondiciones) REFERENCES DBO.BEN_BasesYCondiciones(CodBasesYCondiciones)
)
GO

CREATE TABLE DBO.BEN_BeneficioTipoCliente
(
	CodBeneficioTipoCliente int identity(1, 1), 
	CodBeneficio int, 
	CodTipoCliente int, 
	CONSTRAINT PK_BeneficioTipoCliente PRIMARY KEY (CodBeneficioTipoCliente), 
	CONSTRAINT FK_BeneficioTipoCliente_Beneficio FOREIGN KEY (CodBeneficio) REFERENCES DBO.BEN_Beneficio(CodBeneficio), 
	CONSTRAINT FK_BeneficioTipoCliente_TipoCliente FOREIGN KEY (CodTipoCliente) REFERENCES DBO.BEN_TipoCliente(CodTipoCliente)
)
GO

CREATE TABLE DBO.BEN_BeneficioDiaSemana
(
	CodBeneficioDiaSemana int identity(1, 1), 
	CodBeneficio int, 
	CodDiaSemana int, 
	CONSTRAINT PK_BeneficioDiaSemana PRIMARY KEY (CodBeneficioDiaSemana), 
	CONSTRAINT FK_BeneficioDiaSemana_Beneficio FOREIGN KEY (CodBeneficio) REFERENCES DBO.BEN_Beneficio(CodBeneficio), 
	CONSTRAINT FK_BeneficioDiaSemana_DiaSemana FOREIGN KEY (CodDiaSemana) REFERENCES DBO.BEN_DiaSemana(CodDiaSemana)
)

commit transaction
set xact_abort off
GO