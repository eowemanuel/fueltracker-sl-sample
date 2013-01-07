using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Web;
using FuelTracker.Messages;
using FuelTracker.Services;

namespace FuelTracker.ViewModels
{
	public class CarEditViewModel : ViewModelBase
	{
		private ICarDataService carDataService;
		public ICarDataService CarDataService
		{
			get
			{
				return carDataService;
			}
		}

		private void OnCarDataServiceNotifyHasChanges(object sender, HasChangesEventArgs e)
		{
			CanSaveCar = e.HasChanges;
		}

		public CarEditViewModel(int id, 
									ICarDataService carDataService
									, IEnumerable<UserProfile> userProfiles
									
									):this(carDataService
										, userProfiles
)
		{
			if(id > 0)
			{
				LoadCar(id);
			}
		}

		public CarEditViewModel(ICarDataService carDataService
									, IEnumerable<UserProfile> userProfiles
											
		)
		{
			if (carDataService == null)
			{
				throw new ArgumentNullException("carDataService must not be null");
			}
			this.carDataService = carDataService;
			this.carDataService.NotifyHasChanges += OnCarDataServiceNotifyHasChanges;

			//Parent entities assign
			if(userProfiles == null)
			{
				throw new ArgumentNullException("userProfiles must not be null");
			}
			this.UserProfiles = new ObservableCollection<UserProfile>(userProfiles);

									
			LoadNomenclaturesData();
			NewCar();
		}

		public CarEditViewModel(Car car, 
									ICarDataService carDataService
									, IEnumerable<UserProfile> userProfiles
									
									):this(carDataService
										, userProfiles
)

		{
			if (car == null)
            {
                throw new ArgumentNullException("car must not be null");
            }
			this.Car = car;
		}

		#region Nomenclatures
		private void LoadNomenclaturesData()
		{
			//TO DO: Load nomenclatures data
			//Load<= relatedEntityCollectionName >();
		}

		#region UserProfiles Nomenclature

		public const string UserProfilesPropertyName = "UserProfiles";
		private ObservableCollection<UserProfile> _userProfiles;
		public ObservableCollection<UserProfile> UserProfiles
		{
			get
			{
				if (_userProfiles == null)
				{
					_userProfiles = new ObservableCollection<UserProfile>();
				}
				return _userProfiles;
			}

			private set
			{
				if (_userProfiles == value)
				{
					return;
				}

				// Update bindings, no broadcast
				RaisePropertyChanged(UserProfilesPropertyName);
			}
		}

		#region Manualy load UserProfiles
		//IUserProfileDataService userProfileDataService = ServiceProvider.Instance.UserProfileDataService;

		///// <summary>
		///// Load UserProfiles nomenlcature
		///// </summary>
		//private void LoadUserProfiles()
		//{
		//    UserProfiles = null;
		//    userProfileDataService.GetUserProfilesList(GetUserProfilesCallback, int.MaxValue);
		//}

		///// <summary>
		///// Fires when UserProfiles are  loaded
		///// </summary>
		//private void GetUserProfilesCallback(ObservableCollection<UserProfile> userProfiles)
		//{
		//    if (userProfiles == null)
		//    {
		//        return;
		//    }

		//    if (userProfiles.Count <= 0)
		//    {
		//        return;
		//    }

		//    this.UserProfiles = userProfiles;

		//}
		#endregion

		#endregion
		#endregion

		#region Car logic

		#region Car Entity Property

		//the Id of Car
		protected int CarId { get; set; }

		private void LoadCar(int carId)
		{
			Car = null;
			CarDataService.GetCarById(carId, GetCarByIdCallback);
		}

		private void NewCar()
		{
			Car = new Car();
			CanSaveCar = true;
		}

		/// <summary>
		/// Fires when Car is loaded
		/// </summary>
		/// <param name="car"></param>
		private void GetCarByIdCallback(ObservableCollection<Car> cars)
		{
			if (cars == null)
			{
				return;
			}

			if (cars.Count <= 0)
			{
				return;
			}

			var car = cars.FirstOrDefault();
			this.Car = car;
		}

		/// <summary>
		/// The <see cref="Car" /> property's name.
		/// </summary>
		public const string CarPropertyName = "Car";

		private Car _car;

		/// <summary>
		/// The Car that is being edited
		/// </summary>
		public Car Car
		{
			get
			{
				return _car;
			}

			set
			{
				if (_car == value)
				{
					return;
				}

				var oldValue = _car;
				_car = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(CarPropertyName);

				//if (Car == null)
				//{
				//	CanAddNewCarChildEntity = false;
				//	//HasSelectedCarChildEntity = false;
				//}
				//else
				//{
				//	CanAddNewCarChildEntity = true;
				//	//HasSelectedCarChildEntity = true;
				//}
			}
		}

