using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal static class FileViewUtility
    {
        private static readonly NumberFormatInfo SizeColumnNumberFormatInfo = new NumberFormatInfo { CurrencyGroupSeparator = " ", NumberGroupSeparator = " ", PercentGroupSeparator = " " };

        public static ListViewItem BuildFile(string name, ulong size, DateTime lastWriteTime, FileAttributes attributes)
        {
            return new ListViewItem
            (
                new[]
                {
                    new ListViewItem.ListViewSubItem { Text = name, Tag = null },
                    new ListViewItem.ListViewSubItem { Text = Path.GetExtension(name).ToUpperInvariant(), Tag = null },
                    new ListViewItem.ListViewSubItem { Text = size.ToString("N0", SizeColumnNumberFormatInfo), Tag = size },
                    new ListViewItem.ListViewSubItem { Text = lastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"), Tag = lastWriteTime },
                    new ListViewItem.ListViewSubItem { Text = FileAttributesUtility.ToString(attributes), Tag = attributes }
                },
                GetFileIconIndex(name)
            )
            {
                Name = name
            };
        }

        public static ListViewItem BuildFolder(string name, DateTime lastWriteTime, FileAttributes attributes)
        {
            return new ListViewItem
            (
                new[]
                {
                    new ListViewItem.ListViewSubItem { Text = name, Tag = null },
                    new ListViewItem.ListViewSubItem { Text = null, Tag = null },
                    new ListViewItem.ListViewSubItem { Text = null, Tag = null },
                    new ListViewItem.ListViewSubItem { Text = lastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"), Tag = lastWriteTime },
                    new ListViewItem.ListViewSubItem { Text = FileAttributesUtility.ToString(attributes), Tag = attributes }
                },
                0
            )
            {
                Name = name
            };
        }

        public static bool FindNext(ListView listView, Regex nameRegex)
        {
            var startIndex = (listView.FocusedItem != null) ? (listView.FocusedItem.Index + 1) : 0;

            for (int i = startIndex; i < listView.Items.Count; i++)
            {
                var viewItem = listView.Items[i];

                if (nameRegex.IsMatch(viewItem.Name))
                {
                    listView.BeginUpdate();

                    try
                    {
                        listView.SelectedItems.Clear();

                        viewItem.Selected = true;
                        viewItem.Focused = true;

                        viewItem.EnsureVisible();
                    }
                    finally
                    {
                        listView.EndUpdate();
                    }

                    return true;
                }
            }

            return false;
        }

        public static bool FindPrevious(ListView listView, Regex nameRegex)
        {
            var startIndex = (listView.FocusedItem != null) ? (listView.FocusedItem.Index - 1) : (listView.Items.Count - 1);

            for (int i = startIndex; i >= 0; i--)
            {
                var viewItem = listView.Items[i];

                if (nameRegex.IsMatch(viewItem.Name))
                {
                    listView.BeginUpdate();

                    try
                    {
                        listView.SelectedItems.Clear();

                        viewItem.Selected = true;
                        viewItem.Focused = true;

                        viewItem.EnsureVisible();
                    }
                    finally
                    {
                        listView.EndUpdate();
                    }

                    return true;
                }
            }

            return false;
        }

        public static bool IsDirectory(ListViewItem viewItem)
        {
            return viewItem.ImageIndex == 0;
        }

        public static void MoveTo(ListViewItem viewItem)
        {
            viewItem.Selected = true;
            viewItem.Focused = true;

            viewItem.EnsureVisible();
        }

        public static void Rename(ListViewItem viewItem, string name)
        {
            viewItem.Name = name;
            viewItem.Text = name;

            if (!IsDirectory(viewItem))
            {
                viewItem.SubItems[1].Text = Path.GetExtension(name).ToUpperInvariant();
            }
        }

        public static void SetFile(ListViewItem viewItem, string name, ulong size, DateTime lastWriteTime, FileAttributes attributes)
        {
            viewItem.ImageIndex = GetFileIconIndex(name);

            viewItem.Text = name;

            viewItem.SubItems[1].Text = Path.GetExtension(name).ToUpperInvariant();
            viewItem.SubItems[1].Tag = null;

            viewItem.SubItems[2].Text = size.ToString("N0", SizeColumnNumberFormatInfo);
            viewItem.SubItems[2].Tag = size;

            viewItem.SubItems[3].Text = lastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            viewItem.SubItems[3].Tag = lastWriteTime;

            viewItem.SubItems[4].Text = FileAttributesUtility.ToString(attributes);
            viewItem.SubItems[4].Tag = attributes;
        }

        public static void SetFolder(ListViewItem viewItem, string name, DateTime lastWriteTime, FileAttributes attributes)
        {
            viewItem.ImageIndex = 0;

            viewItem.Text = name;

            viewItem.SubItems[1].Text = null;
            viewItem.SubItems[1].Tag = null;

            viewItem.SubItems[2].Text = null;
            viewItem.SubItems[2].Tag = null;

            viewItem.SubItems[3].Text = lastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            viewItem.SubItems[3].Tag = lastWriteTime;

            viewItem.SubItems[4].Text = FileAttributesUtility.ToString(attributes);
            viewItem.SubItems[4].Tag = attributes;
        }

        private static int GetFileIconIndex(string name)
        {
            return ConfigurationUtility.FileIcons.TryGetValue(Path.GetExtension(name), out var index) ? index : 1;
        }
    }
}