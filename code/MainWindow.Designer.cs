namespace LightFileExplorer
{
    internal partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip MenuStrip;

        private System.Windows.Forms.ToolStripMenuItem FilesNewShortcutToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesAdvancedSelectionToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FileCopyPathToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesCopyToToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesMoveToToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesFindToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesFindNextToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesFindPreviousToolStripMenuItem;

        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItemSeparator9;

        private System.Windows.Forms.ToolStripMenuItem GotoLogicalDriveToolStripMenuItem;

        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItemSeparator6;

        private System.Windows.Forms.ToolStripMenuItem FileCopyNameToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesCopyToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesCutToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesDeleteToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesViewAsBinaryToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesExitToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesInvertSelectionToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesNewFolderToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesOpenToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesPasteToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesRefreshToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesRenameToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesSelectAllToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem FilesViewAsTextToolStripMenuItem;

        private System.Windows.Forms.ColumnHeader FileViewColumnHeaderAttributes;

        private System.Windows.Forms.ColumnHeader FileViewColumnHeaderExtension;

        private System.Windows.Forms.ColumnHeader FileViewColumnHeaderDateModified;

        private System.Windows.Forms.ColumnHeader FileViewColumnHeaderName;

        private System.Windows.Forms.ColumnHeader FileViewColumnHeaderSize;

        private System.Windows.Forms.ToolStripMenuItem GotoCustomFolderToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem GotoDownloadsFolderToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem GotoParentFolderToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem GotoToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem GotoUserFolderToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem HelpProjectPageToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem HelpAboutToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;

        private System.Windows.Forms.ToolStripStatusLabel StatusStripMessageLabel;

        private System.Windows.Forms.ToolStripMenuItem ToolsCommandPromptHereToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem ToolsFileExplorerHereToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem ToolsLightFileExplorerHereToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem ToolsPowerShellConsoleHereToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;

        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItemSeparator1;

        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItemSeparator2;

        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItemSeparator3;

        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItemSeparator5;

        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItemSeparator8;

        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItemSeparator10;

        private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem ViewSortToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem ViewSortNameToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem ViewSortExtensionToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem ViewSortSizeToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem ViewSortDateModifiedToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem ViewSortAttributesToolStripMenuItem;

        private System.Windows.Forms.ListViewEx FileView;

        private System.Windows.Forms.ImageList FileIcons;

        private System.Windows.Forms.StatusStrip StatusStrip;

        private System.Windows.Forms.Timer Timer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.FileIcons = new System.Windows.Forms.ImageList(this.components);
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesNewFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesNewShortcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesRenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesViewAsTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesViewAsBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesRefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesCopyToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesMoveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FilesCutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileCopyNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileCopyPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesPasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.FilesSelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesInvertSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesAdvancedSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FilesFindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesFindNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilesFindPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.FilesExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GotoLogicalDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.GotoParentFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GotoCustomFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.GotoUserFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GotoDownloadsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsLightFileExplorerHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolsFileExplorerHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsCommandPromptHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsPowerShellConsoleHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSortNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSortExtensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSortSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSortDateModifiedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSortAttributesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpProjectPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.HelpAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusStripMessageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.FileView = new System.Windows.Forms.ListViewEx();
            this.FileViewColumnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileViewColumnHeaderExtension = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileViewColumnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileViewColumnHeaderDateModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileViewColumnHeaderAttributes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MenuStrip.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            //
            // FileIcons
            //
            this.FileIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FileIcons.ImageStream")));
            this.FileIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.FileIcons.Images.SetKeyName(0, "0000Directory.png");
            this.FileIcons.Images.SetKeyName(1, "0001File.png");
            this.FileIcons.Images.SetKeyName(2, "0002FileArchive.png");
            this.FileIcons.Images.SetKeyName(3, "0003FileLibrary.png");
            this.FileIcons.Images.SetKeyName(4, "0004FileProgram.png");
            this.FileIcons.Images.SetKeyName(5, "0005FileAudio.png");
            this.FileIcons.Images.SetKeyName(6, "0006FileImage.png");
            this.FileIcons.Images.SetKeyName(7, "0007FileVideo.png");
            this.FileIcons.Images.SetKeyName(8, "0008FileText.png");
            this.FileIcons.Images.SetKeyName(9, "0009FileIllustration.png");
            this.FileIcons.Images.SetKeyName(10, "0010FileSpreadsheet.png");
            this.FileIcons.Images.SetKeyName(11, "0011FilePresentation.png");
            this.FileIcons.Images.SetKeyName(12, "0012FileWeb.png");
            this.FileIcons.Images.SetKeyName(13, "0013FileCode.png");
            this.FileIcons.Images.SetKeyName(14, "0014FileBook.png");
            this.FileIcons.Images.SetKeyName(15, "0015FilePdf.png");
            //
            // MenuStrip
            //
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FilesToolStripMenuItem,
            this.GotoToolStripMenuItem,
            this.ToolsToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MenuStrip.Size = new System.Drawing.Size(1104, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
            //
            // FilesToolStripMenuItem
            //
            this.FilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FilesNewFolderToolStripMenuItem,
            this.FilesNewShortcutToolStripMenuItem,
            this.FilesOpenToolStripMenuItem,
            this.FilesRenameToolStripMenuItem,
            this.FilesViewAsTextToolStripMenuItem,
            this.FilesViewAsBinaryToolStripMenuItem,
            this.FilesRefreshToolStripMenuItem,
            this.FilesCopyToToolStripMenuItem,
            this.FilesMoveToToolStripMenuItem,
            this.ToolStripMenuItemSeparator1,
            this.FilesCutToolStripMenuItem,
            this.FilesCopyToolStripMenuItem,
            this.FileCopyNameToolStripMenuItem,
            this.FileCopyPathToolStripMenuItem,
            this.FilesPasteToolStripMenuItem,
            this.FilesDeleteToolStripMenuItem,
            this.ToolStripMenuItemSeparator8,
            this.FilesSelectAllToolStripMenuItem,
            this.FilesInvertSelectionToolStripMenuItem,
            this.FilesAdvancedSelectionToolStripMenuItem,
            this.ToolStripMenuItemSeparator2,
            this.FilesFindToolStripMenuItem,
            this.FilesFindNextToolStripMenuItem,
            this.FilesFindPreviousToolStripMenuItem,
            this.ToolStripMenuItemSeparator9,
            this.FilesExitToolStripMenuItem});
            this.FilesToolStripMenuItem.Name = "FilesToolStripMenuItem";
            this.FilesToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.FilesToolStripMenuItem.Text = "&Files";
            //
            // FilesNewFolderToolStripMenuItem
            //
            this.FilesNewFolderToolStripMenuItem.Name = "FilesNewFolderToolStripMenuItem";
            this.FilesNewFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.N)));
            this.FilesNewFolderToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesNewFolderToolStripMenuItem.Text = "New &Folder";
            this.FilesNewFolderToolStripMenuItem.Click += new System.EventHandler(this.FilesNewFolderToolStripMenuItemClick);
            //
            // FilesNewShortcutToolStripMenuItem
            //
            this.FilesNewShortcutToolStripMenuItem.Name = "FilesNewShortcutToolStripMenuItem";
            this.FilesNewShortcutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.Q)));
            this.FilesNewShortcutToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesNewShortcutToolStripMenuItem.Text = "New &Shortcut";
            this.FilesNewShortcutToolStripMenuItem.Click += new System.EventHandler(this.FilesNewShortcutToolStripMenuItemClick);
            //
            // FilesOpenToolStripMenuItem
            //
            this.FilesOpenToolStripMenuItem.Name = "FilesOpenToolStripMenuItem";
            this.FilesOpenToolStripMenuItem.ShortcutKeyDisplayString = "Enter, Right";
            this.FilesOpenToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesOpenToolStripMenuItem.Text = "Open";
            this.FilesOpenToolStripMenuItem.Click += new System.EventHandler(this.FilesOpenToolStripMenuItemClick);
            //
            // FilesRenameToolStripMenuItem
            //
            this.FilesRenameToolStripMenuItem.Name = "FilesRenameToolStripMenuItem";
            this.FilesRenameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.FilesRenameToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesRenameToolStripMenuItem.Text = "Rename";
            this.FilesRenameToolStripMenuItem.Click += new System.EventHandler(this.FilesRenameToolStripMenuItemClick);
            //
            // FilesViewAsTextToolStripMenuItem
            //
            this.FilesViewAsTextToolStripMenuItem.Name = "FilesViewAsTextToolStripMenuItem";
            this.FilesViewAsTextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.FilesViewAsTextToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesViewAsTextToolStripMenuItem.Text = "View As Text";
            this.FilesViewAsTextToolStripMenuItem.Click += new System.EventHandler(this.FilesViewAsTextToolStripMenuItemClick);
            //
            // FilesViewAsBinaryToolStripMenuItem
            //
            this.FilesViewAsBinaryToolStripMenuItem.Name = "FilesViewAsBinaryToolStripMenuItem";
            this.FilesViewAsBinaryToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.FilesViewAsBinaryToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesViewAsBinaryToolStripMenuItem.Text = "View As Binary";
            this.FilesViewAsBinaryToolStripMenuItem.Click += new System.EventHandler(this.FilesViewAsBinaryToolStripMenuItemClick);
            //
            // FilesRefreshToolStripMenuItem
            //
            this.FilesRefreshToolStripMenuItem.Name = "FilesRefreshToolStripMenuItem";
            this.FilesRefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.FilesRefreshToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesRefreshToolStripMenuItem.Text = "Refresh";
            this.FilesRefreshToolStripMenuItem.Click += new System.EventHandler(this.FilesRefreshToolStripMenuItemClick);
            //
            // FilesCopyToToolStripMenuItem
            //
            this.FilesCopyToToolStripMenuItem.Name = "FilesCopyToToolStripMenuItem";
            this.FilesCopyToToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.FilesCopyToToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesCopyToToolStripMenuItem.Text = "Copy To";
            this.FilesCopyToToolStripMenuItem.Click += new System.EventHandler(this.FilesCopyToToolStripMenuItemClick);
            //
            // FilesMoveToToolStripMenuItem
            //
            this.FilesMoveToToolStripMenuItem.Name = "FilesMoveToToolStripMenuItem";
            this.FilesMoveToToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.FilesMoveToToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesMoveToToolStripMenuItem.Text = "Move To";
            this.FilesMoveToToolStripMenuItem.Click += new System.EventHandler(this.FilesMoveToToolStripMenuItemClick);
            //
            // ToolStripMenuItemSeparator1
            //
            this.ToolStripMenuItemSeparator1.Name = "ToolStripMenuItemSeparator1";
            this.ToolStripMenuItemSeparator1.Size = new System.Drawing.Size(218, 6);
            //
            // FilesCutToolStripMenuItem
            //
            this.FilesCutToolStripMenuItem.Name = "FilesCutToolStripMenuItem";
            this.FilesCutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.FilesCutToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesCutToolStripMenuItem.Text = "Cut";
            this.FilesCutToolStripMenuItem.Click += new System.EventHandler(this.FilesCutToolStripMenuItemClick);
            //
            // FilesCopyToolStripMenuItem
            //
            this.FilesCopyToolStripMenuItem.Name = "FilesCopyToolStripMenuItem";
            this.FilesCopyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.FilesCopyToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesCopyToolStripMenuItem.Text = "Copy";
            this.FilesCopyToolStripMenuItem.Click += new System.EventHandler(this.FilesCopyToolStripMenuItemClick);
            //
            // FileCopyNameToolStripMenuItem
            //
            this.FileCopyNameToolStripMenuItem.Name = "FileCopyNameToolStripMenuItem";
            this.FileCopyNameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.FileCopyNameToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FileCopyNameToolStripMenuItem.Text = "Copy Name";
            this.FileCopyNameToolStripMenuItem.Click += new System.EventHandler(this.FilesCopyNameToolStripMenuItemClick);
            //
            // FileCopyPathToolStripMenuItem
            //
            this.FileCopyPathToolStripMenuItem.Name = "FileCopyPathToolStripMenuItem";
            this.FileCopyPathToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.T)));
            this.FileCopyPathToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FileCopyPathToolStripMenuItem.Text = "Copy Path";
            this.FileCopyPathToolStripMenuItem.Click += new System.EventHandler(this.FilesCopyPathToolStripMenuItemClick);
            //
            // FilesPasteToolStripMenuItem
            //
            this.FilesPasteToolStripMenuItem.Name = "FilesPasteToolStripMenuItem";
            this.FilesPasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.FilesPasteToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesPasteToolStripMenuItem.Text = "Paste";
            this.FilesPasteToolStripMenuItem.Click += new System.EventHandler(this.FilesPasteToolStripMenuItemClick);
            //
            // FilesDeleteToolStripMenuItem
            //
            this.FilesDeleteToolStripMenuItem.Name = "FilesDeleteToolStripMenuItem";
            this.FilesDeleteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.FilesDeleteToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesDeleteToolStripMenuItem.Text = "Delete";
            this.FilesDeleteToolStripMenuItem.Click += new System.EventHandler(this.FilesDeleteToolStripMenuItemClick);
            //
            // ToolStripMenuItemSeparator8
            //
            this.ToolStripMenuItemSeparator8.Name = "ToolStripMenuItemSeparator8";
            this.ToolStripMenuItemSeparator8.Size = new System.Drawing.Size(218, 6);
            //
            // FilesSelectAllToolStripMenuItem
            //
            this.FilesSelectAllToolStripMenuItem.Name = "FilesSelectAllToolStripMenuItem";
            this.FilesSelectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.FilesSelectAllToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesSelectAllToolStripMenuItem.Text = "Select All";
            this.FilesSelectAllToolStripMenuItem.Click += new System.EventHandler(this.FilesSelectAllToolStripMenuItemClick);
            //
            // FilesInvertSelectionToolStripMenuItem
            //
            this.FilesInvertSelectionToolStripMenuItem.Name = "FilesInvertSelectionToolStripMenuItem";
            this.FilesInvertSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.FilesInvertSelectionToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesInvertSelectionToolStripMenuItem.Text = "Invert Selection";
            this.FilesInvertSelectionToolStripMenuItem.Click += new System.EventHandler(this.FilesInvertSelectionToolStripMenuItemClick);
            //
            // FilesAdvancedSelectionToolStripMenuItem
            //
            this.FilesAdvancedSelectionToolStripMenuItem.Name = "FilesAdvancedSelectionToolStripMenuItem";
            this.FilesAdvancedSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.FilesAdvancedSelectionToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesAdvancedSelectionToolStripMenuItem.Text = "Advanced Selection";
            this.FilesAdvancedSelectionToolStripMenuItem.Click += new System.EventHandler(this.FilesAdvancedSelectionToolStripMenuItemClick);
            //
            // ToolStripMenuItemSeparator2
            //
            this.ToolStripMenuItemSeparator2.Name = "ToolStripMenuItemSeparator2";
            this.ToolStripMenuItemSeparator2.Size = new System.Drawing.Size(218, 6);
            //
            // FilesFindToolStripMenuItem
            //
            this.FilesFindToolStripMenuItem.Name = "FilesFindToolStripMenuItem";
            this.FilesFindToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.FilesFindToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesFindToolStripMenuItem.Text = "Find";
            this.FilesFindToolStripMenuItem.Click += new System.EventHandler(this.FilesFindToolStripMenuItemClick);
            //
            // FilesFindNextToolStripMenuItem
            //
            this.FilesFindNextToolStripMenuItem.Enabled = false;
            this.FilesFindNextToolStripMenuItem.Name = "FilesFindNextToolStripMenuItem";
            this.FilesFindNextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.FilesFindNextToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesFindNextToolStripMenuItem.Text = "Find Next";
            this.FilesFindNextToolStripMenuItem.Click += new System.EventHandler(this.FilesFindNextToolStripMenuItemClick);
            //
            // FilesFindPreviousToolStripMenuItem
            //
            this.FilesFindPreviousToolStripMenuItem.Enabled = false;
            this.FilesFindPreviousToolStripMenuItem.Name = "FilesFindPreviousToolStripMenuItem";
            this.FilesFindPreviousToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.FilesFindPreviousToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesFindPreviousToolStripMenuItem.Text = "Find Previous";
            this.FilesFindPreviousToolStripMenuItem.Click += new System.EventHandler(this.FilesFindPreviousToolStripMenuItemClick);
            //
            // ToolStripMenuItemSeparator9
            //
            this.ToolStripMenuItemSeparator9.Name = "ToolStripMenuItemSeparator9";
            this.ToolStripMenuItemSeparator9.Size = new System.Drawing.Size(218, 6);
            //
            // FilesExitToolStripMenuItem
            //
            this.FilesExitToolStripMenuItem.Name = "FilesExitToolStripMenuItem";
            this.FilesExitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.FilesExitToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.FilesExitToolStripMenuItem.Text = "E&xit";
            this.FilesExitToolStripMenuItem.Click += new System.EventHandler(this.FilesExitToolStripMenuItemClick);
            //
            // GotoToolStripMenuItem
            //
            this.GotoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GotoLogicalDriveToolStripMenuItem,
            this.ToolStripMenuItemSeparator6,
            this.GotoParentFolderToolStripMenuItem,
            this.GotoCustomFolderToolStripMenuItem,
            this.ToolStripMenuItemSeparator5,
            this.GotoUserFolderToolStripMenuItem,
            this.GotoDownloadsFolderToolStripMenuItem});
            this.GotoToolStripMenuItem.Name = "GotoToolStripMenuItem";
            this.GotoToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.GotoToolStripMenuItem.Text = "&Goto";
            //
            // GotoLogicalDriveToolStripMenuItem
            //
            this.GotoLogicalDriveToolStripMenuItem.Name = "GotoLogicalDriveToolStripMenuItem";
            this.GotoLogicalDriveToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.GotoLogicalDriveToolStripMenuItem.Text = "Logical &Drive";
            //
            // ToolStripMenuItemSeparator6
            //
            this.ToolStripMenuItemSeparator6.Name = "ToolStripMenuItemSeparator6";
            this.ToolStripMenuItemSeparator6.Size = new System.Drawing.Size(211, 6);
            //
            // GotoParentFolderToolStripMenuItem
            //
            this.GotoParentFolderToolStripMenuItem.Name = "GotoParentFolderToolStripMenuItem";
            this.GotoParentFolderToolStripMenuItem.ShortcutKeyDisplayString = "Back, Left";
            this.GotoParentFolderToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.GotoParentFolderToolStripMenuItem.Text = "Parent Folder";
            this.GotoParentFolderToolStripMenuItem.Click += new System.EventHandler(this.GotoParentFolderToolStripMenuItemClick);
            //
            // GotoCustomFolderToolStripMenuItem
            //
            this.GotoCustomFolderToolStripMenuItem.Name = "GotoCustomFolderToolStripMenuItem";
            this.GotoCustomFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.GotoCustomFolderToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.GotoCustomFolderToolStripMenuItem.Text = "Custom Folder";
            this.GotoCustomFolderToolStripMenuItem.Click += new System.EventHandler(this.GotoCustomFolderToolStripMenuItemClick);
            //
            // ToolStripMenuItemSeparator5
            //
            this.ToolStripMenuItemSeparator5.Name = "ToolStripMenuItemSeparator5";
            this.ToolStripMenuItemSeparator5.Size = new System.Drawing.Size(211, 6);
            //
            // GotoUserFolderToolStripMenuItem
            //
            this.GotoUserFolderToolStripMenuItem.Name = "GotoUserFolderToolStripMenuItem";
            this.GotoUserFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.GotoUserFolderToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.GotoUserFolderToolStripMenuItem.Text = "User Folder";
            this.GotoUserFolderToolStripMenuItem.Click += new System.EventHandler(this.GotoUserFolderToolStripMenuItemClick);
            //
            // GotoDownloadsFolderToolStripMenuItem
            //
            this.GotoDownloadsFolderToolStripMenuItem.Name = "GotoDownloadsFolderToolStripMenuItem";
            this.GotoDownloadsFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.GotoDownloadsFolderToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.GotoDownloadsFolderToolStripMenuItem.Text = "Downloads Folder";
            this.GotoDownloadsFolderToolStripMenuItem.Click += new System.EventHandler(this.GotoDownloadsFolderToolStripMenuItemClick);
            //
            // ToolsToolStripMenuItem
            //
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolsLightFileExplorerHereToolStripMenuItem,
            this.ToolStripMenuItemSeparator3,
            this.ToolsFileExplorerHereToolStripMenuItem,
            this.ToolsCommandPromptHereToolStripMenuItem,
            this.ToolsPowerShellConsoleHereToolStripMenuItem});
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.ToolsToolStripMenuItem.Text = "&Tools";
            //
            // ToolsLightFileExplorerHereToolStripMenuItem
            //
            this.ToolsLightFileExplorerHereToolStripMenuItem.Name = "ToolsLightFileExplorerHereToolStripMenuItem";
            this.ToolsLightFileExplorerHereToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.ToolsLightFileExplorerHereToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.ToolsLightFileExplorerHereToolStripMenuItem.Text = "Light File Explorer";
            this.ToolsLightFileExplorerHereToolStripMenuItem.Click += new System.EventHandler(this.ToolsLightFileExplorerToolStripMenuItemClick);
            //
            // ToolStripMenuItemSeparator3
            //
            this.ToolStripMenuItemSeparator3.Name = "ToolStripMenuItemSeparator3";
            this.ToolStripMenuItemSeparator3.Size = new System.Drawing.Size(218, 6);
            //
            // ToolsFileExplorerHereToolStripMenuItem
            //
            this.ToolsFileExplorerHereToolStripMenuItem.Name = "ToolsFileExplorerHereToolStripMenuItem";
            this.ToolsFileExplorerHereToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ToolsFileExplorerHereToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.ToolsFileExplorerHereToolStripMenuItem.Text = "File Explorer";
            this.ToolsFileExplorerHereToolStripMenuItem.Click += new System.EventHandler(this.ToolsFileExplorerToolStripMenuItemClick);
            //
            // ToolsCommandPromptHereToolStripMenuItem
            //
            this.ToolsCommandPromptHereToolStripMenuItem.Name = "ToolsCommandPromptHereToolStripMenuItem";
            this.ToolsCommandPromptHereToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.ToolsCommandPromptHereToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.ToolsCommandPromptHereToolStripMenuItem.Text = "Command Prompt";
            this.ToolsCommandPromptHereToolStripMenuItem.Click += new System.EventHandler(this.ToolsCommandPromptToolStripMenuItemClick);
            //
            // ToolsPowerShellConsoleHereToolStripMenuItem
            //
            this.ToolsPowerShellConsoleHereToolStripMenuItem.Name = "ToolsPowerShellConsoleHereToolStripMenuItem";
            this.ToolsPowerShellConsoleHereToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.ToolsPowerShellConsoleHereToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.ToolsPowerShellConsoleHereToolStripMenuItem.Text = "PowerShell Console";
            this.ToolsPowerShellConsoleHereToolStripMenuItem.Click += new System.EventHandler(this.ToolsPowerShellConsoleToolStripMenuItemClick);
            //
            // ViewToolStripMenuItem
            //
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewSortToolStripMenuItem});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.ViewToolStripMenuItem.Text = "&View";
            //
            // ViewSortToolStripMenuItem
            //
            this.ViewSortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewSortNameToolStripMenuItem,
            this.ViewSortExtensionToolStripMenuItem,
            this.ViewSortSizeToolStripMenuItem,
            this.ViewSortDateModifiedToolStripMenuItem,
            this.ViewSortAttributesToolStripMenuItem});
            this.ViewSortToolStripMenuItem.Name = "ViewSortToolStripMenuItem";
            this.ViewSortToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.ViewSortToolStripMenuItem.Text = "&Sort";
            //
            // ViewSortNameToolStripMenuItem
            //
            this.ViewSortNameToolStripMenuItem.Name = "ViewSortNameToolStripMenuItem";
            this.ViewSortNameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
            | System.Windows.Forms.Keys.D1)));
            this.ViewSortNameToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ViewSortNameToolStripMenuItem.Text = "&Name";
            this.ViewSortNameToolStripMenuItem.Click += new System.EventHandler(this.ViewSortNameToolStripMenuItemClick);
            //
            // ViewSortExtensionToolStripMenuItem
            //
            this.ViewSortExtensionToolStripMenuItem.Name = "ViewSortExtensionToolStripMenuItem";
            this.ViewSortExtensionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
            | System.Windows.Forms.Keys.D2)));
            this.ViewSortExtensionToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ViewSortExtensionToolStripMenuItem.Text = "&Extension";
            this.ViewSortExtensionToolStripMenuItem.Click += new System.EventHandler(this.ViewSortExtensionToolStripMenuItemClick);
            //
            // ViewSortSizeToolStripMenuItem
            //
            this.ViewSortSizeToolStripMenuItem.Name = "ViewSortSizeToolStripMenuItem";
            this.ViewSortSizeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
            | System.Windows.Forms.Keys.D3)));
            this.ViewSortSizeToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ViewSortSizeToolStripMenuItem.Text = "&Size";
            this.ViewSortSizeToolStripMenuItem.Click += new System.EventHandler(this.ViewSortSizeToolStripMenuItemClick);
            //
            // ViewSortDateModifiedToolStripMenuItem
            //
            this.ViewSortDateModifiedToolStripMenuItem.Name = "ViewSortDateModifiedToolStripMenuItem";
            this.ViewSortDateModifiedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
            | System.Windows.Forms.Keys.D4)));
            this.ViewSortDateModifiedToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ViewSortDateModifiedToolStripMenuItem.Text = "Date &Modified";
            this.ViewSortDateModifiedToolStripMenuItem.Click += new System.EventHandler(this.ViewSortDateModifiedToolStripMenuItemClick);
            //
            // ViewSortAttributesToolStripMenuItem
            //
            this.ViewSortAttributesToolStripMenuItem.Name = "ViewSortAttributesToolStripMenuItem";
            this.ViewSortAttributesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
            | System.Windows.Forms.Keys.D5)));
            this.ViewSortAttributesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.ViewSortAttributesToolStripMenuItem.Text = "&Attributes";
            this.ViewSortAttributesToolStripMenuItem.Click += new System.EventHandler(this.ViewSortAttributesToolStripMenuItemClick);
            //
            // HelpToolStripMenuItem
            //
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpProjectPageToolStripMenuItem,
            this.ToolStripMenuItemSeparator10,
            this.HelpAboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Text = "&Help";
            //
            // HelpProjectPageToolStripMenuItem
            //
            this.HelpProjectPageToolStripMenuItem.Name = "HelpProjectPageToolStripMenuItem";
            this.HelpProjectPageToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.HelpProjectPageToolStripMenuItem.Text = "Project &Page";
            this.HelpProjectPageToolStripMenuItem.Click += new System.EventHandler(this.HelpProjectPageToolStripMenuItemClick);
            //
            // ToolStripMenuItemSeparator10
            //
            this.ToolStripMenuItemSeparator10.Name = "ToolStripMenuItemSeparator10";
            this.ToolStripMenuItemSeparator10.Size = new System.Drawing.Size(137, 6);
            //
            // HelpAboutToolStripMenuItem
            //
            this.HelpAboutToolStripMenuItem.Name = "HelpAboutToolStripMenuItem";
            this.HelpAboutToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.HelpAboutToolStripMenuItem.Text = "&About";
            this.HelpAboutToolStripMenuItem.Click += new System.EventHandler(this.HelpAboutToolStripMenuItemClick);
            //
            // StatusStrip
            //
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripMessageLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 638);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1104, 22);
            this.StatusStrip.TabIndex = 2;
            //
            // StatusStripMessageLabel
            //
            this.StatusStripMessageLabel.Name = "StatusStripMessageLabel";
            this.StatusStripMessageLabel.Size = new System.Drawing.Size(1089, 17);
            this.StatusStripMessageLabel.Spring = true;
            this.StatusStripMessageLabel.Text = "Welcome!";
            this.StatusStripMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // Timer
            //
            this.Timer.Enabled = true;
            this.Timer.Interval = ConfigurationUtility.FileSystemWatcherTimerInterval;
            this.Timer.Tick += new System.EventHandler(this.FileSystemWatcherTimerTick);
            //
            // FileView
            //
            this.FileView.AllowDrop = true;
            this.FileView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileViewColumnHeaderName,
            this.FileViewColumnHeaderExtension,
            this.FileViewColumnHeaderSize,
            this.FileViewColumnHeaderDateModified,
            this.FileViewColumnHeaderAttributes});
            this.FileView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileView.FullRowSelect = true;
            this.FileView.HideSelection = false;
            this.FileView.LabelWrap = false;
            this.FileView.Location = new System.Drawing.Point(0, 24);
            this.FileView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FileView.Name = "FileView";
            this.FileView.ShowGroups = false;
            this.FileView.Size = new System.Drawing.Size(1104, 614);
            this.FileView.SmallImageList = this.FileIcons;
            this.FileView.TabIndex = 1;
            this.FileView.UseCompatibleStateImageBehavior = false;
            this.FileView.View = System.Windows.Forms.View.Details;
            this.FileView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.FileViewColumnClick);
            this.FileView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.FileViewItemDrag);
            this.FileView.SelectedIndexChanged += new System.EventHandler(this.FileViewSelectedIndexChanged);
            this.FileView.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileViewDragDrop);
            this.FileView.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileViewDragEnter);
            this.FileView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileViewKeyDown);
            this.FileView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FileViewMouseClick);
            this.FileView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FileViewMouseDoubleClick);
            //
            // FileViewColumnHeaderName
            //
            this.FileViewColumnHeaderName.Text = "Name ↑";
            this.FileViewColumnHeaderName.Width = 675;
            //
            // FileViewColumnHeaderExtension
            //
            this.FileViewColumnHeaderExtension.Text = "Extension";
            this.FileViewColumnHeaderExtension.Width = 100;
            //
            // FileViewColumnHeaderSize
            //
            this.FileViewColumnHeaderSize.Text = "Size";
            this.FileViewColumnHeaderSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FileViewColumnHeaderSize.Width = 110;
            //
            // FileViewColumnHeaderDateModified
            //
            this.FileViewColumnHeaderDateModified.Text = "Date Modified";
            this.FileViewColumnHeaderDateModified.Width = 115;
            //
            // FileViewColumnHeaderAttributes
            //
            this.FileViewColumnHeaderAttributes.Text = "Attributes";
            this.FileViewColumnHeaderAttributes.Width = 80;
            //
            // MainWindow
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 660);
            this.Controls.Add(this.FileView);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.StatusStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainWindow";
            this.Text = "LFE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindowFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindowFormClosed);
            this.Load += new System.EventHandler(this.MainWindowLoad);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}