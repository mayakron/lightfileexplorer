using System;
using System.Collections;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal class FileViewColumnAttributesSorter : IComparer, IHasName
    {
        public string Name
        {
            get
            {
                return "Attributes";
            }
        }

        public int Compare(object x, object y)
        {
            var a = (ListViewItem)x;
            var b = (ListViewItem)y;

            return string.Compare(a.SubItems[4].Text, b.SubItems[4].Text, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}