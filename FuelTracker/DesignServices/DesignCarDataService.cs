using System;
using FuelTracker.Web;
using FuelTracker.DesignModel;
using FuelTracker.Services;

namespace FuelTracker.DesignServices
{
	//TO DO: Add the DesignDataService property to DesignServiceProvider
	//CarDataService = new DesignCarDataService();	

	public class DesignCarDataService : ICarDataService
	{
		public event EventHandler<HasChangesEventArgs> NotifyHasChanges;

		public void Save(Action<System.ServiceModel.DomainServices.Client.SubmitOperation> submitCallback, object state)
		{
			submitCallback(null);
		}

		public void GetCarsList(Action<System.Collections.ObjectModel.ObservableCollection<Car>> getCarsCallback, int pageSize)
		{
			getCarsCallback(new DesignCars());
		}

		public void GetCarById(int id, Action<System.Collections.ObjectModel.ObservableCollection<Car>> getCarCallback)
		{
			getCarCallback(new DesignCars());
		}


		public void Save(Car car, Action<System.ServiceModel.DomainServices.Client.SubmitOperation> submitCallback, object state)
		{
			submitCallback(null);
		}
	}
}

