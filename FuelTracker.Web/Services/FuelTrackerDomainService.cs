
namespace FuelTracker.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using FuelTracker.Web;


    // Implements application logic using the FuelTrackerEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class FuelTrackerDomainService : LinqToEntitiesDomainService<FuelTrackerEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Cars' query.
        [Query(IsDefault = true)]
        public IQueryable<Car> GetCars()
        {
            return this.ObjectContext.Cars;
        }

        public void InsertCar(Car car)
        {
            if ((car.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(car, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Cars.AddObject(car);
            }
        }

        public void UpdateCar(Car currentCar)
        {
            this.ObjectContext.Cars.AttachAsModified(currentCar, this.ChangeSet.GetOriginal(currentCar));
        }

        public void DeleteCar(Car car)
        {
            if ((car.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(car, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Cars.Attach(car);
                this.ObjectContext.Cars.DeleteObject(car);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'FuelTracks' query.
        [Query(IsDefault = true)]
        public IQueryable<FuelTrack> GetFuelTracks()
        {
            return this.ObjectContext.FuelTracks;
        }

        public void InsertFuelTrack(FuelTrack fuelTrack)
        {
            if ((fuelTrack.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(fuelTrack, EntityState.Added);
            }
            else
            {
                this.ObjectContext.FuelTracks.AddObject(fuelTrack);
            }
        }

        public void UpdateFuelTrack(FuelTrack currentFuelTrack)
        {
            this.ObjectContext.FuelTracks.AttachAsModified(currentFuelTrack, this.ChangeSet.GetOriginal(currentFuelTrack));
        }

        public void DeleteFuelTrack(FuelTrack fuelTrack)
        {
            if ((fuelTrack.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(fuelTrack, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.FuelTracks.Attach(fuelTrack);
                this.ObjectContext.FuelTracks.DeleteObject(fuelTrack);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'UserProfiles' query.
        [Query(IsDefault = true)]
        public IQueryable<UserProfile> GetUserProfiles()
        {
            return this.ObjectContext.UserProfiles;
        }

        public void InsertUserProfile(UserProfile userProfile)
        {
            if ((userProfile.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(userProfile, EntityState.Added);
            }
            else
            {
                this.ObjectContext.UserProfiles.AddObject(userProfile);
            }
        }

        public void UpdateUserProfile(UserProfile currentUserProfile)
        {
            this.ObjectContext.UserProfiles.AttachAsModified(currentUserProfile, this.ChangeSet.GetOriginal(currentUserProfile));
        }

        public void DeleteUserProfile(UserProfile userProfile)
        {
            if ((userProfile.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(userProfile, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.UserProfiles.Attach(userProfile);
                this.ObjectContext.UserProfiles.DeleteObject(userProfile);
            }
        }
    }
}


