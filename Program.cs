using System;

namespace Interview_Refactor1
{
    class Program
    {
        static void Main()
        {            
            // want to maximize the number of apple pies we can make.
            // it takes 3 apples, 2 lbs of sugar and 1 pound of flour to make 1 apple pie
            // this is intended to run on .NET Core                       
            
            do
            {
                
                
                Dictionary<Ingredients, int> inventory = new Dictionary<Ingredients, int>();
                
                Console.WriteLine("How many apples do you have?");                
                inventory[Ingredients.Apples] = getValidatedPositiveInt();

                Console.WriteLine("How many pounds of sugar do you have?");
                inventory[Ingredients.Sugar] = getValidatedPositiveInt();

                Console.WriteLine("How many pounds of flour do you have?");
                inventory[Ingredients.Flour] = getValidatedPositiveInt();

                int applePies = ApplePieCalculations.MaxApplePies(inventory);                
                Console.WriteLine("You can make " + applePies + " apple pie(s)!");

                Dictionary<Ingredients, int> leftovers = ApplePieCalculations.ApplePieLeftovers(applePies, inventory);
                Console.WriteLine(leftovers[Ingredients.Apples] + " apple(s) left over,\n" + 
                    leftovers[Ingredients.Sugar] + " lbs sugar left over,\n" + 
                    leftovers[Ingredients.Flour] + " lbs flour left over.\n");

                Console.WriteLine("\n\nEnter to calculate, 'q' to quit!");
            } while (!string.Equals(Console.ReadLine()?.ToLower(), "q"));

        }

        public static int getValidatedPositiveInt()
        {
            String readLine = Console.ReadLine() ?? "";
            int readInt;

            while (!Int32.TryParse(readLine, out readInt) || readInt < 0)
            {
                Console.WriteLine("Amount must be a positive whole number, please try again.");
                readLine = Console.ReadLine() ?? "";
            }

            return readInt;
        }        
    }

    public class ApplePieCalculations
    {
        // These cannot be zero
        public const int APPLES_PER_PIE = 3;
        public const int SUGAR_POUNDS_PER_PIE = 2;
        public const int FLOUR_POUNDS_PER_PIE = 1;

        public static int MaxApplePies(Dictionary<Ingredients, int> inventory)
        {       
            int applesMaxPies = inventory[Ingredients.Apples] / APPLES_PER_PIE;
            int sugarMaxPies = inventory[Ingredients.Sugar] / SUGAR_POUNDS_PER_PIE;
            int flourMaxPies = inventory[Ingredients.Flour] / FLOUR_POUNDS_PER_PIE;

            return Math.Min(Math.Min(applesMaxPies, sugarMaxPies), flourMaxPies);                            
        }

        public static Dictionary<Ingredients, int> ApplePieLeftovers(int pies, Dictionary<Ingredients, int> inventory)
        {
            Dictionary<Ingredients, int> leftovers = new Dictionary<Ingredients, int>();
            leftovers[Ingredients.Apples] = inventory[Ingredients.Apples] - (pies * APPLES_PER_PIE);
            leftovers[Ingredients.Sugar] = inventory[Ingredients.Sugar] - (pies * SUGAR_POUNDS_PER_PIE);
            leftovers[Ingredients.Flour] = inventory[Ingredients.Flour] - (pies * FLOUR_POUNDS_PER_PIE);

            return leftovers;
        }
    }

    public enum Ingredients
    {
        Apples,
        Sugar,
        Flour
    };

}
