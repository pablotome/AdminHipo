using FluentNHibernate.Mapping;

namespace BH.EPotecario.Adm.Dominio
{
	public class Ahorro
	{
		public virtual int CodAhorro { get; set; }
		public virtual string Nombre { get; set; }
		public virtual string Slug { get; set; }
	}

	public class AhorroMap : ClassMap<Ahorro>
	{
		public AhorroMap()
		{
			// Mapeo de la tabla
			Table("Ahorro");

			// Mapeo de la clave primaria
			Id(a => a.CodAhorro).GeneratedBy.Identity();

			// Mapeo de las propiedades
			Map(a => a.Nombre).Length(50).Not.Nullable();
			Map(a => a.Slug).Length(60).Unique();

			// Habilitar caché de segundo nivel
			// Cache.ReadOnly();
		}
	}
}