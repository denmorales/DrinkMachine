using System;
using System.Linq;
using System.Threading.Tasks;

using DrinkMachine.Core.Responses;
using DrinkMachine.Database;
using DrinkMachine.Database.Entities;

using Microsoft.EntityFrameworkCore;

namespace DrinkMachine.Provider
{
    public class DrinkProvider
    {
        private readonly DrinkContext _context;

        public DrinkProvider(DrinkContext context)
        {
            _context = context;
        }

        public async Task<DrinksResponse> GetDrinks()
        {
            try
            {
                var dbDrinks = await _context.Drinks
                    .Include(dr => dr.Prices)
                    .Include(dr => dr.Consignments)
                    .ToArrayAsync();

                DrinkInfo[] drinks = dbDrinks.Select(dr => new DrinkInfo
                {
                    Id = dr.Id,
                    Name = dr.Name,
                    Description = dr.Description,
                    Price = dr.Prices.OrderByDescending(p => p.SetDate).FirstOrDefault()?.Price,
                    Quantity = dr.Consignments?.Sum(c => c.Quantity) ?? 0,
                }).ToArray();
                var response = new DrinksResponse
                {
                    ResultCode = ApiResultCode.Success,
                    Message = $"успешно получено {drinks.Length} напитков",
                    Drinks = drinks,
                };

                return response;
            }
            catch (Exception e)
            {
                return new DrinksResponse { ResultCode = ApiResultCode.ServerError, Message = e.ToString() };
            }
        }

        /// <summary>
        /// устанавливает новую цену на напиток.
        /// </summary>
        /// <param name="drinkId"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> AddPrice(int drinkId, decimal price)
        {
            try
            {
                if (!await _context.Drinks.AnyAsync(dr => dr.Id == drinkId))
                {
                    return new BaseApiResponse
                        { ResultCode = ApiResultCode.NotFound, Message = $"напиток с id {drinkId} не существует в базе" };
                }

                DrinkPrice newPrice = new DrinkPrice { DrinkId = drinkId, Price = price, SetDate = DateTimeOffset.Now };
                await _context.DrinkPrices.AddAsync(newPrice);
                await _context.SaveChangesAsync();
                return new BaseApiResponse
                {
                    ResultCode = ApiResultCode.Success,
                    Message = $"цена напитка {drinkId} установлена на {price:F}"
                };
            }
            catch (Exception e)
            {
                return new BaseApiResponse { ResultCode = ApiResultCode.ServerError, Message = e.ToString() };
            }
        }

        /// <summary>
        /// добавить напиток в автомат.
        /// </summary>
        /// <param name="drinkId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse> AddConsignment(int drinkId, int quantity)
        {
            try
            {
                if (!await _context.Drinks.AnyAsync(dr => dr.Id == drinkId))
                {
                    return new BaseApiResponse
                        { ResultCode = ApiResultCode.NotFound, Message = $"напиток с id {drinkId} не существует в базе" };
                }

                Consignment newConsignment = new Consignment
                {
                    DrinkId = drinkId, Quantity = quantity, SetDate = DateTimeOffset.Now
                };

                await _context.Consignments.AddAsync(newConsignment);

                await _context.SaveChangesAsync();

                return new BaseApiResponse
                {
                    ResultCode = ApiResultCode.Success,
                    Message = $"поставлено {quantity} напитка {drinkId}",
                };
            }
            catch (Exception e)
            {
                return new BaseApiResponse { ResultCode = ApiResultCode.ServerError, Message = e.ToString() };
            }
        }
    }
}
