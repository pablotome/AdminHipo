using FluentNHibernate.Mapping;

namespace BH.EPotecario.Adm.Dominio
{
    public class TipoCliente
    {
        public virtual int CodTipoCliente { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Slug { get; set; }
    }

    public class TipoClienteMap : ClassMap<TipoCliente>
    {
        public TipoClienteMap()
        {
            // Mapeo de la tabla
            Table("TipoCliente");

            // Mapeo de la clave primaria
            Id(a => a.CodTipoCliente).GeneratedBy.Identity();

            // Mapeo de las propiedades
            Map(a => a.Nombre).Length(50).Not.Nullable();
            Map(a => a.Slug).Length(60).Unique();

            // Habilitar caché de segundo nivel
            // Cache.ReadOnly();
        }
    }
}