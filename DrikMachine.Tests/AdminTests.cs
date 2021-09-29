using System.Linq;
using System.Threading.Tasks;
using DrinkMachine.API.Controllers;
using DrinkMachine.Core.Responses;
using NUnit.Framework;

namespace DrinkMachine.Tests
{
    public class AdminTests : BaseApiControllerTest
    {
        [Test]
        public async Task GetDrinksTest()
        {
            string getDrinksUrl = $"api/Admin/{nameof(AdminController.Drinks)}";
            DrinksResponse addGroupResponse = await this.GetApiMethod<DrinksResponse>(getDrinksUrl);

            Assert.AreEqual(ApiResultCode.Success, addGroupResponse.ResultCode, addGroupResponse.Message);
            CollectionAssert.IsNotEmpty(addGroupResponse.Drinks);
        }

        [TestCase(25)]
        [TestCase(5)]
        [TestCase(-1)]
        public async Task ChangePriceTest(decimal newPrice)
        {

            string getDrinksUrl = $"api/Admin/{nameof(AdminController.Drinks)}";
            DrinksResponse drinksResponse = await this.GetApiMethod<DrinksResponse>(getDrinksUrl);
            Assert.AreEqual(ApiResultCode.Success, drinksResponse.ResultCode, drinksResponse.Message);
            CollectionAssert.IsNotEmpty(drinksResponse.Drinks);
            
            var drink = drinksResponse.Drinks[0];
            string priceUrl = $"api/Admin/{nameof(AdminController.Price)}/{drink.Id}?price={newPrice}";
            BaseApiResponse priceResponse = await CallApiMethod<object, BaseApiResponse>(null, priceUrl, "POST");
            Assert.AreEqual(ApiResultCode.Success, priceResponse.ResultCode, priceResponse.Message);

            drinksResponse = await this.GetApiMethod<DrinksResponse>(getDrinksUrl);
            Assert.AreEqual(ApiResultCode.Success, drinksResponse.ResultCode, drinksResponse.Message);
            var drinkAfter = drinksResponse.Drinks.FirstOrDefault(dr => dr.Id == drink.Id);
            Assert.NotNull(drinkAfter);
            Assert.AreEqual(newPrice, drinkAfter.Price, "цены после изменения неверные");
        }

        [Test]
        public async Task ConsignmentTest()
        {
            string getDrinksUrl = $"api/Admin/{nameof(AdminController.Drinks)}";
            DrinksResponse drinksResponse = await this.GetApiMethod<DrinksResponse>(getDrinksUrl);
            Assert.AreEqual(ApiResultCode.Success, drinksResponse.ResultCode, drinksResponse.Message);
            CollectionAssert.IsNotEmpty(drinksResponse.Drinks);

            var drink = drinksResponse.Drinks[0];
            int oldQuantity = drink.Quantity;
            int addedQuantity = 10;
            string consUrl = $"api/Admin/{nameof(AdminController.Consignment)}/{drink.Id}?quantity={addedQuantity}";
            BaseApiResponse priceResponse = await CallApiMethod<object, BaseApiResponse>(null, consUrl, "POST");
            Assert.AreEqual(ApiResultCode.Success, priceResponse.ResultCode, priceResponse.Message);

            drinksResponse = await this.GetApiMethod<DrinksResponse>(getDrinksUrl);
            Assert.AreEqual(ApiResultCode.Success, drinksResponse.ResultCode, drinksResponse.Message);
            var drinkAfter = drinksResponse.Drinks.FirstOrDefault(dr => dr.Id == drink.Id);
            Assert.NotNull(drinkAfter);
            Assert.AreEqual(oldQuantity + addedQuantity, drinkAfter.Quantity, "количества после изменения неверные");
        }
    }
}