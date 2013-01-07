/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:FuelTracker"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;

namespace FuelTracker.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private static MainViewModel _main;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time services and viewmodels
            ////}
            ////else
            ////{
            ////    // Create run time services and view models
            ////}

            _main = new MainViewModel();
        }


        //private static CarsListViewModel _carsListViewModel;

        ///// <summary>
        ///// Gets the CarsListViewModel property.
        ///// </summary>
        //public static CarsListViewModel CarsListViewModelStatic
        //{
        //    get
        //    {
        //        if (_carsListViewModel == null)
        //        {
        //            CreateCarsListViewModel();
        //        }

        //        return _carsListViewModel;
        //    }
        //}

        ///// <summary>
        ///// Gets the CarsListViewModel property.
        ///// </summary>
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        //    "CA1822:MarkMembersAsStatic",
        //    Justification = "This non-static member is needed for data binding purposes.")]
        //public CarsListViewModel CarsListViewModel
        //{
        //    get
        //    {
        //        return CarsListViewModelStatic;
        //    }
        //}

        ///// <summary>
        ///// Provides a deterministic way to delete the CarsListViewModel property.
        ///// </summary>
        //public static void ClearCarsListViewModel()
        //{
        //    _carsListViewModel.Cleanup();
        //    _carsListViewModel = null;
        //}

        ///// <summary>
        ///// Provides a deterministic way to create the CarsListViewModel property.
        ///// </summary>
        //public static void CreateCarsListViewModel()
        //{
        //    if (_carsListViewModel == null)
        //    {
        //        _carsListViewModel = new CarsListViewModel();
        //    }
        //}

        ///// <summary>
        ///// Cleans up all the resources.
        ///// </summary>
        //public static void Cleanup()
        //{
        //    ClearCarsListViewModel();
        //}


        /// <summary>
        /// Gets the Main property which defines the main viewmodel.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return _main;
            }
        }
    }
}