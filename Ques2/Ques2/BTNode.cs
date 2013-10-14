using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ques2
{
    public class BTNode   // NO CODE IN THIS CLASS MAY BE CHANGED
    {
        private Object Value;
        private BTNode Left;
        private BTNode Right;

        public BTNode()
        {
            Value = null;
            Left = null;
            Right = null;
        }

        public BTNode(Object Data)
        {
            Value = Data;
            Left = null;
            Right = null;
        }

        public BTNode(Object Data, BTNode L, BTNode R)
        {
            Value = Data;
            Left = L;
            Right = R;
        }

        public BTNode left()
        {
            return Left;
        }

        public BTNode right()
        {
            return Right;
        }

        public void setLeft(BTNode newLeft)
        {
            Left = newLeft;
        }

        public void setRight(BTNode newRight)
        {
            Right = newRight;
        }

        public Object value()
        {
            return Value;
        }

        public void setValue(Object Data)
        {
            Value = Data;
        }

        public BTNode clear()
        {
            return null;
        }
   }
}
