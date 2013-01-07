
namespace FuelTracker.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies CarMetadata as the class
    // that carries additional metadata for the Car class.
    [MetadataTypeAttribute(typeof(Car.CarMetadata))]
    public partial class Car
    {

        // This class allows you to attach custom attributes to properties
        // of the Car class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CarMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CarMetadata()
            {
            }

            public string Engine { get; set; }

            public double EngineVolume { get; set; }

            public EntityCollection<FuelTrack> FuelTracks { get; set; }

            public int Id { get; set; }

            public string Manufacturer { get; set; }

            public DateTime ManufacturingDate { get; set; }

            public string Model { get; set; }

            public string Name { get; set; }

            public string Types { get; set; }

            public UserProfile UserProfile { get; set; }

            public int UserProfileId { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies FuelTrackMetadata as the class
    // that carries additional metadata for the FuelTrack class.
    [MetadataTypeAttribute(typeof(FuelTrack.FuelTrackMetadata))]
    public partial class FuelTrack
    {

        // This class allows you to attach custom attributes to properties
        // of the FuelTrack class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class FuelTrackMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private FuelTrackMetadata()
            {
            }

            public Car Car { get; set; }

            public int CarId { get; set; }

            public double CarMiliage { get; set; }

            public DateTime DateExpiration { get; set; }

            public double Distance { get; set; }

            public DateTime FuelingDate { get; set; }

            public int Id { get; set; }

            public double Quantity { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies UserProfileMetadata as the class
    // that carries additional metadata for the UserProfile class.
    [MetadataTypeAttribute(typeof(UserProfile.UserProfileMetadata))]
    public partial class UserProfile
    {

        // This class allows you to attach custom attributes to properties
        // of the UserProfile class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class UserProfileMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private UserProfileMetadata()
            {
            }

            public EntityCollection<Car> Cars { get; set; }

            public string City { get; set; }

            public int Id { get; set; }

            public string UserName { get; set; }
        }
    }
}
