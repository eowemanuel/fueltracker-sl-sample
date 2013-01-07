using System;
using System.Collections.ObjectModel;
using FuelTracker.Web;
using System.Collections.Generic;

namespace FuelTracker.DesignModel
{
	public class DesignCars : ObservableCollection<Car>
	{
		private const int entitiesCount = 10;
		public DesignCars():this(entitiesCount)
		{
		}
		
		public DesignCars(int entitiesCount)
		{
			var carsList = GenerateDesignCarsList(entitiesCount);
			foreach (var car in carsList)
			{
				this.Add(car);
			}
		}

		public IList<Car> GenerateDesignCarsList(int entitiesCount)
		{
			IList<Car> generatedSource = new List<Car>();

			for (int i = 2; i < entitiesCount; i++)
			{
				var car =
		new Car
		{
			Engine = string.Format("Engine {0:5}", i),
			EngineVolume = i,
			FuelTracks = null,//TO DO: Update design model property assignment
			Id = i,
			Manufacturer = string.Format("Manufacturer {0:5}", i),
			ManufacturingDate = DateTime.Now.AddDays(i),
			Model = string.Format("Model {0:5}", i),
			Name = string.Format("Name {0:5}", i),
			Types = string.Format("Types {0:5}", i),
			UserProfile = null,//TO DO: Update design model property assignment
			UserProfileId = i,
		};
				generatedSource.Add(car);
			}

			return generatedSource;
		}

	}
	
}

