using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWpfProject.Helper.PathHelper
{
    public static class PathCheck
    {
        public static bool OpenOrClosed(in string? path) => File.Exists(path) ? true : false;
    }
}