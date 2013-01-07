using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Web;

namespace FuelTracker.Messages
{
	public class UpdatedUserProfileMessage : MessageBase
	{
		public UpdatedUserProfileMessage()
			: base()
		{ }

		public UpdatedUserProfileMessage(FuelTracker.Web.UserProfile userProfile)
		{
			this.UserProfile = userProfile;
		}

		public FuelTracker.Web.UserProfile UserProfile { get; set; }
	}
}
