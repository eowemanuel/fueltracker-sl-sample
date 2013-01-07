
namespace FuelTracker.Models
{
    public class Ninja
    {
        public int NinjaId { get; set; }
        public int Name { get; set; }
        public Master Master { get; set; }
        public int MasterId { get; set; }
    }
}
