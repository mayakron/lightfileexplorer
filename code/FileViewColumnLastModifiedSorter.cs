using System;
using System.Collections;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal class FileViewColumnLastModifiedSorter : IComparer, IHasName
    {
        public string Name
        {
            get
            {
                return "Last Modified";
            }
        }

        public int Compare(object x, object y)
        {
            var a = (ListViewItem)x;
            var b = (ListViewItem)y;

            return string.Compare(a.SubItems[3].Text, b.SubItems[3].Text, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}