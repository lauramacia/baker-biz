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
                
                
                Dictionary<InventoryItems, int> inventory = new Dictionary<InventoryItems, int>();
                
                Console.WriteLine("How many apples do you have?");                
                inventory[InventoryItems.Apples] = getValidatedPositiveInt();

                Console.WriteLine("How many pounds of sugar do you have?");
                inventory[InventoryItems.Sugar] = getValidatedPositiveInt();

                Console.WriteLine("How many pounds of flour do you have?");
                inventory[InventoryItems.Flour] = getValidatedPositiveInt();

                int applePies = ApplePieCalculations.MaxApplePies(inventory);                
                Console.WriteLine("You can make " + applePies + " apple pie(s)!");

                Dictionary<InventoryItems, int> leftovers = ApplePieCalculations.ApplePieLeftovers(applePies, inventory);
                Console.WriteLine(leftovers[InventoryItems.Apples] + " apple(s) left over,\n" + 
                    leftovers[InventoryItems.Sugar] + " lbs sugar left over,\n" + 
                    leftovers[InventoryItems.Flour] + " lbs flour left over.\n");

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

        public static int MaxApplePies(Dictionary<InventoryItems, int> inventory)
        {       
            int applesMaxPies = inventory[InventoryItems.Apples] / APPLES_PER_PIE;
            int sugarMaxPies = inventory[InventoryItems.Sugar] / SUGAR_POUNDS_PER_PIE;
            int flourMaxPies = inventory[InventoryItems.Flour] / FLOUR_POUNDS_PER_PIE;

            return Math.Min(Math.Min(applesMaxPies, sugarMaxPies), flourMaxPies);                            
        }

        public static Dictionary<InventoryItems, int> ApplePieLeftovers(int pies, Dictionary<InventoryItems, int> inventory)
        {
            Dictionary<InventoryItems, int> leftovers = new Dictionary<InventoryItems, int>();
            leftovers[InventoryItems.Apples] = inventory[InventoryItems.Apples] - (pies * APPLES_PER_PIE);
            leftovers[InventoryItems.Sugar] = inventory[InventoryItems.Sugar] - (pies * SUGAR_POUNDS_PER_PIE);
            leftovers[InventoryItems.Flour] = inventory[InventoryItems.Flour] - (pies * FLOUR_POUNDS_PER_PIE);

            return leftovers;
        }
    }

    public enum InventoryItems
    {
        Apples,
        Sugar,
        Flour
    };

}
