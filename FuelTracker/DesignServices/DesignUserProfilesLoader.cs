using Microsoft.Windows.Data.DomainServices;
using System.Collections.Generic;
using FuelTracker.Web;
using System.ComponentModel;
using FuelTracker.DesignModel;

namespace FuelTracker.DesignServices
{
    public class DesignUserProfilesLoader : CollectionViewLoader
    {
        public static void LoadSource()
        {
            var generatedSource = new DesignUserProfiles();
            source = generatedSource;
        }

        private static IEnumerable<UserProfile> source;

        public IEnumerable<UserProfile> Source
        {
            get
            {
                if (DesignUserProfilesLoader.source == null)
                {
                    LoadSource();
                }
                return DesignUserProfilesLoader.source;
            }
        }

        public override bool CanLoad { get { return true; } }
        public override void Load(object userState)
        {
            this.OnLoadCompleted(new AsyncCompletedEventArgs(null, false, userState));
        }
    }
}
