using System.ComponentModel;

namespace FuelTracker.Services
{
    public abstract class ServiceProviderBase
    {
        public virtual IPageConductor PageConductor { get; protected set; }

        //Example:
        //public virtual IModelTypeDataService ModelTypeDataService { get; protected set; }
        public virtual ICarDataService CarDataService { get; protected set; }
        public virtual IUserProfileDataService UserProfileDataService { get; protected set; }

        

        private static ServiceProviderBase _instance;
        public static ServiceProviderBase Instance
        {
            get 
            {
                if (_instance == null)
                {
                    CreateInstance(); 
                }
                return _instance;
            }
        }

        static ServiceProviderBase CreateInstance()
        {
            if (DesignerProperties.IsInDesignTool)
            {
                _instance = new DesignServiceProvider();
            }
            else
            {
                _instance = new ServiceProvider();
            }
            return _instance;

        }
    }
}