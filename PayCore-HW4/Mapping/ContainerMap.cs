using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PayCore_HW4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore_HW4.Mapping
{
    public class ContainerMap:ClassMapping<Container>
    {
        public ContainerMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int64); // long tipinde maplenir
                x.UnsavedValue(0);
                x.Generator(Generators.Increment); // id auto increment olucak şekilde maplenir.
            });
            Property(b => b.ContainerName, x =>
            {
                x.Length(50); 
                x.Type(NHibernateUtil.String); 

            });
            Property(b => b.VehicleId, x =>
            {
                x.Type(NHibernateUtil.Int64); 

            });
            Property(b => b.Latitude, x =>
            {
                x.Precision(10);
                x.Scale(6);
                x.Type(NHibernateUtil.Double);
            });
            Property(b => b.Longitude, x =>
            {
                x.Precision(10);
                x.Scale(6);
                x.Type(NHibernateUtil.Double);
            });
            Table("Container");
        }
    }
}
