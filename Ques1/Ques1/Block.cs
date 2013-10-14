using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ques1
{
    public class Block   // NO CODE IN THIS CLASS MAY BE CHANGED
    {
        public int MemID;           // unique memory block identifier
        public int Size;            // size of memory block in kB
        private bool Assigned;      // true is memory block being used, otherwise false

        public Block(int MId, int S, bool Status)
        {
            MemID = MId;
            Size = S;
            Assigned = Status;
        }

        public void display()
        {
            Console.WriteLine("MemID: {0} Size (in kB): {1} InUse: {2}", MemID, Size, Assigned);
        }

        public bool inUse()
        {
            return Assigned;
        }

        public void assign()
        {
            Assigned = true;
        }

        public void freeUp()
        {
            Assigned = false;
        }

        public int CompareTo(Block Mem)
        {
            return this.MemID - Mem.MemID;
        }
    }
}
