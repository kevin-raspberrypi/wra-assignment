using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Ques2
{
    public class MemoryManager
    {
        private BTNode Free;

        public MemoryManager(string FileName)
        {
            Free = null;

            // initialising Free list

            readData(FileName);
        }

        // MODIFY THE APPROPRIATE METHODS IN THE RELEVANT PLACES BELOW

        public Block findMemory(int Size) // 15 marks
        /* pre:  Have list of available (Free) memory blocks, which could be empty. There is no particular ordering of nodes in the tree.
         * post: Using RECURSIVE parsing of the tree, find and return the smallest memory block available that will suite the specified size 
         *       (does not have to be the exact same size).
         *       If there is no appropriately sized memory block, return null.
         *       The list of available (Free) memory blocks should retain its ordering.
         *       Wherever possible, use MUST be made of existing methods. */
        {

            // ADD CODE FOR METHOD findMemory BELOW 
            Block smallest = recursiveFindMemory(Free, Size, (Block)Free.value());
            if (smallest.Size > Size)
                return smallest;
            return null;
        }

        private Block recursiveFindMemory(BTNode T, int Size, Block CurMin)
        {
            if (T != null)
            {
                if (((Block)T.value()).Size < CurMin.Size)
                {
                    Block temp = (Block)T.value();
                    recursiveFindMemory(T.left(), Size, temp);
                    recursiveFindMemory(T.right(), Size, temp);
                }
                else
                {
                    recursiveFindMemory(T.left(), Size, CurMin);
                    recursiveFindMemory(T.right(), Size, CurMin);
                }
            }
            return CurMin;
        }


        // DO NOT MODIFY ANY METHODS BELOW

        public void readData(string FileName)
        /* pre:  List of available memory blocks is empty.
         * post: Populates list with test data from text file. */
        {
            Console.WriteLine("List of available memory being populated ......");
            StreamReader Input = new StreamReader(FileName);
            while (!Input.EndOfStream)
            {
                Block newOne = new Block(int.Parse(Input.ReadLine()), int.Parse(Input.ReadLine()), bool.Parse(Input.ReadLine()));
                addMemory(newOne);
            }
            Console.WriteLine("List of available memory populated");
            Console.WriteLine();
        }

        private void addMemory(Block newOne)
        {
            Queue visitQ = new Queue();
            BTNode newTree = new BTNode(newOne);
            if (Free == null)
                Free = newTree;
            else
            {
                visitQ.Enqueue(Free);
                while (true)
                {
                    BTNode curTree = (BTNode)visitQ.Dequeue();
                    if (curTree.left() == null)
                    {
                        curTree.setLeft(newTree);
                        return;
                    }
                    if (curTree.right() == null)
                    {
                        curTree.setRight(newTree);
                        return;
                    }
                    visitQ.Enqueue(curTree.left());
                    visitQ.Enqueue(curTree.right());
                }
            }
        }
    }
}
