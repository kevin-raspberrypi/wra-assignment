using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Ques1
{
    public class MemoryManager
    {
        private DoublyLinkedList Used;
        private SinglyLinkedList Free;
        private Stack Corrupt;

        public MemoryManager(string FileName1, string FileName2)
        {
            Used = new DoublyLinkedList();
            Free = new SinglyLinkedList();
            Corrupt = new Stack();

            // initialising Used and Free lists

            readData(FileName1, FileName2);
        }

        // MODIFY THE APPROPRIATE METHODS IN THE RELEVANT PLACES BELOW

        public void allocateMemory(Block MemBlock)   // 10 marks
        /* pre:  Have list of allocated (Used) memory blocks, which could be empty.
         * post: EFFICIENTLY insert the new memory allocation in order of memory block identifier so that the smallest memory identifier is 
         *       at the head of the list. 
         *       Wherever possible, use MUST be made of existing methods. */
        {
            // ADD CODE FOR METHOD allocateMemory BELOW
			//if the memory block is in use
			if (MemBlock.inUse ()) {
				//if the used list is empty, just add it
				if (Used.Count () == 0)
					Used.addFirst (MemBlock);
				//if the used list contains already allocated blocks, add MemBlock in order of identifier
				else {
					DLLNode cur = Used.getFirst ();
					while (cur != null) {
						if (((Block)cur.value ()).MemID > MemBlock.MemID)
							Used.addBefore (MemBlock, cur);
						cur = (DLLNode)cur.next ();
					}
				}
			}
        }

        public void garbageCollect()  // 25 marks
        /* pre:  Have list of allocated (Used) memory blocks and available (Free) memory blocks, each of which could be empty.
         * post: Remove all memory blocks that are not in use from the list of allocated (Used) memory blocks, EFFICIENTLY adding these 
         *       memory blocks into the list of available (Free) memory blocks, ensuring that the list of allocated (Used) memory blocks 
         *       retains its ordering of remaining memory blockc and the list of available (Free) memory blocks is always ordered in 
         *       descending order of memory block size, with the largest available memory block being at the head of the list. 
         *       Wherever possible, use MUST be made of existing methods. 
         *       NOTE: you are required to make use of method decCounter in class DoublyLinkedList appropriately.  */
        {
            // ADD CODE FOR METHOD garbageCollect BELOW
        }

        public Node findMemory(int Size) // 10 marks
        /* pre:  Have list of available (Free) memory blocks, which could be empty.
         * post: EFFICIENTLY find the smallest memory block available that will suite the specified size (does not have to be the exact same
         *       size).
         *       Return a link to the node containing the appropriately sized memory block.  If there is no appropriately sized memory 
         *       block, return null.
         *       The list of available (Free) memory blocks should retain its ordering.
         *       Wherever possible, use MUST be made of existing methods. */
        {

            // ADD CODE FOR METHOD findMemory BELOW
            return null;

        }

        public double findWastage()  // 20 marks
        /* pre:  Have list of available memory blocks (Free) and list of memory blocks detected as being unusable (Corrupt), 
         *       each of which could be empty. Each list is sorted in ascending order of memory block ID, with the first point of access
         *       having the smallest memory block ID value. Memory block IDs are unique within each list, but not necessarily unique
         *       across lists. Each memory block ID in the unusable list appears in the list of available memory blocks as well.
         * post: In ascending order of memory block ID, display those available memory block IDs that are not corrupt.
         *       Determine and return the percentage (a value between 0 and 1) of the total size of the available memory blocks that
         *       are corrupt.
         *       NOTE: Each list may only be processed a MAXIMUM of 1 time.
         *             Make appropriate use of method getNextMemoryBlock (which must also be coded). 
         *             The contents of the lists do not have to be retained. */
        {

            // ADD CODE FOR METHOD findWastage BELOW
            return 0;

        }


        // DO NOT MODIFY ANY OF THE METHODS BELOW

        public void displayLists()
        /* pre:  Have list of allocated (Used) and available (Free) memory blocks, each of which could be empty.
         * post: Display the details for each memory block in each list. */
        {
            Console.WriteLine("List of memory blocks currently allocated: {0}", Used.Count());
            for (DLLNode cur = (DLLNode)Used.getFirst(); cur != null; cur = (DLLNode)cur.next())
                ((Block)cur.value()).display();
            Console.WriteLine();
            Console.WriteLine("List of memory blocks currently available: {0}", Free.Count());
            for (Node cur = Free.getFirst(); cur != null; cur = cur.next())
                ((Block)cur.value()).display();
            Console.WriteLine();
        }

        public void readData(string FileName1, string FileName2)
        /* pre:  Lists of memory block are empty.
         * post: Populates lists with test data from text files. */
        {
            Console.WriteLine("List of allocated memory being populated ......");
            StreamReader Input = new StreamReader(FileName1);
            while (!Input.EndOfStream)
            {
                Block newOne = new Block(int.Parse(Input.ReadLine()), int.Parse(Input.ReadLine()), bool.Parse(Input.ReadLine()));
                allocateMemory(newOne);
            }
            Input = new StreamReader(FileName2);
            while (!Input.EndOfStream)
            {
                Block newOne = new Block(int.Parse(Input.ReadLine()), int.Parse(Input.ReadLine()), false);
                Corrupt.Push(newOne);
            }
            Console.WriteLine("List of allocated memory populated");
            Console.WriteLine();
        }

        public void sortFree()
        /* pre:  Have list of available (Free) memory blocks, which could be empty.
         * post: Uses a priority queue to sort the list in ascending order of memory block ID. */
        {
            PQueue sortQ = new PQueue();
            for (Node cur = Free.getFirst(); cur != null; cur = cur.next())
                sortQ.Enqueue(cur.value());
            Free = new SinglyLinkedList();
            while (sortQ.Count > 0)
                Free.addLast(sortQ.Dequeue());
        }
    }
}
