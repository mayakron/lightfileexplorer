using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class ListViewEx : ListView
    {
        private const uint NM_CUSTOMDRAW = unchecked((uint)-12);

        public ListViewEx()
        {
            this.DoubleBuffered = true;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x204E)
            {
                var hdr = (NMHDR)m.GetLParam(typeof(NMHDR));

                if (hdr.code == NM_CUSTOMDRAW)
                {
                    m.Result = (IntPtr)0; // CDRF_DODEFAULT = 0. The control will draw itself. It will not send any additional NM_CUSTOMDRAW notification codes for this paint cycle.

                    return; // Does not call base.WndProc, so Windows Forms' own custom-draw handler never runs.
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