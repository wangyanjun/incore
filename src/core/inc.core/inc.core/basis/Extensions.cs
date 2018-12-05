using System;
using System.Collections;
using System.Collections.Generic;

namespace inc.core
{
    public static class Extensions
    {
        public static void TraceException(this Exception exception, string context = "")
        {
            //Logger.Default.Error(context, exception);
        }

        internal static void TraceException(string exception, string context = "")
        {
            //Logger.Default.Error(context, exception);
        }

        public static T[] ToArray<T>(this object values) where T : IConvertible
        {
            var result = values as T[];
            if (result != null)
            {
                if (values is IEnumerable ie)
                {
                    var list = new List<T>();
                    var hasError = false;
                    foreach (var e in ie)
                    {
                        if (e is IConvertible conv)
                        {
                            list.Add((T)conv.ToType(typeof(T), null));
                        }
                        else
                        {
                            list.Clear();
                            hasError = true;
                            break;
                        }
                    }

                    result = hasError ? default(T[]) : list.ToArray();
                }
            }

            return result;
        }
    }
}

