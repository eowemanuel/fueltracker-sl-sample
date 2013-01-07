using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Web;

namespace FuelTracker.Messages
{
	public class LaunchNewCarMessage : MessageBase
	{
		public LaunchNewCarMessage()
			: base()
		{ }

		public LaunchNewCarMessage(FuelTracker.Web.Car car)
		{
			this.Car = car;
		}

		public FuelTracker.Web.Car Car { get; set; }
	}

	//TO DO: Use Message
	//Add usage of LaunchNewCarMessage
	//Register:
	//Messenger.Default.Register<FuelTracker.Messages.LaunchNewCarMessage>(this, OnLaunchNewCarMessage);
	//...
	//
	//private void OnLaunchNewCarMessage(FuelTracker.Messages.LaunchNewCarMessage msg)
	//{
	//	  //Most frequent use : Show Edit window for Car
	//    var car = msg.Car;//set car = 0 for launching new
    //    int id = car.Id;
    //    CarEditView editViewWindow = new CarEditView(id);
    //    editViewWindow.Show();
	//}
}
