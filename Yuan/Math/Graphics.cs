using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace Yuan.Math
{
    public static class Graphics
    {

        /// <summary>
        /// 表示一個精準Double座標。
        /// </summary>
        public class ExtraPoint
        {
            public double X { get; set; }
            public double Y { get; set; }

            /// <summary>
            /// 建立一個ExtraPoint個體
            /// </summary>
            /// <param name="x">表示該座標的x</param>
            /// <param name="y">表示該座標的y</param>
            public ExtraPoint(double x,double y)
            {
                X = x;Y = y;
            }

            /// <summary>
            /// 建立一個ExtraPoint個體
            /// </summary>
            /// <param name="source">來源Point，本建構函示會將來源的X與複製到此個體的X、Y</param>
            public ExtraPoint(Point source)
            {
                X = source.X;
                Y = source.Y;
                
            }
            public void Offset(double x,double y)
            {
                X += x;
                Y += y;
            }
        }

        /// <summary>
        /// 此靜態類別用於繪製貝茲曲線。
        /// </summary>
        public static class BezierCurve
        {
            //線性
            /// <summary>
            /// 該函數會計算一個線性貝茲取線的路徑，並以System.Drawing.Point陣列形式輸出。
            /// </summary>
            /// <param name="p0">表示用於計算的P0點</param>
            /// <param name="p1">表示用於計算的P1點</param>
            /// <param name="digit">表示t的小數點後的位數，t的小數點後的位數越高，所輸出的點座標精密度則越高，但輸出時間也會變大</param>
            /// <returns>以p0、p1、p2參數所計算出的貝茲曲線座標</returns>
            public static Point[] Linear(Point p0,Point p1,int digit)
            {
                double t = 0;
                string plus_str = ("1").PadLeft(digit,'0');
                double plus = Convert.ToDouble($@"0.{plus_str}");
                List<Point> output = new List<Point>();
                for (; t <= 1; t += plus)
                {
                    //x point
                    int x = Convert.ToInt32(System.Math.Round(Convert.ToDouble(p0.X + (p1.X - p0.X) )* t));
                    //x point
                    int y = Convert.ToInt32(System.Math.Round(Convert.ToDouble(p0.Y + (p1.Y - p0.Y)) * t));
                    output.Add(new Point(x, y));
                }
                return output.ToArray();
            }
            public static ExtraPoint[] Linear(ExtraPoint p0, ExtraPoint p1, int digit)
            {
                double t = 0;
                string plus_str = ("1").PadLeft(digit, '0');
                double plus = Convert.ToDouble($@"0.{plus_str}");
                List<ExtraPoint> output = new List<ExtraPoint>();
                for (; t <= 1; t += plus)
                {
                    //x point
                    double x = (Convert.ToDouble(p0.X + (p1.X - p0.X)) * t);
                    //x point
                    double y = (Convert.ToDouble(p0.Y + (p1.Y - p0.Y)) * t);
                    output.Add(new ExtraPoint(x, y));
                }
                return output.ToArray();
            }
            public static Point[] Quadratic(Point p0, Point p1, Point p2, int digit)
            {
                double t = 0;
                string plus_str = ("1").PadLeft(digit, '0');
                double plus = Convert.ToDouble($@"0.{plus_str}");
                List<Point> output = new List<Point>();
                for (; t <= 1; t += plus)
                {
                    //x point
                    int x = Convert.ToInt32(System.Math.Round(Convert.ToDouble((1-t)*((1-t)*p0.X+t*p1.X)+t*((1-t)*p1.X+t*p2.X))));
                    //x point
                    int y = Convert.ToInt32(System.Math.Round(Convert.ToDouble((1 - t) * ((1 - t) * p0.Y + t * p1.Y) + t * ((1 - t) * p1.Y + t * p2.Y))));
                    output.Add(new Point(x, y));
                }
                return output.ToArray();
            }
            public static ExtraPoint[] Quadratic(ExtraPoint p0, ExtraPoint p1, ExtraPoint p2, int digit)
            {
                double t = 0;
                string plus_str = ("1").PadLeft(digit, '0');
                double plus = Convert.ToDouble($@"0.{plus_str}");
                List<ExtraPoint> output = new List<ExtraPoint>();
                for (; t <= 1; t += plus)
                {
                    //x point
                    double x = (Convert.ToDouble(System.Math.Pow((1 - t), 2) * p0.X + 2 * t * (1 - t) * p1.X + System.Math.Pow(t, 2) * p2.X));
                    //x point
                    double y = (Convert.ToDouble(System.Math.Pow((1 - t), 2) * p0.Y + 2 * t * (1 - t) * p1.Y + System.Math.Pow(t, 2) * p2.Y));
                    output.Add(new ExtraPoint(x, y));
                }
                return output.ToArray();
            }
        }
    }
}
