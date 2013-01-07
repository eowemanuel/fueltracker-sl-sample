namespace FuelTracker.Services
{
    public class ServiceProvider : ServiceProviderBase
    {
        public ServiceProvider()
        {
            PageConductor = new PageConductor();
        }

        //Example:
        //public override IModelTypeDataService ModelTypeDataService
        //{
        //    get { return new ModelTypeDataService(); }
        //}

        public override ICarDataService CarDataService
        {
            get { return new CarDataService(); }
        }

        public override IUserProfileDataService UserProfileDataService
        {
            get { return new UserProfileDataService(); }
        }


    }
}