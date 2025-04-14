using BH.EPotecario.Adm.Dominio;
using MathNet.Numerics.Distributions;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BH.EPotecario.Adm.Servicios
{
    public interface IRepositorioParametria
    {
        List<TipoCliente> TiposClientes();
        List<MedioDePago> MediosDePago();
        List<DiaSemana> DiasSemana();
        List<Ahorro> Ahorros();
        List<Cuota> Cuotas();
    }

    public class RepositorioParametria : IRepositorioParametria
    {
        private readonly ObjectCache Cache = MemoryCache.Default;

        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30); // Duración del caché

        public List<T> GetOrAddToCache<T>(string key, Func<List<T>> fetchFunction)
        {
            // Verificar si los datos ya están en caché
            if (Cache[key] != null)
            {
                return (List<T>)Cache[key];
            }

            // Si no está en caché, obtener los datos
            var data = fetchFunction();

            // Agregar los datos al caché con tiempo de expiración
            Cache.Add(key, data, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.Add(_cacheDuration) });

            return data;
        }

        public List<TipoCliente> TiposClientes()
        {
            return GetOrAddToCache<TipoCliente>("TiposClientes", () => TiposClientesDesdeBD());
        }

        protected List<TipoCliente> TiposClientesDesdeBD()
        {
            using (var session = NHibernateHelper.CreateSessionFactory().OpenSession())
            {
                return session.Query<TipoCliente>().ToList();
            }
        }

        public List<MedioDePago> MediosDePago()
        {
            return GetOrAddToCache<MedioDePago>("MediosPago", () => MediosDePagoDesdeBD());
        }

        public List<MedioDePago> MediosDePagoDesdeBD()
        {
            using (var session = NHibernateHelper.CreateSessionFactory().OpenSession())
            {
                return session.Query<MedioDePago>().ToList();
            }
        }

        public List<DiaSemana> DiasSemana()
        {
            return GetOrAddToCache<DiaSemana>("DiasSemana", () => DiasSemanaDesdeBD());
        }

        public List<DiaSemana> DiasSemanaDesdeBD()
        {
            using (var session = NHibernateHelper.CreateSessionFactory().OpenSession())
            {
                return session.Query<DiaSemana>().ToList();
            }
        }

        public List<Ahorro> Ahorros()
        {
            return GetOrAddToCache<Ahorro>("Ahorros", () => AhorrosDesdeBD());
        }

        public List<Ahorro> AhorrosDesdeBD()
        {
            using (var session = NHibernateHelper.CreateSessionFactory().OpenSession())
            {
                return session.Query<Ahorro>().ToList();
            }
        }

        public List<Cuota> Cuotas()
        {
            return GetOrAddToCache<Cuota>("Cuotas", () => CuotasDesdeBD());
        }

        public List<Cuota> CuotasDesdeBD()
        {
            using (var session = NHibernateHelper.CreateSessionFactory().OpenSession())
            {
                return session.Query<Cuota>().ToList();
            }
        }



        /*
        public static List<Categoria> Categorias()
        {
            using (var session = NHibernateHelper.CreateSessionFactory().OpenSession())
            {
                return session.Query<Categoria>().ToList();
            }
        }



        public static List<Cuota> Cuotas()
        {
            using (var session = NHibernateHelper.CreateSessionFactory().OpenSession())
            {
                return session.Query<Cuota>().ToList();
            }
        }

        public static List<Localidad> Localidades()
        {
            using (var session = NHibernateHelper.CreateSessionFactory().OpenSession())
            {
                return session.Query<Localidad>().ToList();
            }
        }

        public static List<Provincia> Provincias()
        {
            using (var session = NHibernateHelper.CreateSessionFactory().OpenSession())
            {
                return session.Query<Provincia>().ToList();
            }
        }

        public static List<Beneficio> Beneficios()
        {
            using (var session = NHibernateHelper.CreateSessionFactory().OpenSession())
            {
                var codigos = session.Query<Beneficio>().Select(b => b.CodBeneficio).ToList();
                return codigos.Select(codigo => new Beneficio { CodBeneficio = codigo }).ToList();
            }
        }

        public static List<Alianza> Alianzas()
        {
            using (var session = NHibernateHelper.CreateSessionFactory().OpenSession())
            {
                var codigos = session.Query<Alianza>().Select(b => b.CodAlianza).ToList();
                return codigos.Select(codigo => new Alianza { CodAlianza = codigo }).ToList();
            }
        }

        public static void RefrescarCache()
        {
            foreach (var key in Cache.Select(item => item.Key).ToList())
            {
                Cache.Remove(key);
            }
        }*/
    }
}
