using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore_HW4.Models
{
    public class Container
    {
        public virtual long Id { get; set; }
        public virtual string ContainerName { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
        public virtual long VehicleId { get; set; }

    }
}
