using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = { 1, 1};
            Console.Write(MaxArea(A));
            Console.Read();

        }

        public static int MaxArea(int[] height)
        {
            int count = 0;
            int result = 0;
            int left = 0;
            int right = height.Length - 1;
            while (right - left >= 1)
            {
                if (height[left] > height[right])
                {
                    count = (right - left) * height[right];
                }
                else
                {
                    count = (right - left) * height[left];
                }
                if (count > result)
                {
                    result = count;
                }
                if (height[left] > height[right])
                {
                    right--;
                }
                else
                {
                    left++;
                }
                count = 0;
            }

            return result;
           
        }
    }
}
