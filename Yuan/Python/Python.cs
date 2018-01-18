using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuan
{
    //這是Python函式庫
    public static class Python
    {

        public static int[] range(int stop)
        {
            return range(0,stop);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static int[] range(int start,int stop)
        {
            return range(start,stop,1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static int[] range(int start, int stop, int step)
        {
            List<int> output = new List<int>();
            for (int nowint = start; nowint < stop; nowint += step)
            {
                output.Add(nowint);
            }
            return output.ToArray();
        }
        public static void print(string text)
        {
            Console.WriteLine(text);
        }
        public static class os
        {
            //參數
            const int F_OK = 0;
            const int R_OK = 1;
            const int W_OK = 2;
            const int X_OK = 3;
            public static bool access(string path,int mode)
            {
                return false;
            }
        }
    }
}
