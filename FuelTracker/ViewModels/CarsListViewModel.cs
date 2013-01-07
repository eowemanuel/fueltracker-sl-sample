using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Data;
using FuelTracker.DesignServices;
using FuelTracker.Messages;
using FuelTracker.Services;
using FuelTracker.Web;
using FuelTracker.Web.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Windows.Data.DomainServices;

 
namespace FuelTracker.ViewModels
{
	public class CarsListViewModel : ViewModelBase
	{
		private readonly FuelTrackerContext _context = new FuelTrackerContext();

		private readonly DomainCollectionView<Car> _view;
		private readonly DomainCollectionViewLoader<Car> _loader;
		private readonly EntityList<Car> _source;

		/// <summary>
		/// Initializes a new instance of the CarsListViewModel class.
		/// </summary>
		public CarsListViewModel()
		{
			this._source = new EntityList<Car>(this._context.Cars);
			this._loader = new DomainCollectionViewLoader<Car>(
									this.LoadCars,
									this.OnLoadCarsCompleted);
			this._view = new DomainCollectionView<Car>(this._loader, this._source);

			#region DesignerProperties.IsInDesignTool
			// Swap out the loader for design-time scenarios
			if (IsInDesignMode)
			{
				DesignCarsLoader loader = new DesignCarsLoader();
				this._view = new DomainCollectionView<Car>(loader, loader.Source);
			}
			#endregion
			
			// Go back to the first page when the sorting changes
			INotifyCollectionChanged notifyingSortDescriptions =
				(INotifyCollectionChanged)this.CollectionView.SortDescriptions;
			notifyingSortDescriptions.CollectionChanged +=
				(sender, e) => this._view.MoveToFirstPage();

			this.Init();
			this.LoadView();
			
			//TO DO:Implement LoadNomenclatures method
			//this.LoadNomenclatures();

		}

		private void Init()
		{
			//TO DO: Init uninitialized resources
		}
		
		private int _pageSize = 10;
		public int PageSize
		{
			get { return _pageSize; }
			set { _pageSize = value; }
		}

		private void LoadView()
		{
			using (this.CollectionView.DeferRefresh())
			{
				this.CollectionView.PageSize = PageSize;
				this.CollectionView.SetTotalItemCount(-1);
				this.CollectionView.MoveToFirstPage();
			}
		}

		private void ApplyGrouping()
		{
			//TO DO: Update ApplyGrouping mehod
			//PropertyGroupDescription groupDescriptionMyProperty = new PropertyGroupDescription("MyProperty");
			//this.CollectionView.GroupDescriptions.Add(groupDescriptionMyProperty);
		}

		private void ApplySorting()
		{
			//TO DO: Update Apply sorting method
			//SortDescription sortDescriptionMyProperty = new SortDescription("MyProperty", ListSortDirection.Descending);
			//this.CollectionView.SortDescriptions.Add(sortDescriptionMyProperty);
		}

		#region View Properties
		public const string IsGridEnabledPropertyName = "IsGridEnabled";
		private bool _isGridEnabled;
		public bool IsGridEnabled
		{
			get
			{
				return _isGridEnabled;
			}

			set
			{
				if (_isGridEnabled == value)
				{
					return;
				}

				var oldValue = _isGridEnabled;
				_isGridEnabled = value;

				RaisePropertyChanged(IsGridEnabledPropertyName);
			}
		}

		public DomainCollectionView<Car> CollectionView
		{
			get { return this._view; }
		}

		#region SelectedCar
		public const string SelectedCarPropertyName = "SelectedCar";
		private Car _selectedCar;
		public Car SelectedCar
		{
			get
			{
				return _selectedCar;
			}

			set
			{
				if (_selectedCar == value)
				{
					return;
				}

				var oldValue = _selectedCar;
				_selectedCar = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(SelectedCarPropertyName);
				//notify the Control that CanEditCar is enabled
				if (SelectedCar == null)
				{
					CanEditCar = false;
				}
				else
				{
					CanEditCar = true;
				}
				

			}
		}
		#endregion


		#endregion

		private LoadOperation<Car> LoadCars()
		{
			this.IsGridEnabled = false;

			var query = this._context
								.GetCarsQuery();

			//TO DO: Update filtering
			//if (MyPropertyFilter > 0)
			//{
			//    query = query.Where(mm => mm.MyProperty == MyPropertyFilter);
			//}

			return this._context.Load(query.SortAndPageBy(this._view));
		}

