using FluentNHibernate.Mapping;

namespace BH.EPotecario.Adm.Dominio
{
    public class MedioDePago
    {
        public virtual int CodMedioDePago { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Slug { get; set; }
    }

    public class MedioDePagoMap : ClassMap<MedioDePago>
    {
        public MedioDePagoMap()
        {
            // Mapeo de la tabla
            Table("BEN_MedioDePago");

            // Mapeo de la clave primaria
            Id(a => a.CodMedioDePago).GeneratedBy.Identity();

            // Mapeo de las propiedades
            Map(a => a.Nombre).Length(50).Not.Nullable();
            Map(a => a.Slug).Length(60).Unique();

            // Habilitar caché de segundo nivel
            // Cache.ReadOnly();
        }
    }
}