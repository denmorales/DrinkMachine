namespace DrinkMachine.Core.Responses
{
    /// <summary>
    /// данные напитка для показа
    /// </summary>
    public class DrinkInfo
    {
        /// <summary>
        /// id напитка
        /// </summary>
        public int Id {  get; set; }

        /// <summary>
        /// краткое название
        /// </summary>
        public string Name {  get; set; }

        /// <summary>
        /// описание напитка
        /// </summary>
        public string Description {  get; set; }
        
        /// <summary>
        /// текущая цена напитка.
        /// </summary>
        public decimal? Price {  get; set; }

        /// <summary>
        /// количество сейчас в автомате.
        /// </summary>
        public int Quantity { get; set; }
    }
}