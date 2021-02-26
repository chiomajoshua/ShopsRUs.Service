using ShopsRUs.Service.Core.Storage.Models;
using System.Linq;

namespace ShopsRUs.Service.Features.Customer
{
    public static class UserExtensions
    {
        public static Models.User ToUser(this ShopRusUser shopRusUser)
        {
            return new Models.User
            {
                Blocked = !shopRusUser.Enabled,
                Email = shopRusUser.Email,
                Firstname = shopRusUser.FirstName,
                Phone = shopRusUser.PhoneNumber,
                Lastname = shopRusUser.LastName,
                Name = $"{shopRusUser.FirstName} {shopRusUser.LastName}",
                UserName = shopRusUser.UserName,
                Roles = shopRusUser.Roles.Select(x => x.Role.Name),
                UserId = shopRusUser.UserId,
                Designation = shopRusUser.Designation,
                DateRegistered = shopRusUser.DateRegistered
            };
        }

        public static ShopRusUser ToDbUser(this Models.User account, bool IsEnabled)
        {
            return new ShopRusUser
            {
                Email = account.Email,
                FirstName = account.Firstname,
                LastName = account.Lastname,
                PhoneNumber = account.Phone,
                UserName = account.Email,
                Enabled = IsEnabled
            };
        }
    }
}
