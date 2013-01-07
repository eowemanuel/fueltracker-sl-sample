using Microsoft.Windows.Data.DomainServices;
using System.Collections.Generic;
using FuelTracker.Web;
using System.ComponentModel;
using FuelTracker.DesignModel;

namespace FuelTracker.DesignServices
{
    public class DesignCarsLoader : CollectionViewLoader
    {
        public static void LoadSource()
        {
            var generatedSource = new DesignCars();
            source = generatedSource;
        }

        private static IEnumerable<Car> source;

        public IEnumerable<Car> Source
        {
            get
            {
                if (DesignCarsLoader.source == null)
                {
                    LoadSource();
                }
                return DesignCarsLoader.source;
            }
        }

        public override bool CanLoad { get { return true; } }
        public override void Load(object userState)
        {
            this.OnLoadCompleted(new AsyncCompletedEventArgs(null, false, userState));
        }
    }
}
