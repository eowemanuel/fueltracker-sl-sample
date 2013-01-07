using System.Windows;
using System.Windows.Controls;
using FuelTracker.Web;
using FuelTracker.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Messages;
using FuelTracker.Web.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FuelTracker.Services;

namespace FuelTracker.Views
{
	public partial class CarEditView : ChildWindow
	{
		public CarEditView()
		{
			InitializeComponent();
			
			//TO DO: Assign SelectionChanged of ChildRelatedEntities grid to enable focus on selected items
			//ChildRelatedEntitiesDataGrid.SelectionChanged+=new SelectionChangedEventHandler(dataGrid_SelectionChanged);
		}

		#region SelectionChange Scrolling
		/// <summary>
		/// Scrolls to the selected item and the first column
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var dataGrid = (DataGrid)sender;
			ScrollToSelectedItem(dataGrid);
		}

		/// <summary>
		/// Scrolls to the SelectedItem and first column of a given DataGrid
		/// This method must be used as an extension method to the DataGrid class
		/// </summary>
		/// <param name="dataGrid"></param>
		void ScrollToSelectedItem(DataGrid dataGrid)
		{
			if (dataGrid.SelectedItem == null)
			{
				return;
			}
			var itemToScrollTo = dataGrid.SelectedItem;
			var columnToScrolTo = dataGrid.Columns[0];
			dataGrid.ScrollIntoView(itemToScrollTo, columnToScrolTo);
		}
		#endregion

		public CarEditView(int id):this()
		{
			
			var carDataService = ServiceProvider.Instance.CarDataService;

            //TO DO: Consider parent relations source (ex. are parent relations load manualy or from cached source for entire application). 
            IEnumerable<UserProfile> userProfiles = new List<UserProfile>();
	
			CarEditViewModel carEditViewModel = new CarEditViewModel(id, carDataService, userProfiles);
			this.DataContext = carEditViewModel;
		}

		public CarEditView(Car car, FuelTrackerContext context)
			: this()
		{
			//CarEditViewModel carEditViewModel = new CarEditViewModel( car.Id);
			//CarEditViewModel carEditViewModel = new CarEditViewModel(context, car);
			//this.DataContext = carEditViewModel;
		}

		private void RegisterMessages()
		{
			Messenger.Default.Register<SavedCarDialogMessage>(this, OnSaveCarDialogMessageReceived);
		}

		private void OnSaveCarDialogMessageReceived(SavedCarDialogMessage msg)
		{
			MessageBox.Show(msg.Content, msg.Caption, msg.Button);
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			//this.DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}
	}
}



