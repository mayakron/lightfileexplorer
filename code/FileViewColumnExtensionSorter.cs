using System;
using System.Collections;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal class FileViewColumnExtensionSorter : IComparer, IHasName
    {
        public string Name
        {
            get
            {
                return "Extension";
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
                    return string.Compare(a.Text, b.Text, StringComparison.OrdinalIgnoreCase);
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
                    var keyComparison = string.Compare(a.SubItems[1].Text, b.SubItems[1].Text, StringComparison.OrdinalIgnoreCase);

                    return (keyComparison == 0) ? string.Compare(a.Text, b.Text, StringComparison.OrdinalIgnoreCase) : keyComparison;
                }
            }
        }
    }
}