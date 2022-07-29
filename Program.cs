using System;

namespace baker_biz
{
    class Program
    {
        static void Main()
        {
            // want to maximize the number of apple pies we can make.
            // it takes 3 apples, 2 lbs of sugar and 1 pound of flour to make 1 apple pie
            // Requirement 1: add cinnamon(optional), 1 tsp per pie.Once cinnamon is exhausted, you just make pies without it
            // this is intended to run on .NET Core                       

            do
            {

                ApplePie.ApplePieInventory applePieInventory = new ApplePie.ApplePieInventory();

                Console.WriteLine("How many apples do you have?");
                applePieInventory.Apples = getValidatedPositiveInt();

                Console.WriteLine("How many pounds of sugar do you have?");
                applePieInventory.Sugar = getValidatedPositiveInt();

                Console.WriteLine("How many pounds of flour do you have?");
                applePieInventory.Flour = getValidatedPositiveInt();

                Console.WriteLine("How many teaspoons (tsp) of cinnamon do you have?");
                applePieInventory.Cinnamon = getValidatedPositiveInt();

                ApplePie.CalculateMaxApplePies(ref applePieInventory);

                Console.WriteLine(applePieInventory.Apples + " apple(s) left over,\n" +
                    applePieInventory.Sugar + " lbs sugar left over,\n" +
                    applePieInventory.Flour + " lbs flour left over.\n" +
                    applePieInventory.Cinnamon + " cinnamon teaspoons left over.\n");

                Console.WriteLine("\n\nEnter to calculate, 'q' to quit!");
            } while (!string.Equals(Console.ReadLine()?.ToLower(), "q"));

        }

        public static int getValidatedPositiveInt()
        {
            string readLine = Console.ReadLine() ?? "";
            int readInt;

            while (!int.TryParse(readLine, out readInt) || readInt < 0)
            {
                Console.WriteLine("Amount must be zero or a positive whole number, please try again.");
                readLine = Console.ReadLine() ?? "";
            }

            return readInt;
        }
    }

    public class ApplePie
    {
        public class ApplePieInventory
        {
            public int Apples { get; set; }
            public int Sugar { get; set; }
            public int Flour { get; set; }
            public int Cinnamon { get; set; }

            public ApplePieInventory(int apples, int sugar, int flour, int cinnamon)
            {
                Apples = apples;
                Sugar = sugar;
                Flour = flour;
                Cinnamon = cinnamon;
            }

            public ApplePieInventory()
            {
                Apples = 0;
                Sugar = 0;
                Flour = 0;
                Cinnamon = 0;
            }
        }

        // These cannot be zero
        public const int APPLES_PER_PIE = 3;
        public const int SUGAR_POUNDS_PER_PIE = 2;
        public const int FLOUR_POUNDS_PER_PIE = 1;
        public const int CINNAMON_TSP_PER_PIE = 1;

        public static int CalculateMaxApplePies(ref ApplePieInventory inventory)
        {
            int maxApplePies = 0;
            if (inventory.Cinnamon > 0)
            {
                maxApplePies = MaxApplePiesCinnamon(inventory);
                CalculateApplePieLeftovers(maxApplePies, ref inventory);
            }
            Console.WriteLine("You can make " + maxApplePies + " cinnamon apple pie(s),\n");

            maxApplePies = MaxApplePiesNoCinnamon(inventory);
            Console.WriteLine("and " + maxApplePies + " apple pie(s) without cinnamon.\n");
            CalculateApplePieLeftovers(maxApplePies, ref inventory);

            return maxApplePies;
        }

        public static int MaxApplePiesCinnamon(ApplePieInventory inventory)
        {
            List<int> maxsList = new List<int>
            {
                inventory.Apples / APPLES_PER_PIE,
                inventory.Sugar / SUGAR_POUNDS_PER_PIE,
                inventory.Flour / FLOUR_POUNDS_PER_PIE,
                inventory.Cinnamon / CINNAMON_TSP_PER_PIE
            };

            return maxsList.Min();
        }

        public static int MaxApplePiesNoCinnamon(ApplePieInventory inventory)
        {
            List<int> maxsList = new List<int>
            {
                inventory.Apples / APPLES_PER_PIE,
                inventory.Sugar / SUGAR_POUNDS_PER_PIE,
                inventory.Flour / FLOUR_POUNDS_PER_PIE
            };

            return maxsList.Min();
        }

        public static void CalculateApplePieLeftovers(int pies, ref ApplePieInventory inventory)
        {
            inventory.Apples -= pies * APPLES_PER_PIE;
            inventory.Sugar -= pies * SUGAR_POUNDS_PER_PIE;
            inventory.Flour -= pies * FLOUR_POUNDS_PER_PIE;
            if (inventory.Cinnamon > 0)
            {
                inventory.Cinnamon -= pies * CINNAMON_TSP_PER_PIE;
            }
        }
    }
}
