using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Ques1
{
    class PQueue : Queue     // NO CODE IN THIS CLASS MAY BE CHANGED
    {
        public override void Enqueue(object newOne)
        {
            if (this.Count == 0)
            {
                base.Enqueue(newOne);
                return;
            }
            Queue temp = new Queue();
            while ((this.Count > 0) && (((Block)this.Peek()).CompareTo((Block)newOne) < 0))
                temp.Enqueue(this.Dequeue());
            temp.Enqueue(newOne);
            while (this.Count > 0)
                temp.Enqueue(this.Dequeue());
            while (temp.Count > 0)
                base.Enqueue(temp.Dequeue());
        }
    }
}
