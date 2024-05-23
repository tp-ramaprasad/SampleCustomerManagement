using AutoMapper;
using Pinewood.Customers.MVC.Models;

namespace Pinewood.Customers.MVC.MappingProfiles
{
    /// <summary>
    /// create mapping profile for auomapper class
    /// </summary>
    public class CustomMapperProfile : Profile
    {
        public CustomMapperProfile()
        {
            CreateMap<UpdateCustomerModel, GetCustomerModel>();
            CreateMap<GetCustomerModel, UpdateCustomerModel>();
        }
    }

}
