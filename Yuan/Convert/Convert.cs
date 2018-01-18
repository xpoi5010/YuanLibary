using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
namespace Yuan
{
    public static class convert
    {
        public static Double ByteUnit(BigInteger source,ComputetUnit sourceUnit,ComputetUnit resultUnit)
        {
            Double a = System.Convert.ToDouble((source * new BigInteger(sourceUnit.Multiple)).ToString()) / resultUnit.Multiple;
            return a;
        }
        public static Double ByteUnit(long source, ComputetUnit sourceUnit, ComputetUnit resultUnit)
        {
            return ByteUnit(new BigInteger(source), sourceUnit, resultUnit);
        }
        public static Double ByteUnit(int source, ComputetUnit sourceUnit, ComputetUnit resultUnit)
        {
            return ByteUnit(new BigInteger(source), sourceUnit, resultUnit);
        }
        public static Double ByteUnit(BigInteger source, ComputetUnit resultUnit)
        {
            return ByteUnit(source, ComputetUnit.Bytes.Byte, resultUnit);
        }
        public static Double ByteUnit(long source, ComputetUnit resultUnit)
        {
            return ByteUnit(new BigInteger(source), ComputetUnit.Bytes.Byte, resultUnit);
        }
        public static Double ByteUnit(int source, ComputetUnit resultUnit)
        {
            return ByteUnit(new BigInteger(source), ComputetUnit.Bytes.Byte, resultUnit);
        }
        public class ComputetUnit
        {
            private long multiple;
            private int type_;
            public ComputetUnit(long multiple, type Type)
            {
                this.multiple = multiple;
                type_ = (int)Type;
            }
            public enum type
            {
                Byte=0
            }
            public long Multiple
            {
                get
                {
                    return multiple;
                }
            }
            public static class Bytes
            {
                public static ComputetUnit Byte     { get { return new ComputetUnit((long)                      1, type.Byte); } }
                public static ComputetUnit Kilobyte { get { return new ComputetUnit((long)System.Math.Pow(10, 3 ), type.Byte); } }
                public static ComputetUnit Megabyte { get { return new ComputetUnit((long)System.Math.Pow(10, 6 ), type.Byte); } }
                public static ComputetUnit Gigabyte { get { return new ComputetUnit((long)System.Math.Pow(10, 9 ), type.Byte); } }
                public static ComputetUnit Terabyte { get { return new ComputetUnit((long)System.Math.Pow(10, 12), type.Byte); } }
                public static ComputetUnit Petabyte { get { return new ComputetUnit((long)System.Math.Pow(10, 15), type.Byte); } }
                public static ComputetUnit Exabyte  { get { return new ComputetUnit((long)System.Math.Pow(10, 18), type.Byte); } }
                public static ComputetUnit Zettabyte{ get { return new ComputetUnit((long)System.Math.Pow(10, 21), type.Byte); } }
                public static ComputetUnit Yottabyte{ get { return new ComputetUnit((long)System.Math.Pow(10, 24), type.Byte); } }
            }
        }
    }
}
