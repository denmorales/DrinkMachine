using DrinkMachine.Database.Entities;

namespace DrinkMachine.Database
{
    public class DrinkContextInitializer
    {
        public static void InitContext(DrinkContext db)
        {
            db.Database.EnsureCreated();
        }

        public static Drink[] InitialDrinks()
        {
            return new[]
            {
                new Drink { Id = 1, Name = "Пепси кола", Description = "пепси сладкая и газированная" },
                new Drink { Id = 2, Name = "Кока кола лайт", Description = "кока кола холодная" },
            };
        }
    }
}