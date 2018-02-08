using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace Compiler.Preprocessor
{
    public static class Utilities
    {
        public static List<string> GenerateList(string source)
        {
            return source.Split(new string[] { "\r\n", "\n" },
                StringSplitOptions.None).ToList();
        }
    }
}
