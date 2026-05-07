using System;
using System.Collections;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal class FileViewColumnSizeSorter : IComparer, IHasName
    {
        public string Name
        {
            get
            {
                return "Size";
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
                    var xSize = (ulong)a.SubItems[2].Tag;
                    var ySize = (ulong)b.SubItems[2].Tag;

                    return (xSize > ySize) ? 1 : (ySize > xSize) ? -1 : string.Compare(a.Text, b.Text, StringComparison.OrdinalIgnoreCase);
                }
            }
        }
    }
}