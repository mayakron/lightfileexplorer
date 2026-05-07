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

            if (a.ImageIndex == 0)
            {
                if (b.ImageIndex == 0)
                {
                    var keyComparison = string.Compare(a.SubItems[4].Text, b.SubItems[4].Text, StringComparison.OrdinalIgnoreCase);

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
                    var keyComparison = string.Compare(a.SubItems[4].Text, b.SubItems[4].Text, StringComparison.OrdinalIgnoreCase);

                    return (keyComparison == 0) ? string.Compare(a.Text, b.Text, StringComparison.OrdinalIgnoreCase) : keyComparison;
                }
            }
        }
    }
}