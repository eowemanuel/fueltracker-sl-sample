using System;
using System.Collections;
using System.Collections.Generic;
using FuelTracker.DesignModel;
using FuelTracker.DesignServices;
using FuelTracker.Services;
using FuelTracker.ViewModels;
using FuelTracker.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuelTracker.Tests
{
	//Test metods are named:
	//ObjectOfTest_Condition_ExpectedResult
	[TestClass]
	public class CarEditViewModelTests
	{
		private Car GenerateNewDesignOrganizaiton(int id)
		{
			int i = id;
			var organizaiton =
				new Car
				{
					Engine = string.Format("Engine {0:5}", i),
					EngineVolume = i,
					//FuelTracks = null,//TO DO: Update design model assignment of property FuelTracks on Car
					Id = i,
					Manufacturer = string.Format("Manufacturer {0:5}", i),
					ManufacturingDate = DateTime.Now.AddDays(i),
					Model = string.Format("Model {0:5}", i),
					Name = string.Format("Name {0:5}", i),
					Types = string.Format("Types {0:5}", i),
					//UserProfile = null,//TO DO: Update design model assignment of property UserProfile on Car
					UserProfileId = i,
				};

			return organizaiton;
		}

		#region Constructor validation
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_CarDataServiceIsNull_ThrowException()
		{
			int id = 1;
			ICarDataService carDataService = null;
			//Parent entities

			var viewModelEdit = new CarEditViewModel(carDataService);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_CarIsNull_ThrowException()
		{
			ICarDataService carDataService = new DesignCarDataService();
			//Parent entities

			Car car = null;
			var viewModelEdit = new CarEditViewModel(car, carDataService);
		}

		#region ParentEntities constructor initialization tests
		/// <summary>
		/// Check if throws exception when some of ParentEntities collections are null
		/// </summary>
		#endregion

		[TestMethod]
		public void Car_New_CanSaveCarIsTrue()
		{
			ICarDataService carDataService = new DesignCarDataService();
			//Parent entities
			var viewModelEdit = new CarEditViewModel(carDataService);

			Assert.IsTrue(viewModelEdit.CanSaveCar);
		}
		#endregion

		#region Child Entities tests


		#endregion
	}
}

