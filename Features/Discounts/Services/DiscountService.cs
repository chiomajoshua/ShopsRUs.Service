using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShopsRUs.Service.Core.Storage;
using ShopsRUs.Service.Features.Discounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Service.Features.Discounts.Services
{
    public interface IDiscountService
    {
        /// <summary>
        /// Create a discount Type
        /// </summary>
        /// <param name="createDiscountTypeRequest"></param>
        /// <returns></returns>
        Task<Discount> CreateDiscountAsync(CreateDiscountTypeRequest createDiscountTypeRequest);

        /// <summary>
        /// Get All Discounts
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Discount>> GetAllDiscounts();

        /// <summary>
        /// Get Discount Percentage by Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<double> GetDiscountPercentageByType(string type);

        /// <summary>
        /// Check If Discount Exists
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<bool> IsDiscountExists(string type);
    }

    public class DiscountService : IDiscountService
    {
        private readonly ILogger<DiscountService> _logger;
        private readonly ShopsRusDbContext _shopsRusDbContext;
        private readonly IMapper _mapper;

        public DiscountService(ILogger<DiscountService> logger, ShopsRusDbContext shopsRusDbContext, IMapper mapper)
        {
            _logger = logger;
            _shopsRusDbContext = shopsRusDbContext;
            _mapper = mapper;
        }
        public async Task<Discount> CreateDiscountAsync(CreateDiscountTypeRequest createDiscountTypeRequest)
        {
            try
            {
                await _shopsRusDbContext.Discounts.AddAsync(createDiscountTypeRequest.ToDbDiscount());
                if(await _shopsRusDbContext.SaveChangesAsync() > 0)
                    return _mapper.Map<Discount>(await _shopsRusDbContext.Discounts.FirstOrDefaultAsync(discount => discount.DiscountType == createDiscountTypeRequest.DiscountType));
                return null;
            }
            catch(Exception ce)
            {
                _logger.LogError($"An Error Occurred {JsonConvert.SerializeObject(ce)}");
                return null;
            }
        }

        public async Task<IEnumerable<Discount>> GetAllDiscounts()
        {
            try
            {
                return _mapper.Map<List<Discount>>(await _shopsRusDbContext.Discounts.ToListAsync());
            }
            catch(Exception ce)
            {
                _logger.LogError($"An Error Occurred {JsonConvert.SerializeObject(ce)}");
                return null;
            }
        }

        public async Task<bool> IsDiscountExists(string type)
        {
            try
            {
                return await _shopsRusDbContext.Discounts.AnyAsync(discount => discount.DiscountType == type);
            }
            catch (Exception ce)
            {
                _logger.LogError($"An Error Occurred {JsonConvert.SerializeObject(ce)}");
                return false;
            }
        }

        public async Task<double> GetDiscountPercentageByType(string type)
        {
            try
            {
                return await _shopsRusDbContext.Discounts.Where(discount => discount.DiscountType == type).Select(percentage => percentage.DiscountPercentage).FirstOrDefaultAsync();
            }
            catch(Exception ce)
            {
                _logger.LogError($"An Error Occurred {JsonConvert.SerializeObject(ce)}");
                return 0.0;
            }
        }
    }
}
