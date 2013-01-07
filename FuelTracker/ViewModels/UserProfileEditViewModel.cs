using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FuelTracker.Web;
using FuelTracker.Services;
using FuelTracker.Messages;

namespace FuelTracker.ViewModels
{
	public class UserProfileEditViewModel : ViewModelBase
	{
		private IUserProfileDataService userProfileDataService;
		public IUserProfileDataService UserProfileDataService
		{
			get
			{
				if (userProfileDataService == null)
				{
					UserProfileDataService = ServiceProvider.Instance.UserProfileDataService;
					UserProfileDataService.NotifyHasChanges += OnUserProfileDataServiceNotifyHasChanges;
				}
				return userProfileDataService;
			}
			set
			{
				userProfileDataService = value;
			}
		}

		private void OnUserProfileDataServiceNotifyHasChanges(object sender, HasChangesEventArgs e)
		{
			CanSaveUserProfile = e.HasChanges || IsNew;
			SaveUserProfileCommand.RaiseCanExecuteChanged();
		}

		/// <summary>
		/// Initializes a new instance of the UserProfileEditViewModel class.
		/// </summary>
		public UserProfileEditViewModel(int userProfileId)
		{
			if (IsInDesignMode)
			{
				// Code runs in Blend --> create design time data.
				Init();
				LoadNomenclaturesData();
				UserProfileId = userProfileId;//if null or 0 => create new UserProfile
				if (UserProfileId == 0)
				{
					IsNew = true;
				}
				LoadData();
			}
			else
			{
				Init();
				LoadNomenclaturesData();
				UserProfileId = userProfileId;//if null or 0 => create new UserProfile
				if (UserProfileId == 0)
				{
					IsNew = true;
				}
				LoadData();
			}
		}


		#region Methods
		/// <summary>
		/// Initialize ViewModel objects
		/// </summary>
		private void Init()
		{
			//TO DO: Init resources 
			//TO DO: Init services
			//mealTypeDataService = ServiceProvider.Instance.MealTypeDataService;

		}

		/// <summary>
		/// Loads data from the srever
		/// </summary>
		private void LoadData()
		{
			if (IsNew)
			{
				NewUserProfile();
			}
			else
			{
				LoadUserProfile(UserProfileId);
			}
		}
		#endregion

		#region Nomenclatures
		private void LoadNomenclaturesData()
		{
			//TO DO: Load nomenclatures data
			//Load<= relatedEntityCollectionName >();
		}

		#endregion

		#region UserProfile logic

		#region UserProfile Entity Property

		//the Id of UserProfile
		protected int UserProfileId { get; set; }

		private void LoadUserProfile(int userProfileId)
		{
			UserProfile = null;
			UserProfileDataService.GetUserProfileById(userProfileId, GetUserProfileByIdCallback);
		}

		private void NewUserProfile()
		{
			UserProfile = new UserProfile();
			CanSaveUserProfile = IsNew;
			SaveUserProfileCommand.RaiseCanExecuteChanged();
		}

		/// <summary>
		/// Fires when UserProfile is loaded
		/// </summary>
		/// <param name="userProfile"></param>
		private void GetUserProfileByIdCallback(ObservableCollection<UserProfile> userProfiles)
		{
			if (userProfiles == null)
			{
				return;
			}

			if (userProfiles.Count <= 0)
			{
				return;
			}

			var userProfile = userProfiles.FirstOrDefault();
			this.UserProfile = userProfile;
		}

		/// <summary>
		/// The <see cref="UserProfile" /> property's name.
		/// </summary>
		public const string UserProfilePropertyName = "UserProfile";

		private UserProfile _userProfile;

		/// <summary>
		/// The UserProfile that is being edited
		/// </summary>
		public UserProfile UserProfile
		{
			get
			{
				return _userProfile;
			}

			set
			{
				if (_userProfile == value)
				{
					return;
				}

				var oldValue = _userProfile;
				_userProfile = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(UserProfilePropertyName);

				//if (UserProfile == null)
				//{
				//	CanAddNewUserProfileChildEntity = false;
				//	//HasSelectedUserProfileChildEntity = false;
				//}
				//else
				//{
				//	CanAddNewUserProfileChildEntity = true;
				//	//HasSelectedUserProfileChildEntity = true;
				//}
			}
		}

		#endregion

		#region IsNew
		private bool _isNew;
		public bool IsNew
		{
			get
			{
				return _isNew;
			}

			set
			{
				if (_isNew == value)
				{
					return;
				}
				_isNew = value;
				RaisePropertyChanged("IsNew");
			}
		}
		#endregion

		#region SaveUserProfileCommand

		private RelayCommand _saveUserProfileCommand;

		/// <summary>
		/// The <see cref="SaveUserProfileCommand" />.
		/// </summary>
		public RelayCommand SaveUserProfileCommand
		{
			get
			{
				if (_saveUserProfileCommand == null)
				{
					_saveUserProfileCommand = new RelayCommand(
							() =>
							{
								SaveUserProfileExecute();
							},
							() => CanSaveUserProfile
						);
				}
				return _saveUserProfileCommand;
			}
			set
			{
				_saveUserProfileCommand = value;
			}
		}

