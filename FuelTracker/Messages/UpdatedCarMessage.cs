using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Web;

namespace FuelTracker.Messages
{
	public class UpdatedCarMessage : MessageBase
	{
		public UpdatedCarMessage()
			: base()
		{ }

		public UpdatedCarMessage(FuelTracker.Web.Car car)
		{
			this.Car = car;
		}

		public FuelTracker.Web.Car Car { get; set; }
	}

	//TO DO: Use Message
	//Add usage of UpdatedCarMessage
	//Register:
	//Messenger.Default.Register<FuelTracker.Messages.UpdatedCarMessage>(this, OnUpdatedCarMessage);
	//...
	//
	//private void OnUpdatedCarMessage(FuelTracker.Messages.UpdatedCarMessage msg)
	//{
	//	  //Most frequent use : Show Edit window for Car
	//    var car = msg.Car;//set car = 0 for launching new
    //    int id = car.Id;
    //    CarEditView editViewWindow = new CarEditView(id);
    //    editViewWindow.Show();
	//}
}
