using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigZag
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine( Convert("AB", 1));
            Console.Read();
        }

        public static string Convert(string s, int numRows)
        {
            if (numRows == 1)
            {
                return s;
            }
            int i = 0;
            int rows = 0;
            Boolean line = true;
            int col = 0;
            Dictionary<String, String> result = new Dictionary<string, string>();
            //String[ , ] result = new string[ ,numRows];
            while (i < s.Length)
            {
                if (line == true)
                {
                    result[rows + " "+col + ""] = s[i]+"";
                    if (rows == numRows-1)
                    {
                        line = false;
                    }
                    else
                    {
                        rows = rows + 1;
                    }
                }else 
                if (line == false)
                {
                    rows = rows - 1;
                    col = col + 1;
                    result[rows + " "+col + ""] = s[i] + "";
                    if (rows == 0)
                    {
                        line = true;
                        rows = rows + 1;
                    }
                }
                i++;
            }
            String[ , ] test = new string[ numRows,col+1];
            foreach(String temp in result.Keys)
            {
                String[] A = temp.Split(' ');
                test[Int32.Parse(A[0]), Int32.Parse(A[1])] = result[temp];
            }
            String getReturn = "";
            foreach(String temp in test)
            {
                getReturn += temp;
            }
            return getReturn;
        }
    }
}
