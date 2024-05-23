using AutoMapper;
using Pinewood.Customers.API.Models.AuthModels;
using Pinewood.Customers.API.Models.Common;
using Pinewood.Customers.API.Models.Request;
using Pinewood.Customers.API.Models.Response;
using Pinewood.Customers.Core.Entities;

namespace Pinewood.Customers.API.MappingProfiles
{
    /// <summary>
    /// create mapping profile for auomapper class
    /// </summary>
    public class CustomMapperProfile : Profile
    {
        public CustomMapperProfile()
        {
            CreateMap<GenderModel, Gender>();
            CreateMap<Gender, GenderModel>();
            CreateMap<CountryModel, Country>();
            CreateMap<Country, CountryModel>();
            CreateMap<Preference, PreferenceModel>();
            CreateMap<PreferenceModel, Preference>();
            CreateMap<AddressModel, Address>()
                .ForMember(dest => dest.CountryId,
                opt => opt.MapFrom(src => src.CountryId > 0 ? src.CountryId : null));
            CreateMap<Address, AddressModel>();           
            CreateMap<Contact, CustomerContactModel>();
            CreateMap<CustomerContactModel, Contact>();
            CreateMap<UpdateCustomerModel, Customer>();
            CreateMap<Customer, UpdateCustomerModel>();
            CreateMap<AddCustomerModel, Customer>()
                .ForMember(dest => dest.GenderId,
                opt => opt.MapFrom(src => src.GenderId > 0 ? src.GenderId : null));
            CreateMap<Customer, GetCustomerByIdModel>();
            //CreateMap<UserModel, ApplicationUser>();
            CreateMap<ApplicationUser, UserModel>();
            CreateMap<Services.Models.AuthenticationResponse, AuthenticationResponse>();
        }
    }

}
