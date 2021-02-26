using AutoMapper;
using ShopsRUs.Service.Core.Storage.Models;
using ShopsRUs.Service.Features.Discounts.Models;
using ShopsRUs.Service.Features.Customer.Models;

namespace ShopsRUs.Service.Core.Mapping.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShopRusUser, User>().AfterMap((source, destination) =>
            {
                destination.UserId = source.UserId;
                destination.Email = source.Email;
                destination.Firstname = source.FirstName;
                destination.Lastname = source.LastName;
                destination.Phone = source.PhoneNumber;
                destination.Blocked = source.Enabled;
                destination.Name = $"{source.FirstName} {source.LastName}";
            });

            CreateMap<Discounts, Discount>();
        }
    }
}