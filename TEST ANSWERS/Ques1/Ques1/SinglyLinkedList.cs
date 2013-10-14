using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ques1
{
    public class SinglyLinkedList  // NO CODE IN THIS CLASS MAY BE CHANGED
    {
        protected int counter;
        protected Node head;    // points to first node in the list

        public SinglyLinkedList() 
        {
            counter = 0;
            head = null;
        }

        public int Count()  
        {
            return counter;
        }

        public Node getFirst() 
        {
            return head;
        }

        public void addFirst(Object newItem) 
        /* pre:  Have object to be added to calling singly linked list object, which may be empty.
         * post: newItem is the element of the FIRST node of the singly linked list.  All other existing nodes of the
         *       singly linked list retain their ordering AFTER the new first node.
         *       The counter is modified to reflect the addition of a new node to the singly linked list. */
        {
            Node newNode = new Node(newItem);
            newNode.setNext(head);
            head = newNode;
            counter++;
        }

        public void addLast(Object newItem)
        /* pre:  Have object to be added to calling singly linked list object, which may be empty.
         * post: newItem is the element of the LAST node of the singly linked list.  All other existing nodes of the
         *       singly linked list retain their ordering BEFORE the new last node.
         *       The counter is modified to reflect the addition of a new node to the singly linked list. */
        {
            if (this.Count() == 0)
            {
                this.addFirst(newItem);
                return;
            }
            Node newNode = new Node(newItem);
            Node cur = head;
            Node prev = head;
            while (cur != null)
            {
                prev = cur;
                cur = cur.next();
            }
            prev.setNext(newNode);
            counter++;
        }

        public Node removeFirst()
        /* pre:  Have at least one node in calling singly linked list object.
         * post: Return the node removed, which is the first node in the list.
         *       The counter is modified to reflect the removal of the first node from the singly linked list. */
        {
            Node cur = head;
            head = cur.next();
            cur.setNext(null);
            counter--;
            return cur;
        }

        public Node removeLast()
        /* pre:  Have at least one node in calling singly linked list object.
         * post: Return the node removed, which is the last node in the list.
         *       The counter is modified to reflect the removel of the last node from the singly linked list. */
        {
            Node cur = head;
            Node prev = head;
            while (cur.next() != null)
            {
                prev = cur;
                cur = cur.next();
            }
            if (prev == cur)
                head = null;
            else
                prev.setNext(null);
            counter--;
            return cur;
         }

        public void addBefore(Object newItem, Node link)
        /* pre:  Have object to be added to calling singly linked list object, and a link in the singly linked list BEFORE
         *       which the newItem's node must be added.
         * post: newItem is the element of the added node of the singly linked list.  All other existing nodes of the
         *       singly linked list retain their ordering relevant to the position of the newly added node.
         *       The counter is modified to reflect the addition of a new node to the singly linked list. */
        {
            if (link == null)  // list either empty or must be added at end of list
            {
                this.addLast(newItem);
                return;
            }
            Node newNode = new Node(newItem);
            Node cur = head;
             if (cur == link)  // must be added as first node
            {
                this.addFirst(newItem);
                return;
            }
            while (cur.next() != link)
                cur = cur.next();
            cur.setNext(newNode);
            newNode.setNext(link);
            counter++;
        }

        public void addAfter(Object newItem, Node link)
        /* pre:  Have object to be added to calling singly linked list object, and a link in the singly linked list AFTER
         *       which the newItem's node must be added.
         * post: newItem is the element of the added node of the singly linked list.  All other existing nodes of the
         *       singly linked list retain their ordering relevant to the position of the newly added node.
         *       The counter is modified to reflect the addition of a new node to the singly linked list. */
        {
            if (link == null)
            {
                this.addLast(newItem);
                return;
            }
            Node newNode = new Node(newItem);
            Node cur = head;
            Node prev = head;
            while (prev != link)
            {
                prev = cur;
                cur = cur.next();
            }
            if (prev == cur)
            {
                this.addFirst(newItem);
                return;
            }
            prev.setNext(newNode);
            newNode.setNext(cur);
            counter++;
        }
    }
}
