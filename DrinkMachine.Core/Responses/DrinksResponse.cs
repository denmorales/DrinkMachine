namespace DrinkMachine.Core.Responses
{
    /// <summary>
    /// ответ на запрос списка напитков.
    /// </summary>
    public class DrinksResponse : BaseApiResponse
    {
        /// <summary>
        /// список напитков на текущий момент в автомате.
        /// </summary>
        public DrinkInfo[] Drinks {  get; set; }
    }
}