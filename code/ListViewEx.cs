using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    // To get maximum performance:
    //
    // 1) Double buffering is enabled.
    // 2) Custom draw processing is disabled.
    // 3) Item lookups, removals and renames by key go through a dictionary instead of ListView's native linear key scan.

    public class ListViewEx : ListView
    {
        private const int CDRF_DODEFAULT = 0;

        private const uint NM_CUSTOMDRAW = unchecked((uint)-12);

        private readonly Dictionary<string, ListViewItem> ItemsByKey = new Dictionary<string, ListViewItem>(StringComparer.OrdinalIgnoreCase);

        public ListViewEx()
        {
            this.DoubleBuffered = true;
        }

        public ListViewItem AddItem(ListViewItem item)
        {
            this.Items.Add(item);

            this.ItemsByKey[item.Name] = item;

            return item;
        }

        public void ClearItems()
        {
            this.Items.Clear();

            this.ItemsByKey.Clear();
        }

        public ListViewItem FindItemByKey(string key)
        {
            return this.ItemsByKey.TryGetValue(key, out var item) ? item : null;
        }

        public bool RemoveItemByKey(string key)
        {
            if (this.ItemsByKey.TryGetValue(key, out var item))
            {
                this.Items.Remove(item);

                this.ItemsByKey.Remove(key);

                return true;
            }

            return false;
        }

        public void RenameItemKey(string previousKey, ListViewItem item)
        {
            this.ItemsByKey.Remove(previousKey);

            this.ItemsByKey[item.Name] = item;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x204E)
            {
                var hdr = (NMHDR)m.GetLParam(typeof(NMHDR));

                if (hdr.code == NM_CUSTOMDRAW)
                {
                    m.Result = (IntPtr)CDRF_DODEFAULT;

                    return; // Does not call base.WndProc, so the Windows Forms draw handler never runs.
                }
            }

            base.WndProc(ref m);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NMHDR
        {
            public IntPtr hwndFrom;

            public IntPtr idFrom;

            public uint code;
        }
    }
}