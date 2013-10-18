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
			//if the used list is empty, just add it
			if (Used.Count () == 0)
				Used.addFirst (MemBlock);
			//if the used list contains already allocated blocks, add MemBlock in order of identifier
			else {
				//get the first block
				DLLNode cur = Used.getFirst ();
				//traverse the list and check where to add the block
				do {
					//when the id of the current block is bigger than the one to be inserted, 
					//add it before the current block.
					if (((Block)cur.value ()).CompareTo(MemBlock) >= 0) {
						Used.addBefore (MemBlock, cur);
						return; // break out the loop and stop, otherwise we are wasting time
					}
					cur = (DLLNode)cur.next ();
				} while (cur != null);
				Used.addLast (MemBlock); //if the whole list has been traversed, add the block at the end.
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
			DLLNode cur = Used.getFirst (), prev, next;
			while (cur != null) {
				Block curBlock = (Block)cur.value ();
				if (!curBlock.inUse ()) {
					//add to free memory
					if (Free.Count () == 0)
						Free.addLast (curBlock);
					else {
						Node curFree = Free.getFirst ();
						bool notadded = true; //need to keep track so we can stop iterating if added
						do {
							if (curBlock.Size >= ((Block)curFree.value ()).Size) {
								Free.addBefore (curBlock, curFree);
								notadded = false;
							}
							curFree = curFree.next ();
							//if we reach the end of the list and it hasn't been added, add it to the end
							if (curFree == null && notadded)
								Free.addLast (curBlock);
						} while (curFree != null && notadded);
					}
					//Some serious thinking went on here...This cleans up the Used list to remove the node the moved to free
					//get the previous, and next nodes of the current node
					prev = cur.previous ();
					next = (DLLNode)cur.next ();
					//if we are at the first node currently, then previous will be null and must be dealt with accordingly
					if (prev == null)
						Used.removeFirst ();
					//if are at the end of the list, we neet to do something similar
					else if (next == null)
						Used.removeLast ();
					else { //otherwise we need to move some pointers around and shit starts to get complicated!
						prev.setNext (next);
						next.setPrevious (prev);
						//and DON'T forget to decrease the counter for how many used memory blocks there are...
						Used.decCounter ();
					}
					//remove the links to any other nodes from the current item in Used. Maybe it'll go away if we ignore it
					cur.setPrevious (null);
					cur.setNext (null);
					//tell the program where the next item is...otherwise it'll eat up all your memory and get diabetes and fall apart!
					cur = next;
				} else if (cur.next () != null)
					cur = (DLLNode)cur.next ();
				else
					cur = null;
			}
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
			Node cur = Free.getFirst(), prev = null;
			do {
				//if the current block is smaller that the given size, then the previous block must be big enough...simple as that!
				if (((Block)cur.value()).Size < Size)
					return prev;
				prev = cur;
				cur = cur.next();
			} while (cur != null);
			//if we got to the end, just make sure that the last block is big enough
			if (cur == null && ((Block)prev.value ()).Size > Size)
				return prev;
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
			sortFree ();
			Block free = getNext (Free), corrupt = getNext (Corrupt);
			Queue notWasted = new Queue ();
			double wasted = 0;
			int sizeFree = 0;
			while (free != null && corrupt != null) {
				if (free.CompareTo (corrupt) < 0) {
					notWasted.Enqueue (free);
					sizeFree += free.Size;
					free = getNext (Free);
				} else if (corrupt.CompareTo (free) < 0) {
					wasted += corrupt.Size;
					corrupt = getNext (Corrupt);
				} else {
					sizeFree += free.Size;
					wasted += corrupt.Size;
					free = getNext(Free);
					corrupt = getNext (Corrupt);
				}
			}
			while (corrupt != null && free == null) {
				wasted += corrupt.Size;
				corrupt = getNext (Corrupt);
			}
			while (free != null && corrupt == null) {
				notWasted.Enqueue (free);
				sizeFree += free.Size;
				free = getNext (Free);
			}
			while (notWasted.Count > 0)
				Console.WriteLine (((Block)notWasted.Dequeue()).MemID);
            return wasted/sizeFree;
        }

		private Block getNext(Object List)
		{
			if (List is SinglyLinkedList)
				if (((SinglyLinkedList)List).Count () == 0)
					return null;
				else
					return (Block)((SinglyLinkedList)List).removeFirst ().value ();
			else if (List is Stack)
				if (((Stack)List).Count == 0)
					return null;
				else
					return (Block)((Stack)List).Pop ();
			else
				return null;
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
