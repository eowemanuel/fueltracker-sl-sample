using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Web;

namespace FuelTracker.Messages
{
	public class SavedCarDialogMessage : DialogMessage
	{
		public SavedCarDialogMessage(string content, string title)
			: base(content, null)
		{
			Button = MessageBoxButton.OK;
			Caption = title;
		}

		public FuelTracker.Web.Car Car { get; set; }
	}

	//TO DO: Use Message
	//Add usage of SavedCarDialogMessage
	//Register:
	//Messenger.Default.Register<FuelTracker.Messages.SavedCarDialogMessage>(this, OnSavedCarDialogMessage);
	//...
	//
	//	private void OnSavedCarDialogMessage(FuelTracker.Messages.SavedCarDialogMessage msg)
	//	{
	//		MessageBox.Show(msg.Content, msg.Caption, msg.Button);
	//	}
}
