using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Web;

namespace FuelTracker.Messages
{
	public class SavedUserProfileMessage : MessageBase
	{
		public SavedUserProfileMessage()
			: base()
		{ }

		public SavedUserProfileMessage(FuelTracker.Web.UserProfile userProfile)
		{
			this.UserProfile = userProfile;
		}

		public FuelTracker.Web.UserProfile UserProfile { get; set; }
	}
}
