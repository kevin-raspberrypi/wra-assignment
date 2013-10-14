using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ques1
{
    public class DoublyLinkedList  // NO CODE IN THIS CLASS MAY BE CHANGED
    {
        protected int counter;
        protected DLLNode head;     // points to first node in the list
        protected DLLNode tail;     // points to last node in the list

        public DoublyLinkedList()
        {
            counter = 0;
            head = null;
            tail = null;
        }

        public int Count()
        {
            return counter;
        }

        public void decCounter()  // new method added for test
        {
            counter--;
        }

        public DLLNode getFirst()
        {
            return head;
        }

        public DLLNode getLast()
        {
            return tail;
        }

        public void addFirst(Object newItem)
        /* pre:  Have object to be added to calling doubly linked list object, which may be empty.
       * post: newItem is the element of the FIRST node of the doubly linked list.  All other existing nodes of the
       *       doubly linked list retain their ordering AFTER the new first node.
       *       The counter is modified to reflect the addition of a new node to the doubly linked list. */
        {

            // ADD CODE FOR METHOD addFirst BELOW

            DLLNode newNode = new DLLNode(newItem);
            newNode.setNext(head);
            if (head != null)  // already has at least 1 node
                head.setPrevious(newNode);
            else // the new node is also the tail node
                tail = newNode;
            head = newNode;
            counter++;
        }

        public void addLast(Object newItem)
        /* pre:  Have object to be added to calling doubly linked list object, which may be empty.
        * post: newItem is the element of the LAST node of the doubly linked list.  All other existing nodes of the
        *       doubly linked list retain their ordering BEFORE the new last node.
        *       The counter is modified to reflect the addition of a new node to the doubly linked list. */
        {

            // ADD CODE FOR METHOD addLast BELOW

            if (tail == null)
            {
                addFirst(newItem);
                return;
            }
            DLLNode newNode = new DLLNode(newItem);
            newNode.setPrevious(tail);
            tail.setNext(newNode);
            tail = newNode;
            counter++;
        }

        public DLLNode removeFirst()
        /* pre:  Have at least one node in calling doubly linked list object.
         * post: Return the node removed, which is the first node in the list.
         *       The counter is modified to reflect the removal of the first node from the doubly linked list. */
        {

            // ADD CODE FOR METHOD removeFirst BELOW

            DLLNode cur = head;
            head = (DLLNode)cur.next();
            if (cur.next() != null)
            {
                ((DLLNode)cur.next()).setPrevious(null);
                cur.setNext(null);
            }
            else // only one node in list
                tail = null;
            counter--;
            return cur;
        }

        public DLLNode removeLast()
        /* pre:  Have at least one node in calling doubly linked list object.
         * post: Return the node removed, which is the last node in the list.
         *       The counter is modified to reflect the removal of the last node from the doubly linked list. */
        {

            // ADD CODE FOR METHOD removeLast BELOW

            DLLNode cur = tail;
            if (cur.previous() == null)  // then only one node in DLL
            {
                head = null;
                tail = null;
            }
            else
            {
                tail = cur.previous();
                tail.setNext(null);
                cur.setPrevious(null);
            }
            counter--;
            return cur;
        }

        public void addBefore(Object newItem, DLLNode link)
        /* pre:  Have object to be added to calling doubly linked list object, and a link in the doubly linked list BEFORE
         *       which the newItem's node must be added.
         * post: newItem is the element of the added node of the doubly linked list.  All other existing nodes of the
         *       doubly linked list retain their ordering relevant to the position of the newly added node.
         *       The counter is modified to reflect the addition of a new node to the doubly linked list. */
        {

            // ADD CODE FOR METHOD addBefore BELOW

            if (link == null)  // list either empty or must be added at end of list
            {
                this.addLast(newItem);
                return;
            }
            DLLNode newNode = new DLLNode(newItem);
            if (head == link)  // must be added as first node
            {
                this.addFirst(newItem);
                return;
            }
            newNode.setPrevious(link.previous());
            newNode.setNext(link);
            link.previous().setNext(newNode);
            link.setPrevious(newNode);
            counter++;
        }

        public void addAfter(Object newItem, DLLNode link)
        /* pre:  Have object to be added to calling doubly linked list object, and a link in the doubly linked list AFTER
         *       which the newItem's node must be added.
         * post: newItem is the element of the added node of the doubly linked list.  All other existing nodes of the
         *       doubly linked list retain their ordering relevant to the position of the newly added node.
         *       The counter is modified to reflect the addition of a new node to the doubly linked list. */
        {

            // ADD CODE FOR METHOD addAfter BELOW

            if (link == null)
            {
                this.addLast(newItem);
                return;
            }
            DLLNode newNode = new DLLNode(newItem);
            newNode.setNext(link.next());
            if ((DLLNode)(link.next()) != null)
                ((DLLNode)(link.next())).setPrevious(newNode);
            newNode.setPrevious(link);
            link.setNext(newNode);
            counter++;
        }

        public void Clear()
        /* pre:  Have calling doubly linked list object.
         * post: Empty the list. */
        {

            // ADD CODE FOR METHOD Clear BELOW

            this.head = null;
            this.tail = null;
            this.counter = 0;
        }
    }
}
