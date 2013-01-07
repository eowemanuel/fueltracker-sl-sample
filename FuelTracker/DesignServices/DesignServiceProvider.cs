using FuelTracker.DesignServices;

namespace FuelTracker.Services
{

    public class DesignServiceProvider : ServiceProviderBase
    {
        public DesignServiceProvider()
        {
            //PageConductor = new DesignPageConductor(); //Uncomment if you are using DesignPageConductor

            //Example
            //ModelTypeDataService = new DesignModelTypeDataService();

            CarDataService = new DesignCarDataService();
            UserProfileDataService = new DesignUserProfileDataService();
        }
    }
}