		#endregion

		#region SaveCarCommand

		private RelayCommand _saveCarCommand;

		/// <summary>
		/// The <see cref="SaveCarCommand" />.
		/// </summary>
		public RelayCommand SaveCarCommand
		{
			get
			{
				if (_saveCarCommand == null)
				{
					_saveCarCommand = new RelayCommand(
							() =>
							{
								SaveCarExecute();
							},
							() => CanSaveCar
						);
				}
				return _saveCarCommand;
			}
			set
			{
				_saveCarCommand = value;
			}
		}

		/// <summary>
		/// Executes when SaveCarCommand is called
		/// </summary>
		public void SaveCarExecute()
		{
			if (Car.EntityState == EntityState.Detached)
			{
				CarDataService.Save(Car, OnCarSaved, null);
			}
			else
			{
				CarDataService.Save(OnCarSaved, null);
			}
		}

		public void OnCarSaved(SubmitOperation op)
        {

            if (op.HasError)
            {
                string errorMessage = GenerateSubmitOperationErrorMessage(op);
                this.SaveUnsuccessfull(errorMessage);

                op.MarkErrorAsHandled();
                return;
            }

            string successMessage = "Записът е успешен!";
            this.SaveSuccessfull(successMessage);
        }

        private string GenerateSubmitOperationErrorMessage(SubmitOperation op)
        {
            //Show error message dialog
            string errorMessage = String.Format("{0}:\n{1}\n", "Неуспешен запис!", op.Error.Message);
            foreach (var errorEntity in op.EntitiesInError)
            {
                foreach (var validationResult in errorEntity.ValidationErrors)
                {
                    errorMessage += string.Format("{0}\n", validationResult.ErrorMessage);
                }

            }
            return errorMessage;
        }

        private void SaveUnsuccessfull(string errorMessage)
        {
            this.SendErrorMessage(errorMessage);
        }

        private void SendErrorMessage(string errorMessage)
        {
            var dialogErrorMessage = new ErrorDialogMessage(
                errorMessage, "Съхраняване на Car");//TO DO: Update message
            Messenger.Default.Send(dialogErrorMessage);
        }

        private void SaveSuccessfull(string successMessage)
        {
            this.SendSaveSuccessMessage(successMessage);
            this.SendUpdatedCarMessage();
        }

        private void SendUpdatedCarMessage()
        {
            //Send message to reload the Cars list
            var updatedCarMessage = new UpdatedCarMessage()
            {
                Car = this.Car
            };
            Messenger.Default.Send<UpdatedCarMessage>(updatedCarMessage);
        }

        private void SendSaveSuccessMessage(string message)
        {
            //Show success dialog message
            string successMessage = message;
            var dialogMessage = new SuccessDialogMessage(successMessage, "Съхраняване на Car");
            Messenger.Default.Send(dialogMessage);
        }

		public const string CanSaveCarPropertyName = "CanSaveCar";
		private bool _canSaveCar = false;
		public bool CanSaveCar
		{
			get
			{
				return _canSaveCar;
			}

			set
			{
				if (_canSaveCar == value)
				{
					return;
				}

				_canSaveCar = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(CanSaveCarPropertyName);

				//tells the controls that are binded to the Command if it can execute
				SaveCarCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#region CancelCommand

		private RelayCommand _cancelCommand;
		/// <summary>
		/// The <see cref="CancelCommand" />.
		/// </summary>

		public RelayCommand CancelCommand
		{
			get
			{
				if (_cancelCommand == null)
				{
					_cancelCommand = new RelayCommand(
							() =>
							{
								CancelExecute();
							},
							() => CanCancel
						);
				}
				return _cancelCommand;
			}
			set
			{
				_cancelCommand = value;
			}
		}

		/// <summary>
		/// Executes when CancelCommand is called
		/// </summary>
		public void CancelExecute()
		{
		}

		public const string CanCancelPropertyName = "CanCancel";

		private bool _canCancel = true;
		public bool CanCancel
		{
			get
			{
				return _canCancel;
			}

			set
			{
				if (_canCancel == value)
				{
					return;
				}

				_canCancel = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(CanCancelPropertyName);

				//tells the controls that are binded to the Command if it can execute
				CancelCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#endregion

		#region Related Entities

		#endregion

		public override void Cleanup()
		{
			//TO DO: Consider resources to clean up
			// Clean own resources if needed

			//Unregister notification event
			carDataService.NotifyHasChanges -= OnCarDataServiceNotifyHasChanges;
			base.Cleanup();
		}

	}
}

