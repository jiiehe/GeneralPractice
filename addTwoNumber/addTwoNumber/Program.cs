using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addTwoNumber
{

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val)
        {
            this.val = val;
        }
     
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            ListNode l1 = new ListNode(5);
         //   l1.next = new ListNode(8);
          //  l1.next.next = new ListNode(3);
            ListNode l2 = new ListNode(5);
            //l2.next = new ListNode(6);
            //l2.next.next = new ListNode(4);
            AddTwoNumbers(l1, l2);


        }
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            int carry = 0;
            ListNode result = new ListNode(0); 
            ListNode current = result;
           
            while(l1!=null || l2 != null)
            {
               
                if(l1!=null && l2 == null)
                {
                    carry = carry + l1.val;
                    
                }
                if(l1==null && l2 != null)
                {
                    carry = carry + l2.val;
                }
                if(l1!=null && l2!= null)
                {
                    carry = carry + l1.val + l2.val;
                }
                int temp=carry % 10;
                carry = carry / 10;
                if (carry != 0 || temp != 0)
                {
                    current.val=temp;

                }
                if (l1 != null)
                {
                    l1 = l1.next;
                }
                if (l2 != null)
                {
                    l2 = l2.next;
                }
                if (l1!= null || l2 != null)
                {
                     current.next=new ListNode(0);
                    current = current.next;
                }
            }
            if (carry != 0)
            {
                current.next = new ListNode(carry);
                current = current.next;
            }

            return result;
        }
    }
}
