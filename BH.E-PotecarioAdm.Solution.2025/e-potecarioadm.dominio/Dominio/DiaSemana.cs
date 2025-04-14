using FluentNHibernate.Mapping;

namespace BH.EPotecario.Adm.Dominio
{
	public class DiaSemana
	{
		public virtual int CodDiaSemana { get; set; }
		public virtual string Nombre { get; set; }
		public virtual string Slug { get; set; }
	}

	public class DiaSemanaMap : ClassMap<DiaSemana>
	{
		public DiaSemanaMap()
		{
			// Mapeo de la tabla
			Table("DiaSemana");

			// Mapeo de la clave primaria
			Id(a => a.CodDiaSemana).GeneratedBy.Identity();

			// Mapeo de las propiedades
			Map(a => a.Nombre).Length(50).Not.Nullable();
			Map(a => a.Slug).Length(60).Unique();

			// Habilitar caché de segundo nivel
			// Cache.ReadOnly();
		}
	}
}