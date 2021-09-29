using System;

namespace DrinkMachine.Database.Entities
{
    public class DrinkPrice
    {
        /// <summary>
        /// id цены.
        /// </summary>
        public int Id {  get; set; }

        /// <summary>
        /// стоимость в рублях.
        /// </summary>
        public decimal Price {  get; set; }

        /// <summary>
        /// время, когда она была установлена
        /// </summary>
        public DateTimeOffset? SetDate {  get; set; }

        /// <summary>
        /// id напитка.
        /// </summary>
        public int DrinkId {  get; set; }

        /// <summary>
        /// ссылка на напиток
        /// </summary>
        public Drink Drink {  get; set; }
    }
}