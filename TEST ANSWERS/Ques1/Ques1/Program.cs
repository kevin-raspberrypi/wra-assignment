using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ques1
{
    class Program   // NO CODE IN THIS CLASS MAY BE CHANGED
    {
        static void Main(string[] args)
        {
            MemoryManager OS = new MemoryManager("Memory.txt", "Corrupt.txt");
            OS.garbageCollect();
            OS.displayLists();
            Console.WriteLine("ANSWER QUESTION 1 BONUS MARK a) REFERRING TO THE OUTPUT DISPLAYED DIRECTLY ABOVE");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            int[] test = {256, 150, 128, 50};
            for (int i = 0; i <= test.GetUpperBound(0); i++)
            {
                Node tester = OS.findMemory(test[i]);
                if (tester == null)
                    Console.WriteLine("No appropriate memory block can accommodate {0}kB", test[i]);
                else
                    Console.WriteLine("Memory block {0} can accommodate {1}kB", ((Block)tester.value()).MemID, test[i]);
            }
            Console.WriteLine();
            Console.WriteLine("ANSWER QUESTION 1 BONUS MARK b) REFERRING TO THE OUTPUT DISPLAYED DIRECTLY ABOVE");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            OS.sortFree();
            Console.WriteLine("Corrupt memory blocks are:");
            double wastage = OS.findWastage();
            Console.WriteLine("Percentage wastage of memory blocks is {0}%", wastage * 100);
            Console.WriteLine();
            Console.WriteLine("ANSWER QUESTION 1 BONUS MARK c) REFERRING TO THE OUTPUT DISPLAYED DIRECTLY ABOVE");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();

            Console.WriteLine("Processing terminated ....... press enter to continue");
            Console.ReadLine();
        }
    }
}
