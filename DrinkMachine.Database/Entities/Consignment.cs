using System;

namespace DrinkMachine.Database.Entities
{
    /// <summary>
    /// поставка напитка в автомат.
    /// </summary>
    public class Consignment
    {
        /// <summary>
        /// id поставки.
        /// </summary>
        public int Id {  get; set; }

        /// <summary>
        /// количество напитка в поставке.
        /// </summary>
        public int Quantity {  get; set; }

        /// <summary>
        /// время, когда она была сделана.
        /// </summary>
        public DateTimeOffset? SetDate { get; set; }

        /// <summary>
        /// id напитка.
        /// </summary>
        public int DrinkId { get; set; }

        /// <summary>
        /// ссылка на напиток
        /// </summary>
        public Drink Drink { get; set; }
    }
}