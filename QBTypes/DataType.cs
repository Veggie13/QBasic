using System;
using System.Collections.Generic;
using System.Linq;

namespace QBasic.Types
{
    public abstract class DataType
    {
        public static DataType GetMostSpecific(params DataType[] types)
        {
            return GetMostSpecific((IEnumerable<DataType>)types);
        }

        public static DataType GetMostSpecific(IEnumerable<DataType> types)
        {
            if (types.Any(dt => !Primitives.IsPrimitive(dt)))
            {
                throw new ArgumentException();
            }
            if (types.Contains(Primitives.String))
            {
                if (types.Any(t => t != Primitives.String))
                {
                    throw new NotImplementedException();
                }
                return Primitives.String;
            }
            if (types.Contains(Primitives.Double))
            {
                return Primitives.Double;
            }
            if (types.Contains(Primitives.Single))
            {
                return Primitives.Single;
            }
            if (types.Contains(Primitives.Long))
            {
                return Primitives.Long;
            }
            if (types.Contains(Primitives.Integer))
            {
                return Primitives.Integer;
            }

            throw new NotImplementedException();
        }

        public static DataType GetNextMostSpecific(DataType type)
        {
            if (!Primitives.IsPrimitive(type)
                || type == Primitives.String
                || type == Primitives.Double)
            {
                return null;
            }
            if (type == Primitives.Single)
            {
                return Primitives.Double;
            }
            if (type == Primitives.Long)
            {
                return Primitives.Single;
            }
            if (type == Primitives.Integer)
            {
                return Primitives.Long;
            }

            throw new NotImplementedException();
        }

        public abstract string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
