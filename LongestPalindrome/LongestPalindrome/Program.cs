using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestPalindrome
{
    public class Solution
    {
        public static void Main(String[] args)
        {
            Console.WriteLine(LongestPalindrome("a"));
            Console.Read();
        }
        public static Boolean isPal(String s)
        {
            
            int count = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[count] != s[i])
                {
                    return false;
                }
                count++;
            }
            return true;
        }

        public static string LongestPalindrome(string s)
        {
           
          //  int count = 0;
            //String result="";
            String final = "";
            if (s.Length==0)
            {
                return s;
            }
            for (int i = 0; i <s.Length; i++)
            {
                int left = i;
                int right = i;
                while (left > 0 && right < s.Length-1 && s[left-1] == s[right+1])
                {
                    left--;
                    right++;
                }
                if (right - left + 1 > final.Length)
                {
                    final = s.Substring(left, right - left + 1);
                }
                

              
            }
            for (int i = 0; i < s.Length-1; i++)
            {
                if (s[i] == s[i + 1])
                {
                    int left = i;
                    int right = i + 1;

                    while (left > 0 && right < s.Length - 1 && s[left-1] == s[right+1])
                    {
                        left--;
                        right++;
                    }
                    if (right - left + 1 > final.Length)
                    {
                        final = s.Substring(left, right - left + 1);
                    }
                }



            }



            return final;
        }
    }
}
