using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ques1
{
    public class Node   // NO CODE IN THIS CLASS MAY BE CHANGED
    {
        protected Object Element;  // data object
        protected Node Link;

        public Node(Object theElement, Node theLink)
        {
            Element = theElement;
            Link = theLink;
        }
        
        public Node(Object theElement)
        {
            Element = theElement;
            Link = null;
        }

        public Node()
        {
            Element = null;
            Link = null;
        }

        public Node next()
        {
            return Link;
        }

        public void setNext(Node theLink)
        {
            Link = theLink;
        }

        public Object value()
        {
            return Element;
        }

        public void setValue(Object theElement)
        {
            Element = theElement;
        }
    }
}
