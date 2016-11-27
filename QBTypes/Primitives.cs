using System;
using System.Linq;

namespace QBasic.Types
{
    public class IntegerType : DataType
    {
        public override string Name { get { return "INTEGER"; } }
    }

    public class LongType : DataType
    {
        public override string Name { get { return "LONG"; } }
    }

    public class SingleType : DataType
    {
        public override string Name { get { return "SINGLE"; } }
    }

    public class DoubleType : DataType
    {
        public override string Name { get { return "DOUBLE"; } }
    }

    public class StringType : DataType
    {
        public override string Name { get { return "STRING"; } }
    }

    public static class Primitives
    {
        private static DataType[] All;
        static Primitives()
        {
            Integer = new IntegerType();
            Long = new LongType();
            Single = new SingleType();
            Double = new DoubleType();
            String = new StringType();
            All = new[] { Integer, Long, Single, Double, String };
        }

        public static DataType Integer { get; private set; }
        public static DataType Long { get; private set; }
        public static DataType Single { get; private set; }
        public static DataType Double { get; private set; }
        public static DataType String { get; private set; }

        public static bool IsPrimitive(DataType type)
        {
            return All.Contains(type);
        }
    }
}
