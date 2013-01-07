using System;

public class CarSimpleDto
{
	public CarSimpleDto()
	{
	}
	
	public System.String Engine{ get; set; }
	public System.Double EngineVolume{ get; set; }
	public System.ServiceModel.DomainServices.Client.EntityCollection<FuelTracker.Web.FuelTrack> FuelTracks{ get; set; }
	public System.Int32 Id{ get; set; }
	public System.String Manufacturer{ get; set; }
	public System.DateTime ManufacturingDate{ get; set; }
	public System.String Model{ get; set; }
	public System.String Name{ get; set; }
	public System.String Types{ get; set; }
	public FuelTracker.Web.UserProfile UserProfile{ get; set; }
	public System.Int32 UserProfileId{ get; set; }

	public void ConfigureMapping()
	{
		//Mapper.CreateMapFuelTracker.Web.Car, CarSimpleDto>
			//.ForMember(dest => dest.Engine, opt => opt.MapFrom(src => src.Engine))
			//.ForMember(dest => dest.EngineVolume, opt => opt.MapFrom(src => src.EngineVolume))
			//.ForMember(dest => dest.FuelTracks, opt => opt.MapFrom(src => src.FuelTracks))
			//.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
			//.ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.Manufacturer))
			//.ForMember(dest => dest.ManufacturingDate, opt => opt.MapFrom(src => src.ManufacturingDate))
			//.ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
			//.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
			//.ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types))
			//.ForMember(dest => dest.UserProfile, opt => opt.MapFrom(src => src.UserProfile))
			//.ForMember(dest => dest.UserProfileId, opt => opt.MapFrom(src => src.UserProfileId))
			//;
	}

}

