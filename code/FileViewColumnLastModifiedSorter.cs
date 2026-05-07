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

            if (a.ImageIndex == 0)
            {
                if (b.ImageIndex == 0)
                {
                    var keyComparison = DateTime.Compare((DateTime)a.SubItems[3].Tag, (DateTime)b.SubItems[3].Tag);

                    return (keyComparison == 0) ? string.Compare(a.Text, b.Text, StringComparison.OrdinalIgnoreCase) : keyComparison;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (b.ImageIndex == 0)
                {
                    return 1;
                }
                else
                {
                    var keyComparison = DateTime.Compare((DateTime)a.SubItems[3].Tag, (DateTime)b.SubItems[3].Tag);

                    return (keyComparison == 0) ? string.Compare(a.Text, b.Text, StringComparison.OrdinalIgnoreCase) : keyComparison;
                }
            }
        }
    }
}