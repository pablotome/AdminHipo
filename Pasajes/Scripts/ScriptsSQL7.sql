CREATE TABLE [dbo].[Provincias](
	[CodProvincia] [int] IDENTITY(1,1) NOT NULL,
	[NomProvincia] [varchar](60) NOT NULL,
	[Activa] [bit] NOT NULL,
	[IVA] [decimal](18, 9) NULL,
	[PrimaSeguroIncendio] [decimal](18, 9) NULL,
	[PrimaSeguroIncendioFSPUVA] [decimal](18, 9) NULL,
 CONSTRAINT [PK_Provincias] PRIMARY KEY CLUSTERED 
(
	[CodProvincia] ASC
)
)
GO


CREATE TABLE [dbo].[TiposSucursales](
	[codTipoSucursal] [int] IDENTITY(1,1) NOT NULL,
	[desTipoSucursal] [varchar](150) NOT NULL,
	[Orden] [int] NULL,
 CONSTRAINT [PK_TiposSucursales] PRIMARY KEY NONCLUSTERED 
(
	[codTipoSucursal] ASC
)
)
GO

CREATE TABLE [dbo].[Sucursales](
	[CodSucursal] [int] NOT NULL,
	[DesSucursal] [varchar](255) NOT NULL,
	[CodZonaCotizacion] [int] NULL,
	[Domicilio] [varchar](255) NOT NULL,
	[HorarioAtencion] [varchar](255) NOT NULL,
	[codTipoSucursal] [int] NULL,
	[codProvincia] [int] NULL,
	[Latitud] [float] NULL,
	[Longitud] [float] NULL,
	[AudioNoVidentes] [bit] NULL,
	[CodigoPostal] [int] NULL,
	[EMailOficialEmpresa] [varchar](150) NULL,
	[Vigente] [bit] NOT NULL,
	[EmailOficialNYP] [varchar](100) NULL,
 CONSTRAINT [PK_Sucursales] PRIMARY KEY NONCLUSTERED 
(
	[CodSucursal] ASC
)
)
GO

ALTER TABLE [dbo].[Sucursales] ADD  DEFAULT ((1)) FOR [Vigente]
GO

ALTER TABLE [dbo].[Sucursales]  WITH CHECK ADD  CONSTRAINT [FK_Sucursales_Provincias] FOREIGN KEY([codProvincia])
REFERENCES [dbo].[Provincias] ([CodProvincia])
GO

ALTER TABLE [dbo].[Sucursales] CHECK CONSTRAINT [FK_Sucursales_Provincias]
GO

ALTER TABLE [dbo].[Sucursales]  WITH CHECK ADD  CONSTRAINT [FK_Sucursales_TiposSucursales] FOREIGN KEY([codTipoSucursal])
REFERENCES [dbo].[TiposSucursales] ([codTipoSucursal])
GO

ALTER TABLE [dbo].[Sucursales] CHECK CONSTRAINT [FK_Sucursales_TiposSucursales]
GO


select * from Provincias

set identity_insert Provincias on
insert into Provincias(CodProvincia, NomProvincia, Activa, IVA, PrimaSeguroIncendio, PrimaSeguroIncendioFSPUVA) values
(1	,'CAPITAL FEDERAL', 1,	0.210000000,	0.015000000, 0.010000000), 
(2	,'BUENOS AIRES', 1,	0.210000000,	0.015000000, 0.010000000), 
(3	,'CATAMARCA', 1,	0.210000000,	0.015000000, 0.010000000), 
(4	,'CÓRDOBA', 1,	0.210000000,	0.015000000, 0.010000000), 
(5	,'CORRIENTES', 1,	0.210000000,	0.015000000, 0.010000000), 
(6	,'CHACO', 1,	0.210000000,	0.015000000, 0.010000000), 
(7	,'CHUBUT', 1,	0.210000000,	0.015000000, 0.010000000), 
(8	,'FORMOSA', 1,	0.210000000,	0.015000000, 0.010000000), 
(9	,'ENTRE RIOS', 1,	0.210000000,	0.015000000, 0.010000000), 
(10,'JUJUY', 1,	0.210000000, 0.025000000, 0.016750000), 
(11,'LA PAMPA', 1,	0.210000000,	0.015000000, 	0.010000000), 
(12,'LA RIOJA', 1,	0.210000000,	0.025000000, 	0.016750000), 
(13,'MENDOZA', 1,	0.210000000,	0.025000000, 	0.016750000), 
(14,'MISIONES', 1,	0.210000000,	0.015000000, 	0.010000000), 
(15,'NEUQUÉN', 1,	0.210000000,	0.015000000, 0.010000000), 
(16,'RÍO NEGRO', 1,	0.210000000,	0.015000000, 0.010000000), 
(17,'SALTA', 1,	0.210000000,	0.015000000, 0.010000000), 
(18,'SAN JUAN', 1,	0.210000000,	0.025000000, 0.016750000), 
(19,'SAN LUIS', 1,	0.210000000,	0.015000000, 0.010000000), 
(20,'SANTA CRUZ', 1,	0.210000000, 0.015000000, 0.010000000), 
(21,'SANTA FE', 1,	0.210000000,	0.015000000, 0.010000000), 
(22,'SANTIAGO DEL ESTERO', 1, 0.210000000, 0.015000000, 0.010000000), 
(23,'TIERRA DEL FUEGO', 1,	0.210000000, 0.015000000, 0.010000000), 
(24,'TUCUMÁN', 1, 0.000000000, 0.015000000, 0.010000000)
set identity_insert Provincias off


