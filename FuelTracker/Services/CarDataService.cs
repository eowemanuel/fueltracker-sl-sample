// CarDataService.cs
using System;
using System.Collections.ObjectModel;
using System.ServiceModel.DomainServices.Client;
using Microsoft.Windows.Data.DomainServices;
using FuelTracker.Web;
using FuelTracker.Web.Services;

namespace FuelTracker.Services
{
	//ServiceProviderBase
	//public virtual ICarDataService CarDataService { get; protected set; }
	//
	//ServiceProvider
	//    public override ICarDataService CarDataService
	//    {
	//        get { return new CarDataService(); }
	//    }

	//TO DO: Uncommnent or delete or extract in another file
	//public class HasChangesEventArgs : EventArgs
	//{
	//    public bool HasChanges { get; set; }
	//}
	
	public class CarDataService : ICarDataService
	{
		public event EventHandler<HasChangesEventArgs> NotifyHasChanges;
		public FuelTrackerContext Context { get; set; }
		private Action<ObservableCollection< Car >> _getCarsCallback;

		private LoadOperation<Car> _carsLoadOperation;
		private int _pageIndex;

		/// <summary>
		/// Initialize the CarDataService
		/// </summary>
		public CarDataService()
		{
			Context = new FuelTrackerContext();
			Context.PropertyChanged += ContextPropertyChanged;
		}

		/// <summary>
		/// Saves changes to the Context if available.
		/// </summary>
		/// <param name="submitCallback">CallBack to be called after load complition</param>
		/// <param name="state"></param>
		public void Save(Action<SubmitOperation> submitCallback, object state)
		{
			if (Context.HasChanges)
			{
				Context.SubmitChanges(submitCallback, state);
			}
		}

		/// <summary>
		/// Load CarList
		/// </summary>
		/// <param name="getCarsCallback">CallBack to be called after load complition</param>
		/// <param name="pageSize"></param>
		public void GetCarsList(Action<ObservableCollection<Car>> getCarsCallback, int pageSize)
		{
			ClearCars();
			var query = Context.GetCarsQuery().Take(pageSize);
			RunCarsQuery(query, getCarsCallback);
		}

		/// <summary>
		/// Notifies for property changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ContextPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (NotifyHasChanges != null)
			{
				NotifyHasChanges(this, new HasChangesEventArgs() { HasChanges = Context.HasChanges });
			}
		}

		/// <summary>
		/// Clear Car List
		/// </summary>
		private void ClearCars()
		{
			_pageIndex = 0;
			Context.Cars.Clear();
		}

		/// <summary>
		/// Run Cars Query
		/// </summary>
		/// <param name="query">Query</param>
		/// <param name="getCarsCallback">CallBack</param>
		private void RunCarsQuery(EntityQuery<Car> query, Action<ObservableCollection<Car>> getCarsCallback)
		{
			_getCarsCallback = getCarsCallback;
			_carsLoadOperation = Context.Load<Car>(query);
			_carsLoadOperation.Completed += OnLoadCarsCompleted;
		}

		/// <summary>
		/// Cars loading Completed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLoadCarsCompleted(object sender, EventArgs e)
		{
			_carsLoadOperation.Completed -= OnLoadCarsCompleted;
			var cars = new EntityList<Car>(Context.Cars, _carsLoadOperation.Entities);
			_getCarsCallback(cars);
		}

		/// <summary>
		/// Get Car by Id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="getCarsCallback"></param>
		public void GetCarById(int id, Action<ObservableCollection<Car>> getCarCallback)
		{
			ClearCars();
			var query = Context.GetCarsQuery().Where(mm => mm.Id == id);
			RunCarsQuery(query, getCarCallback);
		}


		public void Save(Car car, Action<SubmitOperation> submitCallback, object state)
		{
			Context.Cars.Add(car);
			if (Context.HasChanges)
			{
				Context.SubmitChanges(submitCallback, state);
			}
		}
	}

	public interface ICarDataService
	{
		event EventHandler<HasChangesEventArgs> NotifyHasChanges;	

		void Save(Action<SubmitOperation> submitCallback, object state);

		void GetCarById(int id, Action<ObservableCollection<Car>> getCarCallback);

		void GetCarsList(
			Action<ObservableCollection<Car>> getCarsCallback,
			int pageSize);

		void Save(Car Car, Action<SubmitOperation> submitCallback, object state);
	}
}

