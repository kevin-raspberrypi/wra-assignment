using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ques1
{
    public class DLLNode : Node  // NO CODE IN THIS CLASS MAY BE CHANGED
    {
        private DLLNode BackLink;

        public DLLNode(Object theElement, DLLNode theLink, DLLNode theBackLink)
            : base(theElement, theLink)
        {
            BackLink = theBackLink;
        }

        public DLLNode(Object theElement)
            : base(theElement)
        {
            BackLink = null;
        }

        public DLLNode()
            : base()
        {
            BackLink = null;
        }

        public DLLNode previous()
        {
            return BackLink;
        }

        public void setPrevious(DLLNode theLink)
        {
            BackLink = theLink;
        }
    }
}
