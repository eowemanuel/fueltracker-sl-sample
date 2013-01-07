using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Web;

namespace FuelTracker.Messages
{
	public class SavedCarMessage : MessageBase
	{
		public SavedCarMessage()
			: base()
		{ }

		public SavedCarMessage(FuelTracker.Web.Car car)
		{
			this.Car = car;
		}

		public FuelTracker.Web.Car Car { get; set; }
	}

	//TO DO: Use Message
	//Add usage of SavedCarMessage
	//Register:
	//Messenger.Default.Register<FuelTracker.Messages.SavedCarMessage>(this, OnSavedCarMessage);
	//...
	//
	//private void OnSavedCarMessage(FuelTracker.Messages.SavedCarMessage msg)
	//{
	//	  //Most frequent use : Show Edit window for Car
	//    var car = msg.Car;//set car = 0 for launching new
    //    int id = car.Id;
    //    CarEditView editViewWindow = new CarEditView(id);
    //    editViewWindow.Show();
	//}
}
