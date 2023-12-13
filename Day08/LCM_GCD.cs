using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    public class LCM_GCD
    {
        // Method to calculate the GCD (Greatest Common Divisor)
        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Method to calculate the LCM (Least Common Multiple) of two numbers
        public static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }

        // Method to calculate the LCM of an array of numbers
        public static long LCMOfArray(long[] arr)
        {
            long result = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                result = LCM(result, arr[i]);
            }
            return result;
        }

    }
}
