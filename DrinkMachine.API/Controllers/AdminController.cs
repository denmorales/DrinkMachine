using System.Threading.Tasks;
using DrinkMachine.Core.Responses;
using DrinkMachine.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrinkMachine.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public AdminController(DrinkProvider provider)
        {
            Provider = provider;
        }

        /// <summary>
        /// провайдер бизнес-логики
        /// </summary>
        private DrinkProvider Provider { get; }

        /// <summary>
        /// получает список всех напитков с текущими количествами и ценами.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<DrinksResponse> Drinks()
        {
            return await Provider.GetDrinks();
        }

        /// <summary>
        /// устанавливает новую цену на напиток.
        /// </summary>
        /// <param name="drinkId"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{drinkId:int}")]
        public async Task<BaseApiResponse> Price(int drinkId, decimal price)
        {
            return await Provider.AddPrice(drinkId, price);
        }

        /// <summary>
        /// добавляет напиток в автомат.
        /// </summary>
        /// <param name="drinkId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{drinkId:int}")]
        public async Task<BaseApiResponse> Consignment(int drinkId, int quantity)
        {
            return await Provider.AddConsignment(drinkId, quantity);
        }
    }
}