		private void OnLoadCarsCompleted(LoadOperation<Car> op)
		{
			this.IsGridEnabled = true;

			if (op.HasError)
			{
				// TODO: handle errors
				var error = op.Error;
				throw error;

				op.MarkErrorAsHandled();
			}
			else if (!op.IsCanceled)
			{
				this._source.Source = op.Entities;

				if (op.TotalEntityCount != -1)
				{
					this.CollectionView.SetTotalItemCount(op.TotalEntityCount);
				}
			}

			if (_source.Count >= 0)
			{
				CollectionView.MoveCurrentToFirst();
			}
		}


		#region Commands

		#region EditCarCommand
		private RelayCommand _editCarCommand;
		/// <summary>
		/// The <see cref="EditCarCommand" />.
		/// </summary>
		public RelayCommand EditCarCommand
		{
			get
			{
				if (_editCarCommand == null)
				{
					_editCarCommand = new RelayCommand(
							() =>
							{
								EditCarExecute();
							},
							() => CanEditCar
						);
				}
				return _editCarCommand;
			}
			set
			{
				_editCarCommand = value;
			}
		}

		/// <summary>
		/// Executes when EditCarCommand is called
		/// </summary>
		public void EditCarExecute()
		{
			throw new NotImplementedException();
			//TO DO: Implement the EditCarExecute method
			// Messenger.Default.Send(
			//     new LaunchEditCarMessage()
			//     {
			//         Car = SelectedCar,
			//         Context = this._context
			//     }
			// );
		}

		public const string CanEditCarPropertyName = "CanEditCar";
		private bool _canEditCar = false;

		/// <summary>
		/// Gets the CanEditCar property.
		/// If false command is not enabled
		/// If true command is enabled
		/// </summary>
		public bool CanEditCar
		{
			get
			{
				return _canEditCar;
			}

			set
			{
				if (_canEditCar == value)
				{
					return;
				}

				var oldValue = _canEditCar;
				_canEditCar = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(CanEditCarPropertyName);

				//tells the controls that are binded to the Command if it can execute
				EditCarCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#region AddNewCarCommand

		private RelayCommand _addNewCarCommand;
		/// <summary>
		/// The <see cref="AddNewCarCommand" />.
		/// </summary>
		public RelayCommand AddNewCarCommand
		{
			get
			{
				if (_addNewCarCommand == null)
				{
					_addNewCarCommand = new RelayCommand(
							() =>
							{
								AddNewCarExecute();
							},
							() => CanAddNewCar
						);
				}
				return _addNewCarCommand;
			}
			set
			{
				_addNewCarCommand = value;
			}
		}

		/// <summary>
		/// Executes when AddNewCarCommand is called
		/// </summary>
		public void AddNewCarExecute()
		{
			throw new NotImplementedException();
			//TO DO: Update AddNew command
			//LaunchNewCarMessage message = new LaunchNewCarMessage();
			//Messenger.Default.Send(message);
		}

		public const string CanAddNewCarPropertyName = "CanAddNewCar";
		private bool _canAddNewCar = true;

		/// <summary>
		/// Gets the CanAddNewCar property. Check if Can Add new Car 
		/// If false command is not enabled
		/// If true command is enabled
		/// </summary>
		public bool CanAddNewCar
		{
			get
			{
				return _canAddNewCar;
			}

			set
			{
				if (_canAddNewCar == value)
				{
					return;
				}

				var oldValue = _canAddNewCar;
				_canAddNewCar = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(CanAddNewCarPropertyName);

				//tells the controls that are binded to the Command if it can execute
				AddNewCarCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#region SearchCommand

		private RelayCommand _searchCommand;
		public RelayCommand SearchCommand
		{
			get
			{
				if (_searchCommand == null)
				{
					_searchCommand = new RelayCommand(
							() =>
							{
								SearchExecute();
							},
							() => CanSearch
						);
				}
				return _searchCommand;
			}
			set
			{
				_searchCommand = value;
			}
		}

		public void SearchExecute()
		{
			this.LoadView();
		}

		public const string CanSearchPropertyName = "CanSearch";
		private bool _canSearch = true;
		public bool CanSearch
		{
			get
			{
				return _canSearch;
			}

			set
			{
				if (_canSearch == value)
				{
					return;
				}

				var oldValue = _canSearch;
				_canSearch = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(CanSearchPropertyName);

				//tells the controls that are binded to the Command if it can execute
				SearchCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#endregion
		

		private void LoadNomenclatures()
		{
			throw new NotImplementedException();
			//TO DO: Load all nomenclatures here
			//this.LoadClientGroupsNomenclature();
			//
		}
		
		public override void Cleanup()
		{
			// Clean own resources if needed
			base.Cleanup();
		}

	}
}
