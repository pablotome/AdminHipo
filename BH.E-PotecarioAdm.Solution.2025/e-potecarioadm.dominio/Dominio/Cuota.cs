using FluentNHibernate.Mapping;

namespace BH.EPotecario.Adm.Dominio
{
	public class Cuota
	{
		public virtual int CodCuota { get; set; }
		public virtual string Nombre { get; set; }
		public virtual string Slug { get; set; }
	}

	public class CuotaMap : ClassMap<Cuota>
	{
		public CuotaMap()
		{
			// Mapeo de la tabla
			Table("Cuota");

			// Mapeo de la clave primaria
			Id(a => a.CodCuota).GeneratedBy.Identity();

			// Mapeo de las propiedades
			Map(a => a.Nombre).Length(50).Not.Nullable();
			Map(a => a.Slug).Length(60).Unique();

			// Habilitar caché de segundo nivel
			// Cache.ReadOnly();
		}
	}
}