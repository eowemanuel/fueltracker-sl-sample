using System;
using System.Collections.ObjectModel;
using FuelTracker.Web;
using System.Collections.Generic;

namespace FuelTracker.DesignModel
{
	public class DesignUserProfiles : ObservableCollection<UserProfile>
	{
		private const int entitiesCount = 10;
		public DesignUserProfiles():this(entitiesCount)
		{
		}
		
		public DesignUserProfiles(int entitiesCount)
		{
			var userProfilesList = GenerateDesignUserProfilesList(entitiesCount);
			foreach (var userProfile in userProfilesList)
			{
				this.Add(userProfile);
			}
		}

		public IList<UserProfile> GenerateDesignUserProfilesList(int entitiesCount)
		{
			IList<UserProfile> generatedSource = new List<UserProfile>();

			for (int i = 2; i < entitiesCount; i++)
			{
				var userProfile =
		new UserProfile
		{
			City = string.Format("City {0:5}", i),
			Id = i,
			UserName = string.Format("UserName {0:5}", i),
		};
				generatedSource.Add(userProfile);
			}

			return generatedSource;
		}

	}
	
}

