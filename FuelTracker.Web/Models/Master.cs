using System.Data.Objects.DataClasses;

namespace FuelTracker.Models
{
    public class Master
    {
        public int MasterId { get; set; }
        public int Name { get; set; }
        public EntityCollection<Ninja> Ninjas { get; set; }
    }
}
