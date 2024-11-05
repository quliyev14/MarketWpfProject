using System.IO;

namespace MarketWpfProject.Helper.PathHelper
{
    public static class PathCheck
    {
        public static bool OpenOrClosed(in string? path) => File.Exists(path) ? true : false;
    }
}