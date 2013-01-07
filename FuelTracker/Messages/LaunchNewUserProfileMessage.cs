using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Web;

namespace FuelTracker.Messages
{
	public class LaunchNewUserProfileMessage : MessageBase
	{
		public LaunchNewUserProfileMessage()
			: base()
		{ }

		public LaunchNewUserProfileMessage(FuelTracker.Web.UserProfile userProfile)
		{
			this.UserProfile = userProfile;
		}

		public FuelTracker.Web.UserProfile UserProfile { get; set; }
	}
}
