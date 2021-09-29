using System.Collections.Generic;

namespace DrinkMachine.Database.Entities
{
    public class Drink
    {
        /// <summary>
        /// id напитка
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// краткое название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// описание напитка
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// цены этого напитка.
        /// </summary>
        public ICollection<DrinkPrice> Prices { get; set; }

        /// <summary>
        /// поставки этого напитка.
        /// </summary>
        public ICollection<Consignment> Consignments { get; set; }
    }
}