using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ques2
{
    class Program   // NO CODE IN THIS CLASS MAY BE CHANGED
    {
        static void Main(string[] args)
        {
            MemoryManager OS = new MemoryManager("Memory.txt");
            int[] test = {256, 150, 128, 50};
            for (int i = 0; i <= test.GetUpperBound(0); i++)
            {
                Block tester = OS.findMemory(test[i]);
                if (tester == null)
                    Console.WriteLine("No appropriate memory block can accommodate {0}kB", test[i]);
                else
                    Console.WriteLine("Memory block {0} can accommodate {1}kB", tester.MemID, test[i]);
            }
            Console.WriteLine();
            Console.WriteLine("ANSWER QUESTION 2 BONUS MARK REFERRING TO THE OUTPUT DISPLAYED DIRECTLY ABOVE");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();

            Console.WriteLine("Processing terminated ....... press enter to continue");
            Console.ReadLine();
        }
    }
}
