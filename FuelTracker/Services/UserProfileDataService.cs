// UserProfileDataService.cs
using System;
using System.Collections.ObjectModel;
using System.ServiceModel.DomainServices.Client;
using Microsoft.Windows.Data.DomainServices;
using FuelTracker.Web;
using FuelTracker.Web.Services;

namespace FuelTracker.Services
{
	//ServiceProviderBase
	//public virtual IUserProfileDataService UserProfileDataService { get; protected set; }
	//
	//ServiceProvider
	//    public override IUserProfileDataService UserProfileDataService
	//    {
	//        get { return new UserProfileDataService(); }
	//    }

	
	public class UserProfileDataService : IUserProfileDataService
	{
		public event EventHandler<HasChangesEventArgs> NotifyHasChanges;
		public FuelTrackerDomainContext Context { get; set; }
		private Action<ObservableCollection< UserProfile >> _getUserProfilesCallback;

		private LoadOperation<UserProfile> _userProfilesLoadOperation;
		private int _pageIndex;

		/// <summary>
		/// Initialize the UserProfileDataService
		/// </summary>
		public UserProfileDataService()
		{
			Context = new FuelTrackerDomainContext();
			Context.PropertyChanged += ContextPropertyChanged;
		}

		/// <summary>
		/// Saves changes to the Context if available.
		/// </summary>
		/// <param name="submitCallback">CallBack to be called after load complition</param>
		/// <param name="state"></param>
		public void Save(Action<SubmitOperation> submitCallback, object state)
		{
			if (Context.HasChanges)
			{
				Context.SubmitChanges(submitCallback, state);
			}
		}

		/// <summary>
		/// Load UserProfileList
		/// </summary>
		/// <param name="getUserProfilesCallback">CallBack to be called after load complition</param>
		/// <param name="pageSize"></param>
		public void GetUserProfilesList(Action<ObservableCollection<UserProfile>> getUserProfilesCallback, int pageSize)
		{
			ClearUserProfiles();
			var query = Context.GetUserProfilesQuery().Take(pageSize);
			RunUserProfilesQuery(query, getUserProfilesCallback);
		}

		/// <summary>
		/// Notifies for property changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ContextPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (NotifyHasChanges != null)
			{
				NotifyHasChanges(this, new HasChangesEventArgs() { HasChanges = Context.HasChanges });
			}
		}

		/// <summary>
		/// Clear UserProfile List
		/// </summary>
		private void ClearUserProfiles()
		{
			_pageIndex = 0;
			Context.UserProfiles.Clear();
		}

		/// <summary>
		/// Run UserProfiles Query
		/// </summary>
		/// <param name="query">Query</param>
		/// <param name="getUserProfilesCallback">CallBack</param>
		private void RunUserProfilesQuery(EntityQuery<UserProfile> query, Action<ObservableCollection<UserProfile>> getUserProfilesCallback)
		{
			_getUserProfilesCallback = getUserProfilesCallback;
			_userProfilesLoadOperation = Context.Load<UserProfile>(query);
			_userProfilesLoadOperation.Completed += OnLoadUserProfilesCompleted;
		}

		/// <summary>
		/// UserProfiles loading Completed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLoadUserProfilesCompleted(object sender, EventArgs e)
		{
			_userProfilesLoadOperation.Completed -= OnLoadUserProfilesCompleted;
			var userProfiles = new EntityList<UserProfile>(Context.UserProfiles, _userProfilesLoadOperation.Entities);
			_getUserProfilesCallback(userProfiles);
		}

		/// <summary>
		/// Get UserProfile by Id
		/// </summary>
		/// <param name="userProfileId"></param>
		/// <param name="getUserProfilesCallback"></param>
		public void GetUserProfileById(int userProfileId, Action<ObservableCollection<UserProfile>> getUserProfileCallback)
		{
			ClearUserProfiles();
			var query = Context.GetUserProfilesQuery().Where(mm => mm.Id == userProfileId);
			RunUserProfilesQuery(query, getUserProfileCallback);
		}


		public void Save(UserProfile userProfile, Action<SubmitOperation> submitCallback, object state)
		{
			Context.UserProfiles.Add(userProfile);
			if (Context.HasChanges)
			{
				Context.SubmitChanges(submitCallback, state);
			}
		}
	}

	public interface IUserProfileDataService
	{
		event EventHandler<HasChangesEventArgs> NotifyHasChanges;	

		void Save(Action<SubmitOperation> submitCallback, object state);

		void GetUserProfileById(int userProfileId, Action<ObservableCollection<UserProfile>> getUserProfileCallback);

		void GetUserProfilesList(
			Action<ObservableCollection<UserProfile>> getUserProfilesCallback,
			int pageSize);

		void Save(UserProfile UserProfile, Action<SubmitOperation> submitCallback, object state);
	}
}

