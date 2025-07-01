using System.Windows.Forms;

namespace LightFileExplorer
{
    internal static class MenuStripUtility
    {
        public static void DisableMenuItems(ToolStripItemCollection items)
        {
            foreach (var item in items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.Enabled = false;

                    if (menuItem.HasDropDownItems)
                    {
                        DisableMenuItems(menuItem.DropDownItems);
                    }
                }
            }
        }

        public static void EnableMenuItems(ToolStripItemCollection items)
        {
            foreach (var item in items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.Enabled = true;

                    if (menuItem.HasDropDownItems)
                    {
                        EnableMenuItems(menuItem.DropDownItems);
                    }
                }
            }
        }
    }
}