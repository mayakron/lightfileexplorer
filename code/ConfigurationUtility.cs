using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace LightFileExplorer
{
    internal static class ConfigurationUtility
    {
        public static readonly string BinaryViewer = ConfigurationManager.AppSettings["BinaryViewer"];

        public static readonly List<Tuple<string, string>> GotoFavorites = ConfigurationManager.AppSettings["GotoFavorites"]?.Split('|').Select(x => x.Split('>')).Select(x => new Tuple<string, string>(x[0], x[1])).ToList();

        public static readonly List<Tuple<string, string>> OpenWith = ConfigurationManager.AppSettings["OpenWith"]?.Split('|').Select(x => x.Split('>')).Select(x => new Tuple<string, string>(x[0], x[1])).ToList();

        public static readonly string TextViewer = ConfigurationManager.AppSettings["TextViewer"];
    }
}