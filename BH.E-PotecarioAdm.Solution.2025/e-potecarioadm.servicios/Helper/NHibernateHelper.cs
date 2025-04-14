using BH.EPotecario.Adm.Dominio;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Configuration;
using System.Diagnostics;

namespace BH.EPotecario.Adm.Servicios
{
	public class NHibernateHelper
	{
		private static ISessionFactory _sessionFactory;

		public static ISessionFactory CreateSessionFactory()
		{
			try
			{
				string _connectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

				if (_sessionFactory == null)
				{
					_sessionFactory = Fluently.Configure()
						.Database(
							MsSqlConfiguration
								.MsSql2012
								.ConnectionString(_connectionString)
						)
						/*.Cache(c => c
							.UseSecondLevelCache() // Habilitar caché de segundo nivel
							.ProviderClass<NHibernate.Caches.CoreMemoryCache.CoreMemoryCacheProvider>())*/
						.Mappings(m =>
							m.FluentMappings
								//.AddFromAssemblyOf<AhorroMap>()
								//.AddFromAssemblyOf<CategoriaMap>()
								.AddFromAssemblyOf<TipoClienteMap>()
								//.AddFromAssemblyOf<CuotaMap>()
								//.AddFromAssemblyOf<DiaSemanaMap>()
								//.AddFromAssemblyOf<LocalidadMap>()
								//.AddFromAssemblyOf<MedioDePagoMap>()
								//.AddFromAssemblyOf<ProvinciaMap>()
								//.AddFromAssemblyOf<BeneficioMap>()
								//.|AddFromAssemblyOf<AlianzaMap>()
						)
						.BuildSessionFactory();
				}
				return _sessionFactory;
			}
			catch (Exception ex)
			{
				Trace.WriteLine($"Error: {ex.Message}");
				Trace.WriteLine($"Detalles: {ex.InnerException}");
			}
			return null;
		}
	}

}