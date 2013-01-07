using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Web;

namespace FuelTracker.Messages
{
	public class LaunchEditCarMessage : MessageBase
	{
		public LaunchEditCarMessage()
			: base()
		{ }

		public LaunchEditCarMessage(FuelTracker.Web.Car car)
		{
			this.Car = car;
		}

		public FuelTracker.Web.Car Car { get; set; }
	}

	//TO DO: Use Message
	//Add usage of LaunchEditCarMessage
	//Register:
	//Messenger.Default.Register<FuelTracker.Messages.LaunchEditCarMessage>(this, OnLaunchEditCarMessage);
	//...
	//
	//private void OnLaunchEditCarMessage(FuelTracker.Messages.LaunchEditCarMessage msg)
	//{
	//	  //Most frequent use : Show Edit window for Car
	//    var car = msg.Car;//set car = 0 for launching new
    //    int id = car.Id;
    //    CarEditView editViewWindow = new CarEditView(id);
    //    editViewWindow.Show();
	//}
}
