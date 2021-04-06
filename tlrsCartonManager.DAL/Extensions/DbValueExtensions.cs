using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Extensions
{
    public static class DbValueExtensions
    {
        public static T As<T>(this object source)
        {
            return source == null || source == DBNull.Value
                ? default(T)
                : (T)source;
        }

        // Used to convert values going to the db
        public static object AsDbValue(this object source)
        {
            return source ?? DBNull.Value;
        }
    }
}
