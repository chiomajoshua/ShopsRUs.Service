using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShopsRUs.Service.Features.Customer.Services;
using ShopsRUs.Service.Features.Discounts.Services;
using ShopsRUs.Service.Features.Invoice.Models;
using System;
using System.Threading.Tasks;
using static ShopsRUs.Service.Core.Storage.Models.Enums;

namespace ShopsRUs.Service.Features.Invoice.Services
{
    public interface IInvoiceService
    {
        /// <summary>
        /// Calculate Customer Bill
        /// </summary>
        /// <param name="invoiceRequest"></param>
        /// <returns></returns>
        Task<double> CalculateBill(InvoiceRequest invoiceRequest);               
    }


    public class InvoiceService : IInvoiceService
    {
        private bool discountApplied = false;
        private readonly ILogger<InvoiceService> _logger;
        private readonly IUserService _userService;
        private readonly IDiscountService _discountService;
        public InvoiceService(ILogger<InvoiceService> logger, IUserService userService, IDiscountService discountService)
        {
            _logger = logger;
            _userService = userService;
            _discountService = discountService;
        }

        public async Task<double> CalculateBill(InvoiceRequest invoiceRequest)
        {
            try
            {
                var userInformation = await _userService.FindUserByEmailAsync(invoiceRequest.Email);
                if (userInformation != null)
                {
                    var discount = await _discountService.GetDiscountPercentageByType(userInformation.Designation);
                    if (!invoiceRequest.IsGrocery)
                    {
                        if (userInformation.Designation == CustomerType.Affiliate.ToString())
                        {
                            invoiceRequest.Total = (invoiceRequest.Total / 100) * discount;
                            discountApplied = true;
                        }
                        else
                        if (userInformation.Designation == CustomerType.Employee.ToString() && !discountApplied)
                        {
                            invoiceRequest.Total = (invoiceRequest.Total / 100) * discount;
                            discountApplied = true;
                        }
                        else
                        if (userInformation.Designation == CustomerType.Customer.ToString() && userInformation.DateRegistered.Year > 2 && !discountApplied)
                        {
                            invoiceRequest.Total = (invoiceRequest.Total / 100) * discount;
                            discountApplied = true;
                        }
                    }
                }
                return CalculateCashDiscount(invoiceRequest.Total);
            }
            catch(Exception ce)
            {
                _logger.LogError($"An Error Occured {JsonConvert.SerializeObject(ce)}");
                return 0.0;
            }
        }

        private double CalculateCashDiscount(double total)
        {
            if (total < 100)
                return total;
            var discountedAmount = Convert.ToInt32(total) / 100;
            return total - (discountedAmount * 5);
        }
    }
}
