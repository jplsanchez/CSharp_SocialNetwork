using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Repository
{
    public static class Utils
    {
        public static string GetModelShortName(string className)
        {
            if (className.StartsWith("Model"))
            {
                return className[5..];
            }
            if (className.EndsWith("Model"))
            {
                return className[..^5];
            }
            return className;
        }
    }
}