		/// <summary>
		/// Executes when SaveUserProfileCommand is called
		/// </summary>
		public void SaveUserProfileExecute()
		{
			if (UserProfile.EntityState == EntityState.Detached)
			{
				UserProfileDataService.Save(UserProfile, OnUserProfileSaved, null);
			}
			else
			{
				UserProfileDataService.Save(OnUserProfileSaved, null);
			}
		}

		public void OnUserProfileSaved(SubmitOperation op)
		{

			if (op.HasError)
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

				var dialogErrorMessage = new ErrorDialogMessage(
					errorMessage, "Съхраняване на XXXX");//TO DO: Update 
				Messenger.Default.Send(dialogErrorMessage);

				op.MarkErrorAsHandled();
				return;
			}

			this.SendSaveSuccessMessage();
			this.SendUpdatedUserProfileMessage();
		}

		private void SendUpdatedUserProfileMessage()
		{
			//Send message to reload the UserProfiles list
			var updatedUserProfileMessage = new UpdatedUserProfileMessage()
			{
				UserProfile = this.UserProfile
			};
			Messenger.Default.Send<UpdatedUserProfileMessage>(updatedUserProfileMessage);
		}

		private void SendSaveSuccessMessage()
		{
			//Show success dialog message
			string successMessage = "Записът е успешен!";
			var dialogMessage = new SuccessDialogMessage(successMessage, "Съхраняване на XXXX");
			Messenger.Default.Send(dialogMessage);
		}

		public const string CanSaveUserProfilePropertyName = "CanSaveUserProfile";
		private bool _canSaveUserProfile = false;
		public bool CanSaveUserProfile
		{
			get
			{
				return _canSaveUserProfile;
			}

			set
			{
				if (_canSaveUserProfile == value)
				{
					return;
				}

				_canSaveUserProfile = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(CanSaveUserProfilePropertyName);

				//tells the controls that are binded to the Command if it can execute
				SaveUserProfileCommand.RaiseCanExecuteChanged();
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

		#region Car

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
			var newCar = new Car();
			UserProfile.Cars.Add(newCar);
			SelectedCar = newCar;
		}

		public const string CanAddNewCarPropertyName = "CanAddNewCar";
		private bool _canAddNewCar = false;
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

				_canAddNewCar = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(CanAddNewCarPropertyName);

				//tells the controls that are binded to the Command if it can execute
				AddNewCarCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#region DeleteCarCommand

		private RelayCommand _deleteCarCommand;
		public RelayCommand DeleteCarCommand
		{
			get
			{
				if (_deleteCarCommand == null)
				{
					_deleteCarCommand = new RelayCommand(
							() =>
							{
								DeleteCarExecute();
							},
							() => CanDeleteCar
						);
				}
				return _deleteCarCommand;
			}
			set
			{
				_deleteCarCommand = value;
			}
		}

		/// <summary>
		/// Executes when DeleteCarCommand is called
		/// </summary>
		public void DeleteCarExecute()
		{
			if (SelectedCar == null)
			{
				//throw new ArgumentNullException("{0} is Null", SelectedCarPropertyName);
				return;
			}
			UserProfile.Cars.Remove(SelectedCar);
		}


		/// <summary>
		/// The <see cref="CanDeleteCar" /> property's name.
		/// </summary>
		public const string CanDeleteCarPropertyName = "CanDeleteCar";
		private bool _canDeleteCar = false;
		public bool CanDeleteCar
		{
			get
			{
				return _canDeleteCar;
			}

			set
			{
				if (_canDeleteCar == value)
				{
					return;
				}

				_canDeleteCar = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(CanDeleteCarPropertyName);

				//tells the controls that are binded to the Command if it can execute
				DeleteCarCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

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

				_selectedCar = value;
				// Update bindings, no broadcast
				RaisePropertyChanged(SelectedCarPropertyName);

				if (_selectedCar == null)
				{
					HasSelectedCar = false;
					CanDeleteCar = false;
				}
				else
				{
					HasSelectedCar = true;
					CanDeleteCar = true;
				}

			}
		}
		#endregion

		#region HasSelectedCar
		/// <summary>
		/// The <see cref="HasSelectedCar" /> property's name.
		/// </summary>
		public const string HasSelectedCardPropertyName = "HasSelectedCar";
		private bool _hassSelectedCar = false;
		public bool HasSelectedCar
		{
			get
			{
				return _hassSelectedCar;
			}

			set
			{
				if (_hassSelectedCar == value)
				{
					return;
				}

				_hassSelectedCar = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(HasSelectedCardPropertyName);
			}
		}
		#endregion

		#endregion
		#endregion

		public override void Cleanup()
		{
			//TO DO: Consider resources to clean up
			// Clean own resources if needed
			base.Cleanup();
		}

	}
}

