﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("FuelTracker.Models", "FuelTrack_Car", "Cars", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(FuelTracker.Web.Car), "FuelTracks", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(FuelTracker.Web.FuelTrack), true)]
[assembly: EdmRelationshipAttribute("FuelTracker.Models", "UserProfile_Cars", "UserProfiles", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(FuelTracker.Web.UserProfile), "Cars", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(FuelTracker.Web.Car), true)]

#endregion

namespace FuelTracker.Web
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class FuelTrackerEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new FuelTrackerEntities object using the connection string found in the 'FuelTrackerEntities' section of the application configuration file.
        /// </summary>
        public FuelTrackerEntities() : base("name=FuelTrackerEntities", "FuelTrackerEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new FuelTrackerEntities object.
        /// </summary>
        public FuelTrackerEntities(string connectionString) : base(connectionString, "FuelTrackerEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new FuelTrackerEntities object.
        /// </summary>
        public FuelTrackerEntities(EntityConnection connection) : base(connection, "FuelTrackerEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Car> Cars
        {
            get
            {
                if ((_Cars == null))
                {
                    _Cars = base.CreateObjectSet<Car>("Cars");
                }
                return _Cars;
            }
        }
        private ObjectSet<Car> _Cars;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<FuelTrack> FuelTracks
        {
            get
            {
                if ((_FuelTracks == null))
                {
                    _FuelTracks = base.CreateObjectSet<FuelTrack>("FuelTracks");
                }
                return _FuelTracks;
            }
        }
        private ObjectSet<FuelTrack> _FuelTracks;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<sysdiagram> sysdiagrams
        {
            get
            {
                if ((_sysdiagrams == null))
                {
                    _sysdiagrams = base.CreateObjectSet<sysdiagram>("sysdiagrams");
                }
                return _sysdiagrams;
            }
        }
        private ObjectSet<sysdiagram> _sysdiagrams;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<UserProfile> UserProfiles
        {
            get
            {
                if ((_UserProfiles == null))
                {
                    _UserProfiles = base.CreateObjectSet<UserProfile>("UserProfiles");
                }
                return _UserProfiles;
            }
        }
        private ObjectSet<UserProfile> _UserProfiles;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Cars EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToCars(Car car)
        {
            base.AddObject("Cars", car);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the FuelTracks EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToFuelTracks(FuelTrack fuelTrack)
        {
            base.AddObject("FuelTracks", fuelTrack);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the sysdiagrams EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTosysdiagrams(sysdiagram sysdiagram)
        {
            base.AddObject("sysdiagrams", sysdiagram);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the UserProfiles EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToUserProfiles(UserProfile userProfile)
        {
            base.AddObject("UserProfiles", userProfile);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="FuelTracker.Models", Name="Car")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Car : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Car object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="engineVolume">Initial value of the EngineVolume property.</param>
        /// <param name="manufacturingDate">Initial value of the ManufacturingDate property.</param>
        /// <param name="userProfileId">Initial value of the UserProfileId property.</param>
        public static Car CreateCar(global::System.Int32 id, global::System.Double engineVolume, global::System.DateTime manufacturingDate, global::System.Int32 userProfileId)
        {
            Car car = new Car();
            car.Id = id;
            car.EngineVolume = engineVolume;
            car.ManufacturingDate = manufacturingDate;
            car.UserProfileId = userProfileId;
            return car;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Manufacturer
        {
            get
            {
                return _Manufacturer;
            }
            set
            {
                OnManufacturerChanging(value);
                ReportPropertyChanging("Manufacturer");
                _Manufacturer = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Manufacturer");
                OnManufacturerChanged();
            }
        }
        private global::System.String _Manufacturer;
        partial void OnManufacturerChanging(global::System.String value);
        partial void OnManufacturerChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Model
        {
            get
            {
                return _Model;
            }
            set
            {
                OnModelChanging(value);
                ReportPropertyChanging("Model");
                _Model = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Model");
                OnModelChanged();
            }
        }
        private global::System.String _Model;
        partial void OnModelChanging(global::System.String value);
        partial void OnModelChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double EngineVolume
        {
            get
            {
                return _EngineVolume;
            }
            set
            {
                OnEngineVolumeChanging(value);
                ReportPropertyChanging("EngineVolume");
                _EngineVolume = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("EngineVolume");
                OnEngineVolumeChanged();
            }
        }
        private global::System.Double _EngineVolume;
        partial void OnEngineVolumeChanging(global::System.Double value);
        partial void OnEngineVolumeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Types
        {
            get
            {
                return _Types;
            }
            set
            {
                OnTypesChanging(value);
                ReportPropertyChanging("Types");
                _Types = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Types");
                OnTypesChanged();
            }
        }
        private global::System.String _Types;
        partial void OnTypesChanging(global::System.String value);
        partial void OnTypesChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Engine
        {
            get
            {
                return _Engine;
            }
            set
            {
                OnEngineChanging(value);
                ReportPropertyChanging("Engine");
                _Engine = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Engine");
                OnEngineChanged();
            }
        }
        private global::System.String _Engine;
        partial void OnEngineChanging(global::System.String value);
        partial void OnEngineChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime ManufacturingDate
        {
            get
            {
                return _ManufacturingDate;
            }
            set
            {
                OnManufacturingDateChanging(value);
                ReportPropertyChanging("ManufacturingDate");
                _ManufacturingDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ManufacturingDate");
                OnManufacturingDateChanged();
            }
        }
        private global::System.DateTime _ManufacturingDate;
        partial void OnManufacturingDateChanging(global::System.DateTime value);
        partial void OnManufacturingDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 UserProfileId
        {
            get
            {
                return _UserProfileId;
            }
            set
            {
                OnUserProfileIdChanging(value);
                ReportPropertyChanging("UserProfileId");
                _UserProfileId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("UserProfileId");
                OnUserProfileIdChanged();
            }
        }
        private global::System.Int32 _UserProfileId;
        partial void OnUserProfileIdChanging(global::System.Int32 value);
        partial void OnUserProfileIdChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("FuelTracker.Models", "FuelTrack_Car", "FuelTracks")]
        public EntityCollection<FuelTrack> FuelTracks
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<FuelTrack>("FuelTracker.Models.FuelTrack_Car", "FuelTracks");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<FuelTrack>("FuelTracker.Models.FuelTrack_Car", "FuelTracks", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("FuelTracker.Models", "UserProfile_Cars", "UserProfiles")]
        public UserProfile UserProfile
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<UserProfile>("FuelTracker.Models.UserProfile_Cars", "UserProfiles").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<UserProfile>("FuelTracker.Models.UserProfile_Cars", "UserProfiles").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<UserProfile> UserProfileReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<UserProfile>("FuelTracker.Models.UserProfile_Cars", "UserProfiles");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<UserProfile>("FuelTracker.Models.UserProfile_Cars", "UserProfiles", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="FuelTracker.Models", Name="FuelTrack")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class FuelTrack : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new FuelTrack object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="fuelingDate">Initial value of the FuelingDate property.</param>
        /// <param name="dateExpiration">Initial value of the DateExpiration property.</param>
        /// <param name="quantity">Initial value of the Quantity property.</param>
        /// <param name="carId">Initial value of the CarId property.</param>
        /// <param name="distance">Initial value of the Distance property.</param>
        /// <param name="carMiliage">Initial value of the CarMiliage property.</param>
        public static FuelTrack CreateFuelTrack(global::System.Int32 id, global::System.DateTime fuelingDate, global::System.DateTime dateExpiration, global::System.Double quantity, global::System.Int32 carId, global::System.Double distance, global::System.Double carMiliage)
        {
            FuelTrack fuelTrack = new FuelTrack();
            fuelTrack.Id = id;
            fuelTrack.FuelingDate = fuelingDate;
            fuelTrack.DateExpiration = dateExpiration;
            fuelTrack.Quantity = quantity;
            fuelTrack.CarId = carId;
            fuelTrack.Distance = distance;
            fuelTrack.CarMiliage = carMiliage;
            return fuelTrack;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime FuelingDate
        {
            get
            {
                return _FuelingDate;
            }
            set
            {
                OnFuelingDateChanging(value);
                ReportPropertyChanging("FuelingDate");
                _FuelingDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("FuelingDate");
                OnFuelingDateChanged();
            }
        }
        private global::System.DateTime _FuelingDate;
        partial void OnFuelingDateChanging(global::System.DateTime value);
        partial void OnFuelingDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime DateExpiration
        {
            get
            {
                return _DateExpiration;
            }
            set
            {
                OnDateExpirationChanging(value);
                ReportPropertyChanging("DateExpiration");
                _DateExpiration = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DateExpiration");
                OnDateExpirationChanged();
            }
        }
        private global::System.DateTime _DateExpiration;
        partial void OnDateExpirationChanging(global::System.DateTime value);
        partial void OnDateExpirationChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                OnQuantityChanging(value);
                ReportPropertyChanging("Quantity");
                _Quantity = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Quantity");
                OnQuantityChanged();
            }
        }
        private global::System.Double _Quantity;
        partial void OnQuantityChanging(global::System.Double value);
        partial void OnQuantityChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 CarId
        {
            get
            {
                return _CarId;
            }
            set
            {
                OnCarIdChanging(value);
                ReportPropertyChanging("CarId");
                _CarId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CarId");
                OnCarIdChanged();
            }
        }
        private global::System.Int32 _CarId;
        partial void OnCarIdChanging(global::System.Int32 value);
        partial void OnCarIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double Distance
        {
            get
            {
                return _Distance;
            }
            set
            {
                OnDistanceChanging(value);
                ReportPropertyChanging("Distance");
                _Distance = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Distance");
                OnDistanceChanged();
            }
        }
        private global::System.Double _Distance;
        partial void OnDistanceChanging(global::System.Double value);
        partial void OnDistanceChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Double CarMiliage
        {
            get
            {
                return _CarMiliage;
            }
            set
            {
                OnCarMiliageChanging(value);
                ReportPropertyChanging("CarMiliage");
                _CarMiliage = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CarMiliage");
                OnCarMiliageChanged();
            }
        }
        private global::System.Double _CarMiliage;
        partial void OnCarMiliageChanging(global::System.Double value);
        partial void OnCarMiliageChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("FuelTracker.Models", "FuelTrack_Car", "Cars")]
        public Car Car
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Car>("FuelTracker.Models.FuelTrack_Car", "Cars").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Car>("FuelTracker.Models.FuelTrack_Car", "Cars").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Car> CarReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Car>("FuelTracker.Models.FuelTrack_Car", "Cars");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Car>("FuelTracker.Models.FuelTrack_Car", "Cars", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="FuelTracker.Models", Name="sysdiagram")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class sysdiagram : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new sysdiagram object.
        /// </summary>
        /// <param name="name">Initial value of the name property.</param>
        /// <param name="principal_id">Initial value of the principal_id property.</param>
        /// <param name="diagram_id">Initial value of the diagram_id property.</param>
        public static sysdiagram Createsysdiagram(global::System.String name, global::System.Int32 principal_id, global::System.Int32 diagram_id)
        {
            sysdiagram sysdiagram = new sysdiagram();
            sysdiagram.name = name;
            sysdiagram.principal_id = principal_id;
            sysdiagram.diagram_id = diagram_id;
            return sysdiagram;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String name
        {
            get
            {
                return _name;
            }
            set
            {
                OnnameChanging(value);
                ReportPropertyChanging("name");
                _name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("name");
                OnnameChanged();
            }
        }
        private global::System.String _name;
        partial void OnnameChanging(global::System.String value);
        partial void OnnameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 principal_id
        {
            get
            {
                return _principal_id;
            }
            set
            {
                Onprincipal_idChanging(value);
                ReportPropertyChanging("principal_id");
                _principal_id = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("principal_id");
                Onprincipal_idChanged();
            }
        }
        private global::System.Int32 _principal_id;
        partial void Onprincipal_idChanging(global::System.Int32 value);
        partial void Onprincipal_idChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 diagram_id
        {
            get
            {
                return _diagram_id;
            }
            set
            {
                if (_diagram_id != value)
                {
                    Ondiagram_idChanging(value);
                    ReportPropertyChanging("diagram_id");
                    _diagram_id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("diagram_id");
                    Ondiagram_idChanged();
                }
            }
        }
        private global::System.Int32 _diagram_id;
        partial void Ondiagram_idChanging(global::System.Int32 value);
        partial void Ondiagram_idChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> version
        {
            get
            {
                return _version;
            }
            set
            {
                OnversionChanging(value);
                ReportPropertyChanging("version");
                _version = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("version");
                OnversionChanged();
            }
        }
        private Nullable<global::System.Int32> _version;
        partial void OnversionChanging(Nullable<global::System.Int32> value);
        partial void OnversionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.Byte[] definition
        {
            get
            {
                return StructuralObject.GetValidValue(_definition);
            }
            set
            {
                OndefinitionChanging(value);
                ReportPropertyChanging("definition");
                _definition = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("definition");
                OndefinitionChanged();
            }
        }
        private global::System.Byte[] _definition;
        partial void OndefinitionChanging(global::System.Byte[] value);
        partial void OndefinitionChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="FuelTracker.Models", Name="UserProfile")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class UserProfile : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new UserProfile object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        public static UserProfile CreateUserProfile(global::System.Int32 id)
        {
            UserProfile userProfile = new UserProfile();
            userProfile.Id = id;
            return userProfile;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                OnUserNameChanging(value);
                ReportPropertyChanging("UserName");
                _UserName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("UserName");
                OnUserNameChanged();
            }
        }
        private global::System.String _UserName;
        partial void OnUserNameChanging(global::System.String value);
        partial void OnUserNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String City
        {
            get
            {
                return _City;
            }
            set
            {
                OnCityChanging(value);
                ReportPropertyChanging("City");
                _City = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("City");
                OnCityChanged();
            }
        }
        private global::System.String _City;
        partial void OnCityChanging(global::System.String value);
        partial void OnCityChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("FuelTracker.Models", "UserProfile_Cars", "Cars")]
        public EntityCollection<Car> Cars
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Car>("FuelTracker.Models.UserProfile_Cars", "Cars");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Car>("FuelTracker.Models.UserProfile_Cars", "Cars", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}
