using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.DomainServices.Client;

namespace FuelTracker.Models
{
    public class Master
    {
        public int MasterId { get; set; }
        public int Name { get; set; }
        public IEnumerable<Ninja> Ninjas { get; set; }
    }
}
