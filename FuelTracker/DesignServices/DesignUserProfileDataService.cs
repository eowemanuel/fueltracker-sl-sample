using System;
using FuelTracker.Web;
using FuelTracker.DesignModel;
using FuelTracker.Services;

namespace FuelTracker.DesignServices
{
	//TO DO: Add the DesignDataService property to DesignServiceProvider
	//UserProfileDataService = new DesignUserProfileDataService;	

	public class DesignUserProfileDataService : IUserProfileDataService
	{
		public event EventHandler<HasChangesEventArgs> NotifyHasChanges;

		public void Save(Action<System.ServiceModel.DomainServices.Client.SubmitOperation> submitCallback, object state)
		{
			submitCallback(null);
		}

		public void GetUserProfilesList(Action<System.Collections.ObjectModel.ObservableCollection<UserProfile>> getUserProfilesCallback, int pageSize)
		{
			getUserProfilesCallback(new DesignUserProfiles());
		}

		public void GetUserProfileById(int userProfileId, Action<System.Collections.ObjectModel.ObservableCollection<UserProfile>> getUserProfileCallback)
		{
			getUserProfileCallback(new DesignUserProfiles());
		}


		public void Save(UserProfile userProfile, Action<System.ServiceModel.DomainServices.Client.SubmitOperation> submitCallback, object state)
		{
			submitCallback(null);
		}
	}
}

