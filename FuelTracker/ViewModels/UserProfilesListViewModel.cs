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
	public class UserProfilesListViewModel : ViewModelBase
	{
		private readonly FuelTrackerDomainContext _context = new FuelTrackerDomainContext();

		private readonly DomainCollectionView<UserProfile> _view;
		private readonly DomainCollectionViewLoader<UserProfile> _loader;
		private readonly EntityList<UserProfile> _source;

		/// <summary>
		/// Initializes a new instance of the UserProfilesListViewModel class.
		/// </summary>
		public UserProfilesListViewModel()
		{
			this._source = new EntityList<UserProfile>(this._context.UserProfiles);
			this._loader = new DomainCollectionViewLoader<UserProfile>(
									this.LoadUserProfiles,
									this.OnLoadUserProfilesCompleted);
			this._view = new DomainCollectionView<UserProfile>(this._loader, this._source);

			#region DesignerProperties.IsInDesignTool
			// Swap out the loader for design-time scenarios
			if (IsInDesignMode)
			{
				DesignUserProfilesLoader loader = new DesignUserProfilesLoader();
				this._view = new DomainCollectionView<UserProfile>(loader, loader.Source);
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
		
		private void LoadView()
		{
			using (this.CollectionView.DeferRefresh())
			{
				this.CollectionView.PageSize = 10;
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

		public DomainCollectionView<UserProfile> CollectionView
		{
			get { return this._view; }
		}

		#region SelectedUserProfile
		public const string SelectedUserProfilePropertyName = "SelectedUserProfile";
		private UserProfile _selectedUserProfile;
		public UserProfile SelectedUserProfile
		{
			get
			{
				return _selectedUserProfile;
			}

			set
			{
				if (_selectedUserProfile == value)
				{
					return;
				}

				var oldValue = _selectedUserProfile;
				_selectedUserProfile = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(SelectedUserProfilePropertyName);
				//notify the Control that CanEditUserProfile is enabled
				if (SelectedUserProfile == null)
				{
					CanEditUserProfile = false;
				}
				else
				{
					CanEditUserProfile = true;
				}
				

			}
		}
		#endregion


		#endregion

		private LoadOperation<UserProfile> LoadUserProfiles()
		{
			this.IsGridEnabled = false;

			var query = this._context
								.GetUserProfilesQuery();

			//TO DO: Update filtering
			//if (MyPropertyFilter > 0)
			//{
			//    query = query.Where(mm => mm.MyProperty == MyPropertyFilter);
			//}

			return this._context.Load(query.SortAndPageBy(this._view));
		}

		private void OnLoadUserProfilesCompleted(LoadOperation<UserProfile> op)
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

		#region EditUserProfileCommand
		private RelayCommand _editUserProfileCommand;
		/// <summary>
		/// The <see cref="EditUserProfileCommand" />.
		/// </summary>
		public RelayCommand EditUserProfileCommand
		{
			get
			{
				if (_editUserProfileCommand == null)
				{
					_editUserProfileCommand = new RelayCommand(
							() =>
							{
								EditUserProfileExecute();
							},
							() => CanEditUserProfile
						);
				}
				return _editUserProfileCommand;
			}
			set
			{
				_editUserProfileCommand = value;
			}
		}

		/// <summary>
		/// Executes when EditUserProfileCommand is called
		/// </summary>
		public void EditUserProfileExecute()
		{
			throw new NotImplementedException();
			//TO DO: Implement the EditUserProfileExecute method
			// Messenger.Default.Send(
			//     new LaunchEditUserProfileMessage()
			//     {
			//         UserProfile = SelectedUserProfile,
			//         Context = this._context
			//     }
			// );
		}

		public const string CanEditUserProfilePropertyName = "CanEditUserProfile";
		private bool _canEditUserProfile = false;

		/// <summary>
		/// Gets the CanEditUserProfile property.
		/// If false command is not enabled
		/// If true command is enabled
		/// </summary>
		public bool CanEditUserProfile
		{
			get
			{
				return _canEditUserProfile;
			}

			set
			{
				if (_canEditUserProfile == value)
				{
					return;
				}

				var oldValue = _canEditUserProfile;
				_canEditUserProfile = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(CanEditUserProfilePropertyName);

				//tells the controls that are binded to the Command if it can execute
				EditUserProfileCommand.RaiseCanExecuteChanged();
			}
		}
		#endregion

		#region AddNewUserProfileCommand

		private RelayCommand _addNewUserProfileCommand;
		/// <summary>
		/// The <see cref="AddNewUserProfileCommand" />.
		/// </summary>
		public RelayCommand AddNewUserProfileCommand
		{
			get
			{
				if (_addNewUserProfileCommand == null)
				{
					_addNewUserProfileCommand = new RelayCommand(
							() =>
							{
								AddNewUserProfileExecute();
							},
							() => CanAddNewUserProfile
						);
				}
				return _addNewUserProfileCommand;
			}
			set
			{
				_addNewUserProfileCommand = value;
			}
		}

		/// <summary>
		/// Executes when AddNewUserProfileCommand is called
		/// </summary>
		public void AddNewUserProfileExecute()
		{
			throw new NotImplementedException();
			//TO DO: Update AddNew command
			//LaunchNewUserProfileMessage message = new LaunchNewUserProfileMessage();
			//Messenger.Default.Send(message);
		}

		public const string CanAddNewUserProfilePropertyName = "CanAddNewUserProfile";
		private bool _canAddNewUserProfile = true;

		/// <summary>
		/// Gets the CanAddNewUserProfile property. Check if Can Add new UserProfile 
		/// If false command is not enabled
		/// If true command is enabled
		/// </summary>
		public bool CanAddNewUserProfile
		{
			get
			{
				return _canAddNewUserProfile;
			}

			set
			{
				if (_canAddNewUserProfile == value)
				{
					return;
				}

				var oldValue = _canAddNewUserProfile;
				_canAddNewUserProfile = value;

				// Update bindings, no broadcast
				RaisePropertyChanged(CanAddNewUserProfilePropertyName);

				//tells the controls that are binded to the Command if it can execute
				AddNewUserProfileCommand.RaiseCanExecuteChanged();
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
