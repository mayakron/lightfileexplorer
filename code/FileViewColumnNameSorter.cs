using System;
using System.Collections;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal class FileViewColumnNameSorter : IComparer, IHasName
    {
        public string Name
        {
            get
            {
                return "Name";
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
                    return string.Compare(a.Text, b.Text, StringComparison.OrdinalIgnoreCase);
                }
            }
        }
    }
}