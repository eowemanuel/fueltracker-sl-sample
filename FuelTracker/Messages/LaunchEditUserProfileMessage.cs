using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Web;

namespace FuelTracker.Messages
{
	public class LaunchEditUserProfileMessage : MessageBase
	{
		public LaunchEditUserProfileMessage()
			: base()
		{ }

		public LaunchEditUserProfileMessage(FuelTracker.Web.UserProfile userProfile)
		{
			this.UserProfile = userProfile;
		}

		public FuelTracker.Web.UserProfile UserProfile { get; set; }
	}
}
