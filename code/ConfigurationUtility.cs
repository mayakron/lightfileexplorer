using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace LightFileExplorer
{
    internal static class ConfigurationUtility
    {
        public static readonly string BinaryViewer;

        public static readonly List<Tuple<string, string>> CustomTools;

        public static readonly Dictionary<string, int> FileIcons;

        public static readonly List<Tuple<string, string>> GotoFavorites;

        public static readonly List<Tuple<string, string>> OpenWith;

        public static readonly string TextViewer;

        static ConfigurationUtility()
        {
            // By wrapping the reading of configuration elements in try-catch blocks, we accept that they may fail silently.

            try
            {
                FileIcons = ConfigurationManager.AppSettings["FileIcons"].Split('|').Select(x => x.Split('>')).ToDictionary(x => x[0], x => int.Parse(x[1]), StringComparer.OrdinalIgnoreCase);
            }
            catch
            {
                FileIcons = new Dictionary<string, int>();
            }

            try
            {
                OpenWith = ConfigurationManager.AppSettings["OpenWith"]?.Split('|').Select(x => x.Split('>')).Select(x => new Tuple<string, string>(x[0], x[1])).ToList();
            }
            catch
            {
            }

            try
            {
                TextViewer = ConfigurationManager.AppSettings["TextViewer"];
            }
            catch
            {
            }

            try
            {
                BinaryViewer = ConfigurationManager.AppSettings["BinaryViewer"];
            }
            catch
            {
            }

            try
            {
                GotoFavorites = ConfigurationManager.AppSettings["GotoFavorites"]?.Split('|').Select(x => x.Split('>')).Select(x => new Tuple<string, string>(x[0], x[1])).ToList();
            }
            catch
            {
            }

            try
            {
                CustomTools = ConfigurationManager.AppSettings["CustomTools"]?.Split('|').Select(x => x.Split('>')).Select(x => new Tuple<string, string>(x[0], x[1])).ToList();
            }
            catch
            {
            }
        }
    }
}