select * from TiposSucursales
set identity_insert TiposSucursales on
insert into TiposSucursales ( codTipoSucursal, desTipoSucursal, Orden) values
(1	,'Sucursal', 1), 
(2	,'Dependencia', 2), 
(3	,'Oficina de Venta', 3), 
(4	,'Punto de Venta', 4), 
(5	,'Stand de Ventas', 5), 
(6	,'Local de Ventas', 6), 
(7	,'Stand de Promoción', 7), 
(8	,'Oficina de Promoción', 8), 
(9	,'Cajero Automático', 9), 
(10 ,'Filial de Ventas e Inversiones', 11)
set identity_insert TiposSucursales off

select * from Sucursales
insert into Sucursales (CodSucursal, DesSucursal, CodZonaCotizacion, Domicilio, HorarioAtencion, codTipoSucursal, codProvincia, Latitud, Longitud, AudioNoVidentes, CodigoPostal, EMailOficialEmpresa, Vigente, EmailOficialNYP) values 
(0, 'BUENOS AIRES', NULL, 'Reconquista 101 -CP 1003- Capital Federal', 'L a V de 10 a 15 hs', 1, 1, -34.6066586, -58.3722567, 1, 1003, 'nreyeeeee@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, ''), 
(1, 'BAHÍA BLANCA', NULL, 'Chiclana 417 -CP 8000- Buenos Aires', 'L a V de 10 a 15 hs', 1, 2, -38.7218253, -62.2618726, 0, 8000, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 0, 'cgimenez12345@hipotecario.com.ar'), 
(11, 'JUJUY', NULL, 'Belgrano 901 -CP 4600- Jujuy', 'L a V de 8 a 13 hs', 1, 10, -24.185406, -65.305527, 0, 4600, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'muriondo@hipotecario.com.ar'), 
(13, 'LA PLATA', NULL, 'Calle 50 N* 673 -CP 1900- Buenos Aires', 'L a V de 8 a 13 hs', 1, 2, -34.9162587, -57.9512551, 1, 1900, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'jpburgos@hipotecario.com.ar'), 
(14, 'LA RIOJA', NULL, '25 de Mayo 170 -CP 5300- La Rioja', 'L a V de 8 a 13 hs', 1, 12, -29.410829, -66.8345987, 0, 5300, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'mvaguaysol@hipotecario.com.ar'), 
(15, 'MAR DEL PLATA', NULL, 'Avda. Independencia 1701-CP 7600- Mar del Plata', 'L a V de 8 a 13 hs', 1, 2, -37.9964292, -57.5528901, 0, 7600, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'vgarciacastanon@hipotecario.com.ar'), 
(16, 'MENDOZA', NULL, 'Garibaldi 70 -CP 5500- Mendoza', 'L a V de 8 a 13 hs', 1, 13, -32.8975306, -68.8406043, 0, 5500, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'rnmaris@hipotecario.com.ar'), 
(17, 'NEUQUÉN', NULL, 'Av. Argentina 79 -CP 8300- Neuquén', 'L a V de 8 a 13 hs', 1, 15, -38.9543383, -68.0588903, 1, 8300, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'eghernandez@hipotecario.com.ar;fschultz@hipotecario.com.ar'), 
(18, 'PARANÁ', NULL, 'Urquiza 1100 -CP 3100- Paraná', 'L a V de 8 a 13 hs', 1, 9, -31.7366097, -60.5170132, 1, 3100, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrau@hipotecario.com.ar'), 
(22, 'POSADAS', NULL, 'Colón 1704 -CP 3300- Posadas', 'L a V de 8 a 13 hs', 1, 14, -27.3653203, -55.8945909, 0, 3300, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'sepereyra@hipotecario.com.ar'), 
(25, 'RESISTENCIA', NULL, 'Juan B. Justo 4 -CP 3500- Resistencia', 'L a V de 7:30 a 12:30 hs', 1, 6, -27.4528077, -58.9864716, 1, 3500, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'iibulfonfernandez@hipotecario.com.ar'), 
(27, 'ROSARIO', NULL, 'Santa Fé 1157 -CP 2000- Rosario', 'L a V de 8:15 a 13:15 hs', 1, 21, -32.9450082, -60.6386839, 1, 2000, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'ebocos@hipotecario.com.ar;mlmattheus@hipotecario.com.ar'), 
(28, 'SALTA', NULL, 'España 701 -CP 4400- Salta', 'L a V 8.30 a 13.30 hs', 1, 17, -24.7884619, -65.4123322, 0, 4400, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'mfmachargo@hipotecario.com.ar'), 
(29, 'SAN JUAN', NULL, 'Rivadavia 350 -CP 5402- San Juan', 'L a V de 8 a 13 hs', 1, 18, -31.536977, -68.531058, 0, 5402, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'psanchezlima@hipotecario.com.ar'), 
(3, 'CATAMARCA', NULL, 'República 560 -CP 4700- Catamarca', 'L a V de 8 a 13 hs', 1, 3, -28.4682545, -65.7790701, 0, 4700, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'aoromerovega@hipotecario.com.ar'), 
(30, 'SAN LUIS', NULL, 'Rivadavia 802 -CP 5702- San Luis', 'L a V de 8:30 a 13 hs', 1, 19, -33.3011306, -66.3364583, 1, 5702, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'siortega@hipotecario.com.ar'), 
(31, 'SAN RAFAEL', NULL, 'Av. Mitre 80 -CP 5600- Mendoza', 'L a V de 8 a 13 hs', 1, 13, -33.0750578, -68.4669996, 0, 5600, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(32, 'SANTA FE', NULL, 'Primera Junta 2532 -CP 3000- Santa Fe', 'L a V de 8:15 a 13:15 hs', 1, 21, -31.6480539, -60.7067144, 1, 3000, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'mrioja@hipotecario.com.ar'), 
(33, 'SANTA ROSA', NULL, 'Av. San Martín 98 -CP 6300- Santa Rosa', 'L a V de 8 a 13 hs', 1, 11, -36.6210739, -64.2899241, 0, 6300, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'lcfalkenstein@hipotecario.com.ar'), 
(34, 'SANTIAGO DEL ESTERO', NULL, 'Sarmiento 2 -CP 4200- Santiago del Estero', 'L a V de 7:45 a 12:45 hs', 1, 22, -27.4976076, -64.8618685, 0, 4200, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'gagarnica@hipotecario.com.ar'), 
(36, 'TRELEW', NULL, 'Belgrano 265 -CP 9100- Trelew', 'L a V de 8 a 13 hs', 1, 7, -43.2519907, -65.3097458, 0, 9100, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'ajyusiuk@hipotecario.com.ar'), 
(37, 'TUCUMÁN', NULL, 'San Martín 788  -CP 4000- Tucumán', 'L a V de 8:30 a 13:30 hs', 1, 24, -26.8289744, -65.2083171, 1, 4000, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'edurena@hipotecario.com.ar'), 
(38, 'FORMOSA', NULL, 'Av. 25 de Mayo 708 -CP 3600- Formosa', 'L a V de 7 a 11:45 hs', 1, 8, -25.2861828, -57.711504, 0, 3600, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'plarroza@hipotecario.com.ar'), 
(4, 'COMODORO RIVADAVIA', NULL, '9 de Julio 855 -CP 9000- Cdro. Rivadavia', 'L a V de 8 a 13 hs', 1, 7, -45.8613724, -67.4784016, 1, 9000, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'schamberger@hipotecario.com.ar'), 
(45, 'RÍO GALLEGOS', NULL, 'San Martin 801 -CP 9400- Río Gallegos', 'L a V de 8 a 13 hs', 1, 20, -51.6259363, -69.2206271, 1, 9400, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'pdnavarro@hipotecario.com.ar'), 
(46, 'USHUAIA', NULL, 'Rivadavia 141 -CP 9410- Ushuaia', 'L a V de 8 a 13 hs', 1, 23, -54.805026, -68.301426, 1, 9410, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'gpcoman@hipotecario.com.ar'), 
(51, 'VENADO TUERTO', NULL, 'San Martín 761 -CP 2600- Venado Tuerto', 'L a V de 8:15 a 13:15 hs', 1, 21, -33.7454998, -61.9658644, 0, 2600, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'gfuentes@hipotecario.com.ar'), 
(7, 'CÓRDOBA', NULL, 'San Jerónimo 82 -CP 5000- Córdoba', 'L a V de 8:30 a 13:30 hs', 1, 4, -31.4174357, -64.1834281, 0, 5000, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'mokohan@hipotecario.com.ar;sjamardo@hipotecario.com.ar'), 
(8, 'CORRIENTES', NULL, '25 de Mayo 998 -CP 3400- Corrientes', 'L a V de 7 a 11:30 hs', 1, 5, -27.4639471, -58.8390516, 1, 3400, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'anplanjar@hipotecario.com.ar'), 
(35, 'TANDIL', NULL, 'Gral. Rodriguez 726', 'L a V de 8:00 a 13:00 hs.', 1, 2, -37.3248691, -59.1376957, 0, 7000, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'laleman@hipotecario.com.ar'), 
(40, 'VIEDMA', NULL, 'Colón 461 -CP 8500- Viedma', 'L a V de 8 a 13 hs', 1, 16, -40.8100884, -62.9920866, 0, 8500, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'velizondo@hipotecario.com.ar'), 
(26, 'RÍO CUARTO', NULL, 'Constitución 818 -CP 5800- Córdoba', 'L a V de 8 a 13 hs', 1, 4, -33.1242703, -64.3496749, 0, 5800, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nomigani@hipotecario.com.ar'), 
(47, 'AVELLANEDA', NULL, 'Av. Mitre 506,C.P. 1870 - Avellaneda', 'L a V de 10 a 15 hs', 1, 2, -34.659774, -58.3686192, 0, 1870, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 0, 'mdutria@hipotecario.com.ar'), 
(52, 'ALMAGRO', NULL, 'Corrientes 3820-CP 1194 - Capital Federal', 'L a V de 10 a 15 hs', 1, 1, -34.6034331, -58.4182957, 0, 1194, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'marende@hipotecario.com.ar'), 
(816, 'HIPERMERCADO LIBERTAD - CÓRDOBA JACINTO RÍOS', NULL, 'Libertad 1100 -CP 5000- Córdoba', 'L a D de 9 a 22 hs.', 5, 4, -31.4090895, -64.1703522, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(817, 'HIPERMERCADO LIBERTAD - CÓRDOBA RUTA 9', NULL, 'Av. Sabattini 3250 -CP 5014- Córdoba', 'L a D de 9 a 22 hs.', 5, 4, -31.433162, -64.142335, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(821, 'HIPERMERCADO LIBERTAD - TUCUMÁN I', NULL, 'Av Roca 3440 -CP 4000- Tucumán', 'L a D de 9 a 22 hs.', 5, 24, -26.8330944, -65.2674717, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(822, 'HIPERMERCADO LIBERTAD -TUCUMÁN II', NULL, 'Emilio Castelar y Suipacha -CP 4000- Tucumán', 'L a D de 9 a 22 hs.', 5, 24, -26.7954, -65.2088, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(823, 'HIPERMERCADO LIBERTAD - SALTA', NULL, 'Av. Tavella y Av.Ex Com.de Malvinas -CP 4400- Salta', 'L a D de 9 a 22 hs.', 5, 17, -24.8302, -65.4307, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(824, 'HIPERMERCADO LIBERTAD - SANTIAGO DEL ESTERO', NULL, 'Autopista Juan D. Perón s/n -CP 4300- Santiago del Estero', 'L a D de 9 a 22 hs.', 5, 22, -27.77657, -64.254079, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(818, 'HIPERMERCADO LIBERTAD - MENDOZA I', NULL, 'Av. Joaquín V.Gonzalez y Cipolletti -CP 5501- Godoy Cruz', 'L a D de 9 a 22 hs.', 5, 13, -32.9372024, -68.8583842, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(64, 'BELGRANO', NULL, 'Av. Cabildo 2971 - CP 1429', 'L a V de 10 a 15hs.', 1, 1, -34.554739, -58.462987, 0, 1429, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(820, 'HIPERMERCADO LIBERTAD - SAN JUAN', NULL, 'Scalabrini Ortiz y Circunvalación -CP 5400- San Juan', 'L a D de 9 a 22 hs.', 5, 18, -31.52052, -68.5409, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(815, 'HIPERMERCADO LIBERTAD - CÓRDOBA POETA LUGONES', NULL, 'Cardeñosa y Fray L. Beltrán s/n -CP 5008- Córdoba', 'L a D de 10 a 22 hs.', 5, 4, -31.36552, -64.21781, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(825, 'HIPERMERCADO LIBERTAD - ROSARIO', NULL, 'Bv. Otoño y Av. Battle y Ordoñez -CP 2000- Rosario', 'L a D de 10 a 22 hs.', 5, 21, -33.00923, -60.665, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(826, 'HIPERMERCADO LIBERTAD - RESISTENCIA', NULL, 'Ruta Nac. N* 16 Nicolás Avellaneda y Av. Dr. Sabín -CP 3500- Resistencia', 'L a D de 10 a 22 hs.', 5, 6, -27.4126, -58.9675, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(827, 'HIPERMERCADO LIBERTAD - POSADAS', NULL, 'Av. Quaranta y Tomás Guido -CP 3300- Posadas', 'L a D de 10 a 22 hs.', 5, 14, -27.40093, -55.9167, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(24, 'RECONQUISTA', NULL, 'San Martín 801 -CP 3560- Santa Fe', 'L a V 7:15 a 12:15 hs.', 1, 21, -33.2251501, -60.3326486, 0, 3560, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'lrpereyra@hipotecario.com.ar'), 
(834, 'HIPERMERCADO LIBERTAD - VILLA MARÍA', NULL, 'Ruta Nacional 158 - Km. 155,5 - CP 5900- Córdoba', 'L a D de 10 a 22 hs', 5, 4, -32.409965, -63.262242, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(56, 'VILLA MARÍA', NULL, 'Buenos Aires 1148 -CP 5900- Córdoba', 'L a V de 8 a 13hs', 1, 4, -32.4109551, -63.2431969, 0, 5900, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'mabustamante@hipotecario.com.ar'), 
(43, 'SAN FRANCISCO', NULL, 'Blvd. 25 de Mayo 1540 -CP 2400- Córdoba', 'L a V 8 a 13 hs.', 1, 4, -31.4298592, -62.0833076, 0, 2400, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'mafilippa@hipotecario.com.ar'), 
(57, 'CERRO DE LAS ROSAS', NULL, 'Rafael Núñez 4230 -CP 5009,Córdoba', 'L a V de 8.30 a 13.30hs.', 1, 4, -31.3683603, -64.2331976, 1, 5009, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'cpaz@hipotecario.com.ar'), 
(65, 'LOMAS DE ZAMORA', NULL, 'Av. Hipólito Yrigoyen N* 9205/09 esquina Loria N* 402 y S/N - CP 1832 - Lomas de Zamora', 'L a V de 10 a 15hs.', 1, 2, -34.7639008, -58.4032551, 1, 1832, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'mbsanchez@hipotecario.com.ar'), 
(6, 'CONCORDIA', NULL, 'Carlos Pellegrini 699 - E3200AMK - Concordia', 'L a V de 8 a 13hs', 1, 9, -31.396121, -58.016402, 0, 3200, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'sfrossard@hipotecario.com.ar'), 
(12, 'JUNÍN', NULL, 'Saavedra 10 esq. Arias - CP 6000 - Buenos Aires', 'L a V de 8 a 13 hs', 1, 2, -34.593, -60.9449, 0, 6000, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'dfdiviesti@hipotecario.com.ar'), 
(61, 'SAN MARTÍN', NULL, 'Mitre 3743,San Martín - CP 1650 - Buenos Aires', 'L a V de 10 a 15 hs', 1, 2, -34.57698, -58.53773, 0, 1650, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'rgavalos@hipotecario.com.ar'), 
(62, 'LUJAN', NULL, 'San Martín 161 - CP 6700 - Buenos Aires', 'L a V de 8 a 13 hs', 1, 2, -34.5643289, -59.1198128, 0, 6700, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'pdgaitan@hipotecario.com.ar'), 
(49, 'SAN JUSTO', NULL, 'Hip. Irigoyen 2350 -CP 1754- Buenos Aires', 'L a V 10 a 15 hs.', 1, 2, -34.6773755, -58.5597321, 1, 1754, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'irperez@hipotecario.com.ar'), 
(55, 'SAN ISIDRO', NULL, '25 de Mayo 538 -CP 1642- Buenos Aires', 'L a V de 10 a 15hs.', 1, 2, -34.4677043, -58.5113907, 0, 1642, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(60, 'LANÚS', NULL, 'Avda. Hip. Yrigoyen 4619/4623 -CP 1824- Buenos Aires', 'L a V de 10 a 15 hs.', 1, 2, -34.7085008, -58.3918304, 1, 1824, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3285, 'ALMAGRO', NULL, 'Av. Corrientes 3820', '24 hs', 9, 1, -34.6034331, -58.4182957, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3280, 'AVELLANEDA', NULL, 'Av. Mitre 421', '24 hs', 9, 2, -34.659774, -58.3686192, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(23, 'RAFAELA', NULL, 'Bv. Santa Fe 143 -CP 2300', 'L a V de 7:15 a 12:15hs', 1, 21, -31.252763, -61.490374, 0, 2300, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'enyuste@hipotecario.com.ar'), 
(21, 'PERGAMINO', NULL, 'San Nicolás 750 -CP 2700- Bs.As.', 'L a V de 8 a 13hs', 1, 2, -33.8961826, -60.5738612, 1, 2700, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3254, 'BAHIA BLANCA', NULL, 'Chiclana 417 - Bahia Blanca', '24 hs', 9, 2, -38.7218253, -62.2618726, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3350, 'BARRIO NORTE', NULL, 'Av. Santa Fe 1883', '24 hs', 9, 1, -34.5958143, -58.3944536, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3359, 'BELGRANO', NULL, 'Avda. Cabildo 2412/16', '24 hs', 9, 1, -34.5591426, -58.4591912, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2626, 'CASA CENTRAL', NULL, 'Reconquista 151', '24 hs', 9, 1, -34.6060729, -58.3723006, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3266, 'CATAMARCA', NULL, 'República 560', '24 hs', 9, 3, -28.4682545, -65.7790701, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3255, 'COMODORO RIVADAVIA', NULL, '9 de Julio 820/880', '24 hs', 9, 7, -45.8619199, -67.478402, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(833, 'HML RIVERA INDARTE', NULL, 'Ricardo Rojas y Manuel de Falla s/ número (Local 120) - CP 5149 - Córdoba', 'L a D de 10 a 22 hs.', 5, 4, -31.311408, -64.275719, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(71, 'SAN MIGUEL', NULL, 'Pte. Juan Domingo Perón 1075 - B1629AXG', 'L a V 10 a 15 hs', 1, 2, -34.546191, -58.706284, 0, 1629, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(63, 'TIGRE', NULL, 'Av. Cazón 1383 - CP 1648 - Buenos Aires', 'L a V 10 a 15 hs', 1, 2, -34.424204, -58.578332, 0, 1648, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'yblanco@hipotecario.com.ar'), 
(2603, 'CORDOBA', NULL, 'San Jeronimo 82', '24 hs', 9, 4, -31.4174357, -64.1834281, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3201, 'HIPERMERCADO LIBERTAD', NULL, 'Fray Luis Beltran y Cardeñosa S/N§', '9 a 22 hs', 9, 4, -31.36552, -64.21781, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(54, 'MORÓN', NULL, 'Ntra Sra del Buen Viaje 835 -CP 1708- Bs.As.', 'L a V de 10 a 15 hs', 1, 2, -34.6517434, -58.6203203, 0, 1708, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'msgarcia@hipotecario.com.ar'), 
(3202, 'HIPERMERCADO LIBERTAD', NULL, 'Libertad 1100', '9 a 22 hs', 9, 4, -31.4090895, -64.1703522, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(53, 'QUILMES', NULL, 'Nicolás Videla 127 -CP 1878- Bs.As.', 'L a V de 10 a 15hs', 1, 2, -34.7213013, -58.2597817, 1, 1878, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'arusso@hipotecario.com.ar'), 
(59, 'TRIBUNALES', NULL, 'Paraná 645 -C1017AAN- Capital Federal', 'L a V de 10 a 15 hs', 1, 1, -34.601124, -58.387993, 1, 1017, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 0, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3271, 'CORDOBA', NULL, 'San Jeronimo 83', '24 hs', 9, 4, -31.4174357, -64.1834281, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3292, 'CERRO DE LAS ROSAS', NULL, 'Av. Rafael Nuñez 4230', '24 hs', 9, 4, -31.3683603, -64.2331976, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2611, 'CORRIENTES', NULL, '25 de Mayo 998', '24 hs', 9, 5, -27.4639471, -58.8390516, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(99, 'CASA CENTRAL', NULL, 'Reconquista 101 -CP 1003- Capital Federal', 'L a V de 10 a 15 hs', 1, 1, -34.6066586, -58.3722567, 0, 1003, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3260, 'FORMOSA', NULL, 'Av. De Mayo 700', '24 hs', 9, 8, -25.2861828, -57.711504, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3268, 'JUJUY', NULL, 'Belgrano 901', '24 hs', 9, 10, -24.185406, -65.305527, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3289, 'JUNIN', NULL, 'Savedra esq. Arias', '24 hs', 9, 2, -34.593, -60.9449, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2604, 'LA RIOJA', NULL, 'San Nicolas De Bari 785', '24 hs', 9, 12, -29.410829, -66.8345987, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3281, 'LANUS', NULL, 'Av Hipólito Yrigoyen 4619/23', '24 hs', 9, 2, -34.7085008, -58.3918304, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3297, 'LUJAN', NULL, 'San Martín 161', '24 hs', 9, 2, -34.5643289, -59.1198128, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3283, 'MAR DEL PLATA', NULL, 'Av Independencia 1701 UF 1 Y 2', '24 hs', 9, 2, -37.9964292, -57.5528901, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2619, 'MENDOZA', NULL, 'Sarmiento y España', '24 hs', 9, 13, -32.8906204, -68.8420677, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3203, 'MORON', NULL, 'Nuestra Señora del Buen Viaje 835/937', '24 hs', 9, 2, -34.6517434, -58.6203203, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3258, 'NEUQUEN', NULL, 'Av. Argentina 79', '24 hs', 9, 15, -38.9543383, -68.0588903, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2615, 'PARANA', NULL, 'Urquiza 1092', '24 hs', 9, 9, -31.7366097, -60.5170132, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3212, 'PERGAMINO', NULL, 'Peatonal San Nicolas 740', '24 hs', 9, 2, -33.8961826, -60.5738612, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3354, 'POLICIA LA RIOJA', NULL, 'Av. Pte. Peron 1275', '24 hs', 9, 12, -29.4128475, -66.8622818, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2636, 'POSADAS', NULL, 'Colon 1702. Esquina San martin.', '24 hs', 9, 14, -27.3659476, -55.8946675, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3205, 'QUILMES', NULL, 'Nicolás Videla 127', '24 hs', 9, 2, -34.7213013, -58.2597817, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3299, 'RAFAELA', NULL, 'Bvb. Santa Fe 143', '24 hs', 9, 21, -31.252965, -61.4902707, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3282, 'RECONQUISTA', NULL, 'San Martín 801', '24 hs', 9, 21, -33.2251501, -60.3326486, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3269, 'RESISTENCIA', NULL, 'Juan B.Justo 4', '24 hs', 9, 6, -27.4528077, -58.9864716, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3274, 'RIO CUARTO', NULL, 'Constitución 818 - Rio IV', '24 hs', 9, 4, -33.1242703, -64.3496749, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2623, 'RIO GALLEGOS', NULL, 'San Martín y Libertad', '24 hs', 9, 20, -51.6232, -69.2174, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2627, 'ROSARIO', NULL, 'Santa Fe 1157', '24 hs', 9, 21, -32.9450082, -60.6386839, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2632, 'SALTA', NULL, 'España 701 esq. Balcarce', '24 hs', 9, 17, -24.7884619, -65.4123322, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3296, 'SAN FRANCISCO', NULL, 'Av. 25 de Mayo 1540', '24 hs', 9, 4, -31.4298592, -62.0833076, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3295, 'SAN ISIDRO', NULL, '25 de Mayo 538', '24 hs', 9, 2, -34.4677043, -58.5113907, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2620, 'SAN JUAN', NULL, 'Rivadavia Este 350 / 54  San Juan', '24 hs', 9, 18, -31.536977, -68.531058, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3290, 'SAN JUSTO', NULL, 'Hipólito Yrigoyen 2350', '24 hs', 9, 2, -34.6773755, -58.5597321, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3264, 'SAN LUIS', NULL, 'Rivadavia 820', '24 hs', 9, 19, -33.3011306, -66.3364583, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3200, 'SAN MARTIN', NULL, 'Mitre 3743', '24 hs', 9, 2, -34.57698, -58.53773, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3275, 'SAN RAFAEL', NULL, 'Av. Mitre 80,San Rafael', '24 hs', 9, 13, -34.6166511, -68.3287367, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2614, 'SANTA FE', NULL, 'Primera Junta 2532', '24 hs', 9, 21, -31.6480539, -60.7067144, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2616, 'SANTA ROSA', NULL, 'Av. San Martin 98 Santa Rosa', '24 hs', 9, 11, -36.6210739, -64.2899241, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2607, 'SANTIAGO DEL ESTERO', NULL, 'Sarmiento 2', '24 hs', 9, 22, -27.4976076, -64.8618685, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3358, 'TANDIL', NULL, 'Gral. Rodriguez 726', '24 hs', 9, 2, -37.3248691, -59.1376957, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3276, 'TRELEW', NULL, 'Belgrano 261 / 265', '24 hs', 9, 7, -43.2516432, -65.3083521, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3252, 'TUCUMAN', NULL, 'San Martin 766', '24 hs', 9, 24, -26.8289744, -65.2083171, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(2624, 'USHUAIA', NULL, 'Rivadavia 141 - CP 9410', '24hs', 9, 23, -54.805026, -68.301426, 1, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3277, 'VENADO TUERTO', NULL, 'San Martín 761 Esq. 25 de Mayo', '24 hs', 9, 21, -33.7454998, -61.9658644, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3262, 'VIEDMA', NULL, 'Colón 461', '24 hs', 9, 16, -40.8100884, -62.9920866, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3211, 'VILLA MARIA', NULL, 'Buenos Aires 1148', '24 hs', 9, 4, -32.4109551, -63.2431969, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3360, 'WALMART LAFERRERE', NULL, 'Ruta 21 y Carlos Casares', '8 a 22 hs', 9, 2, -34.7417, -58.5679, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3361, 'WALMART VIEDMA', NULL, 'Tierra del Fuego 514,entre calle Colectora de Av. Cardenal Cagliero y Corrientes', '8 a 22 hs', 9, 16, -40.8246566, -62.9850273, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3373, 'CHANGO MAS LOMAS', NULL, 'Cerrito. Esquina Rubén Darío', '8 a 22 hs', 9, 2, -34.75725, -58.3787, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3374, 'WALMART FORMOSA', NULL, 'Av. Maradona. Esquina Somacal', '8 a 23 hs', 9, 8, -26.1526813, -58.1643486, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3375, 'CONCORDIA', NULL, 'Carlos Pellegrini 699,esq. Alberdi', '24 hs', 9, 9, -31.396121, -58.016402, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3376, 'SAN MIGUEL', NULL, 'Perón 1075', '24 hs', 9, 2, -34.546191, -58.706284, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3377, 'TIGRE', NULL, 'Av. Cazón 1383', '24 hs', 9, 2, -34.424204, -58.578332, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(835, 'HIPERMERCADO LIBERTAD - RAFAELA', NULL, 'Conscripto Zurbriggen 865 - CP S2300 - Rafaela', 'L a S de 9 a 22hs / D y Feriados 8 a 13hs', 5, 21, -31.252158, -61.491598, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(76, 'YERBA BUENA', NULL, 'Av. Aconquija 725 e/ Oestes y las Rosas - CP 4107', 'L a V de 8:30 a 13:30 hs', 1, 24, -26.816047, -65.276577, 0, 4107, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'msilva@hipotecario.com.ar'), 
(44, 'GENERAL PICO', NULL, 'Calle 13 N° 1053 e/ 11 y 15. CP 6360', 'L a V de 8 a 13 hs', 1, 11, -35.658203, -63.753834, 0, 6360, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(7600, 'YERBA BUENA', NULL, 'Av. Aconquija 725 e/ Venezuela y F. Rossi. CP 4107', 'L a V de 8:30 a 13:30hs', 9, 24, -26.816277, -65.276599, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(69, 'VILLA URQUIZA', NULL, 'Av. Triunvirato 4279/81/83 - C1431FBF', 'L a V de 10 a 15hs', 1, 1, -34.577627, -58.481065, 0, 1431, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'jvit@hipotecario.com.ar'), 
(70, 'FLORES', NULL, 'Av. Rivadavia 7270 - C1406GMP', 'L a V de 10 a 15hs', 1, 1, -34.6301, -58.46774, 0, 1406, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3380, 'GENERAL PICO', NULL, 'Calle 13 N° 1053 - CP 6360', 'Las 24hs', 9, 11, -35.65836, -63.753773, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3379, 'PODER LEGISLATIVO', NULL, 'Vélez Sarfield 874', 'L a V de 6 a 23hs', 9, 12, -29.416143, -66.859964, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3381, 'YERBA BUENA', NULL, 'Av. Aconquija 725 - CP 4107', 'Las 24hs', 9, 24, -26.816239, -65.276474, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3382, 'WALMART FLORES', NULL, 'Ramón Falcón 2452', 'De 8 a 22hs', 9, 1, -34.640393, -58.530031, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3383, 'WALMART POSADAS', NULL, 'Ruta 213 y Av. Cabo de Hornos,Posadas.', 'De 8 a 22hs', 9, 14, -27.36323, -55.893685, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3385, 'WALMART SANTA ROSA', NULL, 'Av. Pres. Perón y Ultracan,Santa Rosa.', 'De 8 a 22hs', 9, 11, -36.634299, -64.314572, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3386, 'WALMART COMODORO RIVADAVIA', NULL, 'Av. Tehuelches y Ruta3,Comodoro Rivadavia', 'De 8 a 22hs', 9, 7, -45.862759, -67.497994, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3387, 'WALMART ROQUE SAENZ PEÑA', NULL, 'Juan M de Rosas (101) y calle Arturo Illia (128). R. S. Peña', 'De 8 a 22hs', 9, 6, -27.425395, -59.00079, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3388, 'VILLA URQUIZA', NULL, 'Av. Triunvirato 4279/81/83', 'Las 24hs', 9, 1, -34.577521, -58.481086, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(3389, 'FLORES', NULL, 'Av. Rivadavia 7270', 'Las 24hs', 9, 1, -34.630048, -58.467761, 0, NULL, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar'), 
(82, 'MORENO', NULL, 'Av del Libertador 483 CP: B1744AAE,localidad Moreno', 'L a V de 10 a 15 hs', 1, 2, -34.6444922, -58.7894812, 0, 1744, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'memular@hipotecario.com.ar'), 
(83, 'PILAR', NULL, 'Pedro Lagrave 523 CP: B1629HGL,localidad Pilar,Buenos Aires', 'L a V de 10 a 15 hs .', 1, 2, -34.4569029, -58.9122326, 0, 1629, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'flizaso@hipotecario.com.ar'), 
(73, 'MONTE GRANDE', NULL, 'Sofía T. de Santamarina 503 - CP 1842 - Monte Grande', 'L a V de 10 a 15 hs.', 1, 2, -34.819925, -58.466717, 0, 1842, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'msabbatella@hipotecario.com.ar'), 
(77, 'SAN NICOLÁS', NULL, 'Bartolomé Mitre 179/181,C.P 2900,San Nicolás de los Arroyos', 'L a V de 8 a 13 hs', 1, 2, -33.3299032, -60.2205081, 0, 2900, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 1, 'hbiaggini@hipotecario.com.ar'), 
(81, 'RAMOS MEJÍA', NULL, 'Avenida de Mayo 595', 'L a V 10 a 15 Hs.', 1, 2, -34.645988, -58.564542, 0, 1704, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar', 0, 'nrey@hipotecario.com.ar;fmartinetti@hipotecario.com.ar')

select * from Sucursales