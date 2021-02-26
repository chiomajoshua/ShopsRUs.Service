using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopsRUs.Service.Core.Error.Exceptions;
using ShopsRUs.Service.Core.Identity;
using ShopsRUs.Service.Core.Storage;
using ShopsRUs.Service.Core.Storage.Models;
using ShopsRUs.Service.Features.Customer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Service.Features.Customer.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Create a new user account
        /// </summary>
        /// <param name="createUserRequest"></param>
        /// <returns></returns>
        Task<User> CreateUserAsync(CreateUserRequest createUserRequest);
        /// <summary>
        /// Find user by email / username 
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        Task<User> FindUserByEmailAsync(string emailAddress);
        /// <summary>
        /// Check if the user is authorized to access sld application
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsAuthorizedAsync(string userName);

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAllCustomers();

        Task<User> GetCustomerById(string Id);
        
    }
    public class UserService : IUserService
    {
        private readonly UserManager<ShopRusUser> _userManager;
        private readonly ILogger<UserService> _logger;
        private readonly ShopsRusDbContext _shopsRusDbContext;
        private readonly IMapper _mapper;

        public UserService(UserManager<ShopRusUser> userManager, ILogger<UserService> logger, ShopsRusDbContext shopsRusDbContext, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _shopsRusDbContext = shopsRusDbContext;
            _mapper = mapper;
        }

        private bool ValidateStaffEmail(string email)
        {
            if (email.ToLower().Contains("@shopsrus.co"))
                return true;
            return false;
        }

        public async Task<User> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            var randomDigit = new Random();
            var user = new ShopRusUser
            {
                Email = createUserRequest.Email,
                FirstName = createUserRequest.Firstname,
                LastName = createUserRequest.Lastname,
                PhoneNumber = createUserRequest.Phone,
                UserName = createUserRequest.Email,
                Enabled = true,
                DateRegistered = DateTime.Now,
                Address = createUserRequest.Address,
                Gender = createUserRequest.Gender,
                Title = createUserRequest.Title,
                UserId = "CUST-" + randomDigit.Next(100, 99999),
                Designation = createUserRequest.Designation,
                IsAdmin = false
            };

            if (ValidateStaffEmail(user.Email))
            {
                if(user.Designation == "Customer" || user.Designation == "Affiliate") throw new ShopsRusDbIntegrityException("Designation Cannot Be Customer");
                user.Designation = "Employee";
                user.DateEmployed = DateTime.Now;
                user.UserId = "SHPRUSTF-" + randomDigit.Next(100, 99999);
                user.IsAdmin = createUserRequest.IsAdmin;
            }
            var result = await _userManager.CreateAsync(user, createUserRequest.Password);
            if (!result.Succeeded)
            {
                if (result.Errors.Any(x => x.Code == "DuplicateUserName"))
                    throw new ShopsRusDbIntegrityException("DuplicateUserName");
                throw new ApiException(result.Errors.FirstOrDefault().Code, System.Net.HttpStatusCode.BadRequest);
            }

            _logger.LogInformation($"RegisterAccount: Account Creation Successful for {createUserRequest.Email}");

            if (ValidateStaffEmail(user.Email))
            {
                if (createUserRequest.IsAdmin) await _userManager.AddToRoleAsync(user, EmployeeRoleName.AdminRoleName);
                else await _userManager.AddToRoleAsync(user, EmployeeRoleName.StaffRoleName);
            }
            var userProfile = await _userManager.FindByEmailAsync(createUserRequest.Email);
            return userProfile.ToUser();
        }

        public async Task<User> FindUserByEmailAsync(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress)) throw new UserNotFoundException();

            var isUserExist = await _userManager.FindByEmailAsync(emailAddress);
            if (isUserExist != null)
            {
                _logger.LogInformation($"FindUserByEmail: User, {emailAddress} Found");
                return isUserExist.ToUser();
            }
            _logger.LogInformation($"FindUserByEmail: User, {emailAddress} Not Found");
            return null;
        }

        public async Task<bool> IsAuthorizedAsync(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);
            if (result == null) return false;
            return result.Enabled;
        }

        public async Task<IEnumerable<User>> GetAllCustomers()
        {
            var customers = await _shopsRusDbContext.Users.Where(customerList => customerList.Designation == "Employee" || customerList.Designation == "Affiliate").ToListAsync();
            if (customers.Count > 0) return _mapper.Map<List<User>>(customers);
            return null;
        }

        public async Task<User> GetCustomerById(string Id)
        {
            var customers = await _shopsRusDbContext.Users.FirstOrDefaultAsync(customer => customer.UserId == Id);
            if (customers != null) return _mapper.Map<User>(customers);
            return null;
        }
    }
}