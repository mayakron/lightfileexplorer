using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace LightFileExplorer
{
    internal static class ConfigurationUtility
    {
        public static readonly string BinaryViewer;

        public static readonly List<Tuple<string, string, string>> CustomTools;

        public static readonly Dictionary<string, int> FileIcons;

        public static readonly int FileSystemWatcherTimerInterval;

        public static readonly List<Tuple<string, string, string>> GotoFavorites;

        public static readonly List<Tuple<string, string, string>> OpenWith;

        public static readonly int ProgressWindowWaitTime;

        public static readonly string TextViewer;

        static ConfigurationUtility()
        {
            // By wrapping the reading of configuration elements in try-catch blocks, we accept that they may fail silently.

            try
            {
                FileSystemWatcherTimerInterval = int.Parse(ConfigurationManager.AppSettings["FileSystemWatcherTimerInterval"]);
            }
            catch
            {
                FileSystemWatcherTimerInterval = 500;
            }

            try
            {
                ProgressWindowWaitTime = int.Parse(ConfigurationManager.AppSettings["ProgressWindowWaitTime"]);
            }
            catch
            {
                ProgressWindowWaitTime = 500;
            }

            try
            {
                FileIcons = ConfigurationManager.AppSettings["FileIcons"].Split('|').Select(x => x.Split('>')).ToDictionary(x => x[0].ToUpperInvariant(), x => int.Parse(x[1]), StringComparer.OrdinalIgnoreCase);
            }
            catch
            {
                FileIcons = new Dictionary<string, int>();
            }

            try
            {
                OpenWith = ConfigurationManager.AppSettings["OpenWith"]?.Split('|').Select(x => x.Split('>')).Select(x => new Tuple<string, string, string>(x[0], x[1], (x.Length > 2) ? x[2] : null)).ToList();
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
                GotoFavorites = ConfigurationManager.AppSettings["GotoFavorites"]?.Split('|').Select(x => x.Split('>')).Select(x => new Tuple<string, string, string>(x[0], x[1], (x.Length > 2) ? x[2] : null)).ToList();
            }
            catch
            {
            }

            try
            {
                CustomTools = ConfigurationManager.AppSettings["CustomTools"]?.Split('|').Select(x => x.Split('>')).Select(x => new Tuple<string, string, string>(x[0], x[1], (x.Length > 2) ? x[2] : null)).ToList();
            }
            catch
            {
            }
        }
    }
}