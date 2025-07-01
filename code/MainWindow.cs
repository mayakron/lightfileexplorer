using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal partial class MainWindow : Form
    {
        private readonly ConcurrentQueue<Action> FileSystemWatcherQueue = new ConcurrentQueue<Action>();

        private readonly FileViewColumnAttributesSorter FileViewColumnAttributesSorter = new FileViewColumnAttributesSorter();

        private readonly FileViewColumnExtensionSorter FileViewColumnExtensionSorter = new FileViewColumnExtensionSorter();

        private readonly FileViewColumnLastModifiedSorter FileViewColumnLastModifiedSorter = new FileViewColumnLastModifiedSorter();

        private readonly FileViewColumnNameSorter FileViewColumnNameSorter = new FileViewColumnNameSorter();

        private readonly FileViewColumnSizeSorter FileViewColumnSizeSorter = new FileViewColumnSizeSorter();

        private string CurrentPath;

        private FileSystemWatcher FileSystemWatcher = null;

        private Regex FindRegex;

        private int NumberOfRunningAsyncOperations = 0;

        public MainWindow()
        {
            Keys GetDigitShortcutKeys(Keys baseKeys, int index)
            {
                switch (index)
                {
                    case 0: return baseKeys | Keys.D1;
                    case 1: return baseKeys | Keys.D2;
                    case 2: return baseKeys | Keys.D3;
                    case 3: return baseKeys | Keys.D4;
                    case 4: return baseKeys | Keys.D5;
                    case 5: return baseKeys | Keys.D6;
                    case 6: return baseKeys | Keys.D7;
                    case 7: return baseKeys | Keys.D8;
                    case 8: return baseKeys | Keys.D9;
                    case 9: return baseKeys | Keys.D0;
                }

                return Keys.None;
            }

            Keys GetFunctionShortcutKeys(Keys baseKeys, int index)
            {
                switch (index)
                {
                    case 0: return baseKeys | Keys.F1;
                    case 1: return baseKeys | Keys.F2;
                    case 2: return baseKeys | Keys.F3;
                    case 3: return baseKeys | Keys.F4;
                    case 4: return baseKeys | Keys.F5;
                    case 5: return baseKeys | Keys.F6;
                    case 6: return baseKeys | Keys.F7;
                    case 7: return baseKeys | Keys.F8;
                    case 8: return baseKeys | Keys.F9;
                    case 9: return baseKeys | Keys.F10;
                }

                return Keys.None;
            }

            ToolStripMenuItem CreateFilesOpenWithMenuItem(string text, string path, Keys shortcutKeys)
            {
                var menuItem = new ToolStripMenuItem();

                menuItem.Text = text;

                menuItem.ShortcutKeys = shortcutKeys;

                menuItem.Click += (sender, e) =>
                {
                    try
                    {
                        if (this.FileView.SelectedItems.Count > 0)
                        {
                            foreach (ListViewItem viewItem in this.FileView.SelectedItems)
                            {
                                Process.Start(new ProcessStartInfo { FileName = path, Arguments = $"\"{Path.Combine(this.CurrentPath, viewItem.Name)}\"", WorkingDirectory = this.CurrentPath, UseShellExecute = false });
                            }
                        }
                        else
                        {
                            throw new Exception("At least one item must be selected.");
                        }
                    }
                    catch (Exception ex)
                    {
                        this.MainWindow_ReportError(ex);
                    }
                };

                return menuItem;
            }

            ToolStripMenuItem CreateGotoKnownFolderMenuItem(string text, string path, Keys shortcutKeys)
            {
                var menuItem = new ToolStripMenuItem();

                menuItem.Text = text;

                menuItem.ShortcutKeys = shortcutKeys;

                menuItem.Click += (sender, e) =>
                {
                    try
                    {
                        this.MainWindow_GotoFolder(path);
                    }
                    catch (Exception ex)
                    {
                        this.MainWindow_ReportError(ex);
                    }
                };

                return menuItem;
            };

            InitializeComponent();

            this.Icon = IconUtility.GetExeIcon();

            this.CurrentPath = Environment.CurrentDirectory;

            this.FileView.ListViewItemSorter = this.FileViewColumnNameSorter;

            if (ConfigurationUtility.OpenWith.Count > 0)
            {
                var filesOpenWithMenuItem = new ToolStripMenuItem();

                filesOpenWithMenuItem.Text = "Open &With...";

                for (int index = 0; index < ConfigurationUtility.OpenWith.Count; index++)
                {
                    var element = ConfigurationUtility.OpenWith[index];

                    filesOpenWithMenuItem.DropDownItems.Add(CreateFilesOpenWithMenuItem(element.Item1, element.Item2, GetFunctionShortcutKeys(Keys.Control, index)));
                }

                this.FilesToolStripMenuItem.DropDownItems.Insert(3, filesOpenWithMenuItem);
            }

            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"A:\", @"A:\", Keys.Control | Keys.Alt | Keys.A));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"B:\", @"B:\", Keys.Control | Keys.Alt | Keys.B));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"C:\", @"C:\", Keys.Control | Keys.Alt | Keys.C));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"D:\", @"D:\", Keys.Control | Keys.Alt | Keys.D));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"E:\", @"E:\", Keys.Control | Keys.Alt | Keys.E));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"F:\", @"F:\", Keys.Control | Keys.Alt | Keys.F));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"G:\", @"G:\", Keys.Control | Keys.Alt | Keys.G));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"H:\", @"H:\", Keys.Control | Keys.Alt | Keys.H));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"I:\", @"I:\", Keys.Control | Keys.Alt | Keys.I));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"J:\", @"J:\", Keys.Control | Keys.Alt | Keys.J));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"K:\", @"K:\", Keys.Control | Keys.Alt | Keys.K));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"L:\", @"L:\", Keys.Control | Keys.Alt | Keys.L));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"M:\", @"M:\", Keys.Control | Keys.Alt | Keys.M));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"N:\", @"N:\", Keys.Control | Keys.Alt | Keys.N));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"O:\", @"O:\", Keys.Control | Keys.Alt | Keys.O));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"P:\", @"P:\", Keys.Control | Keys.Alt | Keys.P));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"Q:\", @"Q:\", Keys.Control | Keys.Alt | Keys.Q));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"R:\", @"R:\", Keys.Control | Keys.Alt | Keys.R));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"S:\", @"S:\", Keys.Control | Keys.Alt | Keys.S));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"T:\", @"T:\", Keys.Control | Keys.Alt | Keys.T));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"U:\", @"U:\", Keys.Control | Keys.Alt | Keys.U));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"V:\", @"V:\", Keys.Control | Keys.Alt | Keys.V));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"W:\", @"W:\", Keys.Control | Keys.Alt | Keys.W));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"X:\", @"X:\", Keys.Control | Keys.Alt | Keys.X));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"Y:\", @"Y:\", Keys.Control | Keys.Alt | Keys.Y));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"Z:\", @"Z:\", Keys.Control | Keys.Alt | Keys.Z));

            if (ConfigurationUtility.GotoFavorites.Count > 0)
            {
                this.GotoToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

                for (int index = 0; index < ConfigurationUtility.GotoFavorites.Count; index++)
                {
                    var element = ConfigurationUtility.GotoFavorites[index];

                    this.GotoToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(element.Item1, element.Item2, GetDigitShortcutKeys(Keys.Control, index)));
                }
            }
        }

        private void FilesAdvancedSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var inputWindow = new InputTextWindow("Advanced Selection", "&Name Regex:");

                if (inputWindow.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(inputWindow.Input1))
                    {
                        var nameRegex = new Regex(inputWindow.Input1, RegexOptions.IgnoreCase);

                        this.FileView.BeginUpdate();

                        try
                        {
                            this.FileView.SelectedItems.Clear();

                            foreach (ListViewItem viewItem in this.FileView.Items)
                            {
                                if (nameRegex.IsMatch(viewItem.Name))
                                {
                                    viewItem.Selected = true;
                                }
                            }
                        }
                        finally
                        {
                            this.FileView.EndUpdate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesCopyNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    Clipboard.SetText(string.Join(Environment.NewLine, this.FileView.SelectedItems.Cast<ListViewItem>().Select(x => x.Name)));
                }
                else
                {
                    throw new Exception("At least one item must be selected.");
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesCopyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    Clipboard.SetText(string.Join(Environment.NewLine, this.FileView.SelectedItems.Cast<ListViewItem>().Select(x => Path.Combine(this.CurrentPath, x.Name))));
                }
                else
                {
                    throw new Exception("At least one item must be selected.");
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    var fileDropList = new StringCollection();

                    foreach (ListViewItem viewItem in this.FileView.SelectedItems)
                    {
                        fileDropList.Add(Path.Combine(this.CurrentPath, viewItem.Name));
                    }

                    Clipboard.SetFileDropList(fileDropList);
                }
                else
                {
                    throw new Exception("At least one item must be selected.");
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesCopyToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    var inputWindow = new InputTextWindow("Copy To", "&Path:");

                    if (inputWindow.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(inputWindow.Input1))
                        {
                            var destinationPath = inputWindow.Input1;

                            if (!FileUtility.DirectoryExists(destinationPath))
                            {
                                throw new Exception($"The \"{destinationPath}\" path does not exist.");
                            }

                            this.MainWindow_RunAsyncOperation
                            (
                                (asyncOperationParameter) =>
                                {
                                    var exceptions = new List<Exception>();

                                    foreach (var element in asyncOperationParameter)
                                    {
                                        try
                                        {
                                            FileUtility.CopyPath(element.Item1, element.Item2);
                                        }
                                        catch (Exception ex)
                                        {
                                            exceptions.Add(ex);
                                        }
                                    }

                                    if (exceptions.Count > 0)
                                    {
                                        throw new AggregateException(exceptions);
                                    }
                                },
                                this.FileView.SelectedItems.Cast<ListViewItem>().Select(x => new ValueTuple<string, string>(Path.Combine(this.CurrentPath, x.Name), Path.Combine(destinationPath, x.Name))).ToList(),
                                $"Copying items to \"{destinationPath}\"..."
                            );
                        }
                    }
                }
                else
                {
                    throw new Exception("At least one item must be selected.");
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesCutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    var fileDropList = new StringCollection();

                    foreach (ListViewItem viewItem in this.FileView.SelectedItems)
                    {
                        fileDropList.Add(Path.Combine(this.CurrentPath, viewItem.Name));
                    }

                    using (var memoryStream = new MemoryStream(new byte[] { 2, 0, 0, 0 }))
                    {
                        DataObject dataObject = new DataObject();

                        dataObject.SetFileDropList(fileDropList);

                        dataObject.SetData("Preferred DropEffect", memoryStream);

                        Clipboard.SetDataObject(dataObject, true);
                    }
                }
                else
                {
                    throw new Exception("At least one item must be selected.");
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    this.MainWindow_RunAsyncOperation
                    (
                        (asyncOperationParameter) =>
                        {
                            var exceptions = new List<Exception>();

                            foreach (var element in asyncOperationParameter)
                            {
                                try
                                {
                                    FileUtility.DeletePath(element);
                                }
                                catch (Exception ex)
                                {
                                    exceptions.Add(ex);
                                }
                            }

                            if (exceptions.Count > 0)
                            {
                                throw new AggregateException(exceptions);
                            }
                        },
                        this.FileView.SelectedItems.Cast<ListViewItem>().Select(x => Path.Combine(this.CurrentPath, x.Name)).ToList(),
                        $"Deleting items from \"{this.CurrentPath}\"..."
                    );
                }
                else
                {
                    throw new Exception("At least one item must be selected.");
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FilesFindNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FindRegex != null)
                {
                    if (!FileViewUtility.FindNext(this.FileView, this.FindRegex))
                    {
                        MessageBox.Show("Cannot find a file matching the specified criteria.", "Find Next", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesFindPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FindRegex != null)
                {
                    if (!FileViewUtility.FindPrevious(this.FileView, this.FindRegex))
                    {
                        MessageBox.Show("Cannot find a file matching the specified criteria.", "Find Previous", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesFindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var inputWindow = new InputTextWindow("Find", "&Name Regex:");

                if (inputWindow.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(inputWindow.Input1))
                    {
                        this.FindRegex = new Regex(inputWindow.Input1, RegexOptions.IgnoreCase);

                        this.FilesFindNextToolStripMenuItem.Enabled = true;
                        this.FilesFindPreviousToolStripMenuItem.Enabled = true;

                        if (!FileViewUtility.FindNext(this.FileView, this.FindRegex))
                        {
                            MessageBox.Show("Cannot find a file matching the specified criteria.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesInvertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FileView.BeginUpdate();

            try
            {
                for (int index = 0; index < this.FileView.Items.Count; index++)
                {
                    var viewItem = this.FileView.Items[index];

                    viewItem.Selected = !viewItem.Selected;
                }
            }
            finally
            {
                this.FileView.EndUpdate();
            }
        }

        private void FilesMoveToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    var inputWindow = new InputTextWindow("Move To", "&Path:");

                    if (inputWindow.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(inputWindow.Input1))
                        {
                            var destinationPath = inputWindow.Input1;

                            if (!FileUtility.DirectoryExists(destinationPath))
                            {
                                throw new Exception($"The \"{destinationPath}\" path does not exist.");
                            }

                            this.MainWindow_RunAsyncOperation
                            (
                                (asyncOperationParameter) =>
                                {
                                    var exceptions = new List<Exception>();

                                    foreach (var element in asyncOperationParameter)
                                    {
                                        try
                                        {
                                            FileUtility.MovePath(element.Item1, element.Item2);
                                        }
                                        catch (Exception ex)
                                        {
                                            exceptions.Add(ex);
                                        }
                                    }

                                    if (exceptions.Count > 0)
                                    {
                                        throw new AggregateException(exceptions);
                                    }
                                },
                                this.FileView.SelectedItems.Cast<ListViewItem>().Select(x => new ValueTuple<string, string>(Path.Combine(this.CurrentPath, x.Name), Path.Combine(destinationPath, x.Name))).ToList(),
                                $"Moving items to \"{destinationPath}\"..."
                            );
                        }
                    }
                }
                else
                {
                    throw new Exception("At least one item must be selected.");
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesNewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var inputWindow = new InputTextWindow("New Folder", "Folder &Name:");

                if (inputWindow.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(inputWindow.Input1))
                    {
                        var folderName = inputWindow.Input1;

                        FileUtility.ValidateName(folderName);

                        if (FileUtility.PathExists(folderName))
                        {
                            throw new Exception($"The \"{folderName}\" path already exists.");
                        }

                        FileUtility.CreateDirectory(folderName);

                        this.FileView.BeginUpdate();

                        try
                        {
                            this.FileView.SelectedItems.Clear();

                            FileViewUtility.MoveTo(FileViewUtility.AddFolder(this.FileView, folderName, DateTime.Now, FileAttributes.Normal));
                        }
                        finally
                        {
                            this.FileView.EndUpdate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesNewShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var inputWindow = new InputTextTextWindow(this, "New Shortcut", "&Shortcut Name:", "&Target Location:");

                if (inputWindow.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(inputWindow.Input1) && !string.IsNullOrEmpty(inputWindow.Input2))
                    {
                        var shortcutName = inputWindow.Input1;

                        FileUtility.ValidateName(shortcutName);

                        if (!shortcutName.EndsWith(".lnk", StringComparison.InvariantCultureIgnoreCase))
                        {
                            shortcutName += ".lnk";
                        }

                        if (FileUtility.PathExists(shortcutName))
                        {
                            throw new Exception($"The \"{shortcutName}\" path already exists.");
                        }

                        var targetLocation = inputWindow.Input2;

                        ShellLinkUtility.CreateShellLink(shortcutName, targetLocation);

                        this.FileView.BeginUpdate();

                        try
                        {
                            this.FileView.SelectedItems.Clear();

                            FileViewUtility.MoveTo(FileViewUtility.AddFile(this.FileView, shortcutName, 0, DateTime.Now, FileAttributes.Normal));
                        }
                        finally
                        {
                            this.FileView.EndUpdate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    if (this.FileView.SelectedItems.Count > 1)
                    {
                        foreach (ListViewItem viewItem in this.FileView.SelectedItems)
                        {
                            if (!FileViewUtility.IsDirectory(viewItem))
                            {
                                Process.Start(new ProcessStartInfo { FileName = Path.Combine(this.CurrentPath, viewItem.Name), WorkingDirectory = this.CurrentPath, UseShellExecute = true });
                            }
                        }
                    }
                    else
                    {
                        var viewItem = this.FileView.SelectedItems[0];

                        if (!FileViewUtility.IsDirectory(viewItem))
                        {
                            Process.Start(new ProcessStartInfo { FileName = Path.Combine(this.CurrentPath, viewItem.Name), WorkingDirectory = this.CurrentPath, UseShellExecute = true });
                        }
                        else
                        {
                            this.MainWindow_GotoFolder(Path.Combine(this.CurrentPath, viewItem.Name));
                        }
                    }
                }
                else
                {
                    throw new Exception("At least one item must be selected.");
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesPasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Shell Clipboard Formats:
            // https://learn.microsoft.com/en-us/windows/win32/shell/clipboard

            bool ProcessDataObjectFileContent(System.Runtime.InteropServices.ComTypes.IDataObject dataObject, int fileIndex, string destinationPath)
            {
                var formatetc = new System.Runtime.InteropServices.ComTypes.FORMATETC
                {
                    cfFormat = (short)DataFormats.GetFormat("FileContents").Id,
                    dwAspect = System.Runtime.InteropServices.ComTypes.DVASPECT.DVASPECT_CONTENT,
                    lindex = fileIndex,
                    ptd = IntPtr.Zero,
                    tymed = System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTREAM
                };

                dataObject.GetData(ref formatetc, out System.Runtime.InteropServices.ComTypes.STGMEDIUM stgmedium);

                switch (stgmedium.tymed)
                {
                    case System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTREAM:

                        try
                        {
                            var iStream = (System.Runtime.InteropServices.ComTypes.IStream)Marshal.GetObjectForIUnknown(stgmedium.unionmember);

                            try
                            {
                                byte[] buffer = new byte[4096];

                                ulong bufferLength = (ulong)buffer.Length;

                                unsafe
                                {
                                    using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.Read))
                                    {
                                        ulong bytesRead = 0;

                                        do
                                        {
                                            iStream.Read(buffer, buffer.Length, (IntPtr)(&bytesRead));

                                            if (bytesRead > 0)
                                            {
                                                fileStream.Write(buffer, 0, (int)bytesRead);
                                            }
                                        }
                                        while (!(bytesRead < bufferLength));
                                    }
                                }
                            }
                            finally
                            {
                                Marshal.ReleaseComObject(iStream);
                            }
                        }
                        finally
                        {
                            Marshal.Release(stgmedium.unionmember);
                        }

                        return true;
                }

                return false;
            }

            try
            {
                var dataObject = Clipboard.GetDataObject();

                if (dataObject != null)
                {
                    if (dataObject.GetDataPresent("FileDrop", false))
                    {
                        var fileDropList = Clipboard.GetFileDropList();

                        if (fileDropList == null)
                        {
                            throw new Exception("An error occured while processing Clipboard data.");
                        }

                        if (!(fileDropList.Count > 0))
                        {
                            throw new Exception("An error occured while processing Clipboard data.");
                        }

                        var fileDropEffect = 1;

                        var fileDropEffectData = Clipboard.GetData("Preferred DropEffect");

                        if (fileDropEffectData != null)
                        {
                            byte[] fileDropEffectDataBytes;

                            using (var memoryStream = fileDropEffectData as MemoryStream)
                            {
                                fileDropEffectDataBytes = memoryStream.ToArray();
                            }

                            if ((fileDropEffectDataBytes.Length >= 4) && (fileDropEffectDataBytes[0] == 2) && (fileDropEffectDataBytes[1] == 0) && (fileDropEffectDataBytes[2] == 0) && (fileDropEffectDataBytes[3] == 0))
                            {
                                fileDropEffect = 2;
                            }
                        }

                        switch (fileDropEffect)
                        {
                            case 1:

                                this.MainWindow_RunAsyncOperation
                                (
                                    (asyncOperationParameter) =>
                                    {
                                        var exceptions = new List<Exception>();

                                        foreach (var element in asyncOperationParameter)
                                        {
                                            try
                                            {
                                                FileUtility.CopyPath(element.Item1, element.Item2);
                                            }
                                            catch (Exception ex)
                                            {
                                                exceptions.Add(ex);
                                            }
                                        }

                                        if (exceptions.Count > 0)
                                        {
                                            throw new AggregateException(exceptions);
                                        }
                                    },
                                    fileDropList.Cast<string>().Select(x => new ValueTuple<string, string>(x, Path.Combine(this.CurrentPath, Path.GetFileName(x)))).ToList(),
                                    $"Copying items to \"{this.CurrentPath}\"..."
                                );

                                break;

                            case 2:

                                this.MainWindow_RunAsyncOperation
                                (
                                    (asyncOperationParameter) =>
                                    {
                                        var exceptions = new List<Exception>();

                                        foreach (var element in asyncOperationParameter)
                                        {
                                            try
                                            {
                                                FileUtility.MovePath(element.Item1, element.Item2);
                                            }
                                            catch (Exception ex)
                                            {
                                                exceptions.Add(ex);
                                            }
                                        }

                                        if (exceptions.Count > 0)
                                        {
                                            throw new AggregateException(exceptions);
                                        }
                                    },
                                    fileDropList.Cast<string>().Select(x => new ValueTuple<string, string>(x, Path.Combine(this.CurrentPath, Path.GetFileName(x)))).ToList(),
                                    $"Moving items to \"{this.CurrentPath}\"..."
                                );

                                break;
                        }
                    }
                    else if (dataObject.GetDataPresent("FileGroupDescriptorW", false))
                    {
                        this.MainWindow_RunAsyncOperation
                        (
                            (asyncOperationParameter) =>
                            {
                                var fileDropList = new List<string>();

                                byte[] fileGroupDescriptorBytes;

                                using (var fileGroupDescriptorStream = (MemoryStream)asyncOperationParameter.Item1.GetData("FileGroupDescriptorW"))
                                {
                                    fileGroupDescriptorBytes = fileGroupDescriptorStream.ToArray();
                                }

                                var fileGroupDescriptorWPointer = Marshal.AllocHGlobal(fileGroupDescriptorBytes.Length);

                                try
                                {
                                    Marshal.Copy(fileGroupDescriptorBytes, 0, fileGroupDescriptorWPointer, fileGroupDescriptorBytes.Length);

                                    var fileGroupDescriptor = (WindowsApi.FILEGROUPDESCRIPTORW)Marshal.PtrToStructure(fileGroupDescriptorWPointer, typeof(WindowsApi.FILEGROUPDESCRIPTORW));

                                    var fileDescriptorPointer = (IntPtr)((ulong)fileGroupDescriptorWPointer + 4);

                                    for (int fileDescriptorIndex = 0; fileDescriptorIndex < fileGroupDescriptor.cItems; fileDescriptorIndex++)
                                    {
                                        var fileDescriptor = (WindowsApi.FILEDESCRIPTORW)Marshal.PtrToStructure(fileDescriptorPointer, typeof(WindowsApi.FILEDESCRIPTORW));

                                        fileDropList.Add(Path.Combine(asyncOperationParameter.Item2, fileDescriptor.cFileName));

                                        fileDescriptorPointer = (IntPtr)((ulong)fileDescriptorPointer + (ulong)Marshal.SizeOf(fileDescriptor));
                                    }
                                }
                                finally
                                {
                                    Marshal.FreeHGlobal(fileGroupDescriptorWPointer);
                                }

                                var comUnderlyingDataObject = (System.Runtime.InteropServices.ComTypes.IDataObject)asyncOperationParameter.Item1;

                                var exceptions = new List<Exception>();

                                for (int index = 0; index < fileDropList.Count; index++)
                                {
                                    try
                                    {
                                        var fileDropItem = fileDropList[index];

                                        if (!ProcessDataObjectFileContent(comUnderlyingDataObject, index, fileDropItem))
                                        {
                                            throw new Exception($"An error occurred while copying item \"{fileDropItem}\".");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        exceptions.Add(ex);
                                    }
                                }

                                if (exceptions.Count > 0)
                                {
                                    throw new AggregateException(exceptions);
                                }
                            },
                            new Tuple<IDataObject, string>(dataObject, this.CurrentPath),
                            $"Copying items to \"{this.CurrentPath}\"..."
                        );
                    }
                    else
                    {
                        throw new Exception($"Don't know how to handle these Clipboard data formats: {string.Join(", ", dataObject.GetFormats(false))}");
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.MainWindow_GotoFolder(this.CurrentPath, false, true);
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesRenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    if (this.FileView.SelectedItems.Count > 1)
                    {
                        throw new Exception($"Don't know how to handle this operation on more than one item.");
                    }
                    else
                    {
                        var viewItem = this.FileView.SelectedItems[0];

                        var inputWindow = new InputTextWindow("Rename", "New &Name:", viewItem.Name);

                        if (inputWindow.ShowDialog() == DialogResult.OK)
                        {
                            if (!string.IsNullOrEmpty(inputWindow.Input1) && !inputWindow.Input1.Equals(viewItem.Name))
                            {
                                var newName = inputWindow.Input1;

                                FileUtility.ValidateName(newName);

                                // Not checking if the path exists here to allow for changes only in casing.

                                FileUtility.RenamePath(viewItem.Name, newName);

                                FileViewUtility.Rename(viewItem, newName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesSelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FileView.BeginUpdate();

            try
            {
                this.FileView.SelectedIndices.Clear();

                for (int i = 0; i < this.FileView.Items.Count; i++)
                {
                    this.FileView.Items[i].Selected = true;
                }
            }
            finally
            {
                this.FileView.EndUpdate();
            }
        }

        private void FilesViewAsBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem viewItem in this.FileView.SelectedItems)
                    {
                        if (!FileViewUtility.IsDirectory(viewItem))
                        {
                            Process.Start(new ProcessStartInfo { FileName = ConfigurationUtility.BinaryViewer, Arguments = $"\"{Path.Combine(this.CurrentPath, viewItem.Name)}\"", WorkingDirectory = this.CurrentPath, UseShellExecute = false });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FilesViewAsTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem viewItem in this.FileView.SelectedItems)
                    {
                        if (!FileViewUtility.IsDirectory(viewItem))
                        {
                            Process.Start(new ProcessStartInfo { FileName = ConfigurationUtility.TextViewer, Arguments = $"\"{Path.Combine(this.CurrentPath, viewItem.Name)}\"", WorkingDirectory = this.CurrentPath, UseShellExecute = false });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FileSystemWatcher_OnChanged(object source, FileSystemEventArgs e)
        {
            this.FileSystemWatcherQueue.Enqueue
            (
                new Action
                (
                    () =>
                    {
                        var viewItem = this.FileView.Items[e.Name];

                        if (viewItem != null)
                        {
                            FileUtility.ScanSingle
                            (
                                e.FullPath,
                                (name, lastWriteTime, attributes) => { FileViewUtility.SetFolder(viewItem, name, lastWriteTime, attributes); },
                                (name, size, lastWriteTime, attributes) => { FileViewUtility.SetFile(viewItem, name, size, lastWriteTime, attributes); }
                            );
                        }
                        else
                        {
                            FileUtility.ScanSingle
                            (
                                e.FullPath,
                                (name, lastWriteTime, attributes) => { FileViewUtility.AddFolder(this.FileView, name, lastWriteTime, attributes); },
                                (name, size, lastWriteTime, attributes) => { FileViewUtility.AddFile(this.FileView, name, size, lastWriteTime, attributes); }
                            );
                        }
                    }
                )
            );
        }

        private void FileSystemWatcher_OnCreated(object source, FileSystemEventArgs e)
        {
            this.FileSystemWatcherQueue.Enqueue
            (
                new Action
                (
                    () =>
                    {
                        var viewItem = this.FileView.Items[e.Name];

                        if (viewItem != null)
                        {
                            FileUtility.ScanSingle
                            (
                                e.FullPath,
                                (name, lastWriteTime, attributes) => { FileViewUtility.SetFolder(viewItem, name, lastWriteTime, attributes); },
                                (name, size, lastWriteTime, attributes) => { FileViewUtility.SetFile(viewItem, name, size, lastWriteTime, attributes); }
                            );
                        }
                        else
                        { 
                            FileUtility.ScanSingle
                            (
                                e.FullPath,
                                (name, lastWriteTime, attributes) => { FileViewUtility.AddFolder(this.FileView, name, lastWriteTime, attributes); },
                                (name, size, lastWriteTime, attributes) => { FileViewUtility.AddFile(this.FileView, name, size, lastWriteTime, attributes); }
                            );
                        }
                    }
                )
            );
        }

        private void FileSystemWatcher_OnDeleted(object source, FileSystemEventArgs e)
        {
            this.FileSystemWatcherQueue.Enqueue
            (
                new Action
                (
                    () =>
                    {
                        if (!FileUtility.PathExists(e.Name))
                        {
                            this.FileView.Items.RemoveByKey(e.Name);
                        }
                    }
                )
            );
        }

        private void FileSystemWatcher_OnRenamed(object source, RenamedEventArgs e)
        {
            this.FileSystemWatcherQueue.Enqueue
            (
                new Action
                (
                    () =>
                    {
                        var oldViewItem = this.FileView.Items[e.OldName];

                        if (oldViewItem != null)
                        {
                            FileViewUtility.Rename(oldViewItem, e.Name);
                        }
                        else
                        {
                            var viewItem = this.FileView.Items[e.Name];

                            if (viewItem != null)
                            {
                                FileUtility.ScanSingle
                                (
                                    e.FullPath,
                                    (name, lastWriteTime, attributes) => { FileViewUtility.SetFolder(viewItem, name, lastWriteTime, attributes); },
                                    (name, size, lastWriteTime, attributes) => { FileViewUtility.SetFile(viewItem, name, size, lastWriteTime, attributes); }
                                );
                            }
                            else
                            {
                                FileUtility.ScanSingle
                                (
                                    e.FullPath,
                                    (name, lastWriteTime, attributes) => { FileViewUtility.AddFolder(this.FileView, name, lastWriteTime, attributes); },
                                    (name, size, lastWriteTime, attributes) => { FileViewUtility.AddFile(this.FileView, name, size, lastWriteTime, attributes); }
                                );
                            }
                        }
                    }
                )
            );
        }

        private void FileView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                switch (e.Column)
                {
                    case 0:

                        this.FileView.ListViewItemSorter = this.FileViewColumnNameSorter;

                        break;

                    case 1:

                        this.FileView.ListViewItemSorter = this.FileViewColumnExtensionSorter;

                        break;

                    case 2:

                        this.FileView.ListViewItemSorter = this.FileViewColumnSizeSorter;

                        break;

                    case 3:

                        this.FileView.ListViewItemSorter = this.FileViewColumnLastModifiedSorter;

                        break;

                    case 4:

                        this.FileView.ListViewItemSorter = this.FileViewColumnAttributesSorter;

                        break;
                }

                this.StatusStrip_UpdateMessage();
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FileView_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var fileDropList = e.Data.GetData(DataFormats.FileDrop) as string[];

                if (fileDropList != null)
                {
                    if (fileDropList.Length > 0)
                    {
                        switch (e.Effect)
                        {
                            case DragDropEffects.Copy:

                                this.MainWindow_RunAsyncOperation
                                (
                                    (asyncOperationParameter) =>
                                    {
                                        var exceptions = new List<Exception>();

                                        foreach (var element in asyncOperationParameter)
                                        {
                                            try
                                            {
                                                FileUtility.CopyPath(element.Item1, element.Item2);
                                            }
                                            catch (Exception ex)
                                            {
                                                exceptions.Add(ex);
                                            }
                                        }

                                        if (exceptions.Count > 0)
                                        {
                                            throw new AggregateException(exceptions);
                                        }
                                    },
                                    fileDropList.Select(x => new ValueTuple<string, string>(x, Path.Combine(this.CurrentPath, Path.GetFileName(x)))).ToList(),
                                    $"Copying items to \"{this.CurrentPath}\"..."
                                );

                                break;

                            case DragDropEffects.Move:

                                this.MainWindow_RunAsyncOperation
                                (
                                    (asyncOperationParameter) =>
                                    {
                                        var exceptions = new List<Exception>();

                                        foreach (var element in asyncOperationParameter)
                                        {
                                            try
                                            {
                                                FileUtility.MovePath(element.Item1, element.Item2);
                                            }
                                            catch (Exception ex)
                                            {
                                                exceptions.Add(ex);
                                            }
                                        }

                                        if (exceptions.Count > 0)
                                        {
                                            throw new AggregateException(exceptions);
                                        }
                                    },
                                    fileDropList.Select(x => new ValueTuple<string, string>(x, Path.Combine(this.CurrentPath, Path.GetFileName(x)))).ToList(),
                                    $"Moving items to \"{this.CurrentPath}\"..."
                                );

                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FileView_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy; else e.Effect = DragDropEffects.None;
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FileView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            try
            {
                this.DoDragDrop(new DataObject(DataFormats.FileDrop, this.FileView.SelectedItems.Cast<ListViewItem>().Select(x => Path.Combine(this.CurrentPath, x.Name)).ToArray()), DragDropEffects.Copy);
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FileView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Back:
                case Keys.Left:

                    this.GotoParentFolderToolStripMenuItem_Click(sender, null);

                    e.SuppressKeyPress = true;

                    e.Handled = true;

                    break;

                case Keys.Enter:
                case Keys.Right:

                    this.FilesOpenToolStripMenuItem_Click(sender, null);

                    e.SuppressKeyPress = true;

                    e.Handled = true;

                    break;
            }
        }

        private void FileView_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                switch (e.Button)
                {
                    case MouseButtons.Right:

                        if (this.FileView.SelectedItems.Count > 0)
                        {
                            new ShellContextMenu().ShowContextMenu(this.CurrentPath, this.FileView.SelectedItems.Cast<ListViewItem>().Select(x => x.Name).ToList(), Cursor.Position);
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FileView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:

                        this.FilesOpenToolStripMenuItem_Click(sender, null);

                        break;
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void FileView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.StatusStrip_UpdateMessage();
        }

        private void GotoCustomFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var gotoWindow = new GotoWindow(this, this.CurrentPath);

                if (gotoWindow.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(gotoWindow.SelectedPath))
                    {
                        FileUtility.ValidatePath(gotoWindow.SelectedPath);

                        if (!FileUtility.DirectoryExists(gotoWindow.SelectedPath))
                        {
                            throw new Exception($"The \"{gotoWindow.SelectedPath}\" path does not exist.");
                        }

                        this.MainWindow_GotoFolder(gotoWindow.SelectedPath);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void GotoDownloadsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.MainWindow_GotoFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"));
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void GotoParentFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CurrentPath.Length > 3)
                {
                    this.MainWindow_GotoFolder(Path.GetDirectoryName(this.CurrentPath), true);
                }
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void GotoUserFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.MainWindow_GotoFolder(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void GotoWindowsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.MainWindow_GotoFolder(Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void HelpAboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"A simple file explorer, written just for fun. (v. {Application.ProductVersion})", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            // If there's anything that needs clean up before exiting, this is the right place to do it.
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.NumberOfRunningAsyncOperations > 0)
            {
                if (MessageBox.Show($"An operation is still in progress.{Environment.NewLine}Are you sure that you want to quit?", "Exiting... - LFE", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void MainWindow_GotoFolder(string path, bool isGoingToParentFolder = false, bool isRefreshingSameFolder = false)
        {
            if (this.FileSystemWatcher != null)
            {
                while (this.FileSystemWatcherQueue.TryDequeue(out _)) { }

                this.FileSystemWatcher.Dispose();

                this.FileSystemWatcher = null;
            }

            string selectedViewItemName = null;

            if (!string.Equals(this.CurrentPath, path, StringComparison.CurrentCultureIgnoreCase))
            {
                if (isGoingToParentFolder)
                {
                    selectedViewItemName = Path.GetFileName(this.CurrentPath);
                }

                Environment.CurrentDirectory = path;

                this.CurrentPath = path;
            }
            else
            {
                if (isRefreshingSameFolder)
                {
                    if (this.FileView.SelectedItems.Count > 0)
                    {
                        var viewItem = this.FileView.SelectedItems[0];

                        selectedViewItemName = viewItem.Name;
                    }
                }
            }

            this.Text = $"{this.CurrentPath} - LFE";

            this.StatusStrip_UpdateMessage("Getting folder content, please wait...", true);

            try
            {
                this.FileView.BeginUpdate();

                try
                {
                    var listViewItemSorter = this.FileView.ListViewItemSorter;

                    this.FileView.ListViewItemSorter = null;

                    try
                    {
                        this.FileView.Items.Clear();

                        FileUtility.ScanMany
                        (
                            this.CurrentPath,
                            (name, lastWriteTime, attributes) => { FileViewUtility.AddFolder(this.FileView, name, lastWriteTime, attributes); },
                            (name, size, lastWriteTime, attributes) => { FileViewUtility.AddFile(this.FileView, name, size, lastWriteTime, attributes); }
                        );
                    }
                    finally
                    {
                        this.FileView.ListViewItemSorter = listViewItemSorter;
                    }

                    if (!string.IsNullOrEmpty(selectedViewItemName))
                    {
                        var viewItem = this.FileView.Items[selectedViewItemName];

                        if (viewItem != null)
                        {
                            FileViewUtility.MoveTo(viewItem);
                        }
                    }
                }
                finally
                {
                    this.FileView.EndUpdate();
                }
            }
            finally
            {
                this.StatusStrip_UpdateMessage();
            }

            this.FileSystemWatcher = new FileSystemWatcher(this.CurrentPath)
            {
                InternalBufferSize = 32768,
            };

            this.FileSystemWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;

            this.FileSystemWatcher.Changed += FileSystemWatcher_OnChanged;
            this.FileSystemWatcher.Created += FileSystemWatcher_OnCreated;
            this.FileSystemWatcher.Deleted += FileSystemWatcher_OnDeleted;
            this.FileSystemWatcher.Renamed += FileSystemWatcher_OnRenamed;

            this.FileSystemWatcher.IncludeSubdirectories = false;

            this.FileSystemWatcher.EnableRaisingEvents = true;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            WindowsApi.SendMessage(this.FileView.Handle, WindowsApi.LVM_SETTEXTBKCOLOR, IntPtr.Zero, unchecked((IntPtr)(int)0xFFFFFF));

            try
            {
                this.MainWindow_GotoFolder(this.CurrentPath);
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void MainWindow_ReportError(string operation, Exception ex)
        {
            new ErrorWindow(this, operation, ex).ShowDialog();
        }

        private void MainWindow_ReportError(Exception ex)
        {
            new ErrorWindow(this, ex).ShowDialog();
        }

        private void MainWindow_RunAsyncOperation<T>(Action<T> action, T parameter, string description)
        {
            const int ProgressWindowWaitTime = 500;

            var controlThread = new Thread
            (
                new ThreadStart
                (
                    () =>
                    {
                        Interlocked.Increment(ref this.NumberOfRunningAsyncOperations);

                        Exception workerEx = null;

                        var workerThread = new Thread
                        (
                            new ThreadStart
                            (
                                () =>
                                {
                                    try
                                    {
                                        action(parameter);
                                    }
                                    catch (Exception ex)
                                    {
                                        workerEx = ex;
                                    }
                                }
                            )
                        )
                        {
                            IsBackground = true
                        };

                        workerThread.SetApartmentState(ApartmentState.STA);

                        workerThread.Start();

                        if (!workerThread.Join(ProgressWindowWaitTime))
                        {
                            var progressWindow = new ProgressWindow(this, description, workerThread);

                            this.Invoke
                            (
                                new Action
                                (
                                    () =>
                                    {
                                        progressWindow.Show();
                                    }
                                )
                            );

                            workerThread.Join();

                            this.Invoke
                            (
                                new Action
                                (
                                    () =>
                                    {
                                        progressWindow.Close();
                                    }
                                )
                            );

                            if (workerEx != null)
                            {
                                this.Invoke
                                (
                                    new Action
                                    (
                                        () =>
                                        {
                                            this.MainWindow_ReportError(description, workerEx);
                                        }
                                    )
                                );
                            }
                        }
                        else
                        {
                            if (workerEx != null)
                            {
                                this.Invoke
                                (
                                    new Action
                                    (
                                        () =>
                                        {
                                            this.MainWindow_ReportError(description, workerEx);
                                        }
                                    )
                                );
                            }
                        }

                        Interlocked.Decrement(ref this.NumberOfRunningAsyncOperations);
                    }
                )
            )
            {
                IsBackground = true
            };

            controlThread.Start();
        }

        private void StatusStrip_UpdateMessage(string message = null, bool forceRefresh = false)
        {
            this.StatusStripMessageLabel.Text = message ?? $"{this.FileView.Items.Count} items (ordered by {((IHasName)this.FileView.ListViewItemSorter).Name}), {this.FileView.SelectedItems.Count} selected.";

            if (forceRefresh)
            {
                this.StatusStrip.Refresh();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.FileSystemWatcherQueue.IsEmpty)
            {
                return;
            }

            try
            {
                this.FileView.BeginUpdate();

                try
                {
                    while (this.FileSystemWatcherQueue.TryDequeue(out var action))
                    {
                        try
                        {
                            action();
                        }
                        catch
                        {
                        }
                    }
                }
                finally
                {
                    this.FileView.EndUpdate();
                }
            }
            catch
            {
            }
            finally
            {
                this.StatusStrip_UpdateMessage();
            }
        }

        private void ToolsCommandPromptHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "Cmd.exe", WorkingDirectory = this.CurrentPath, UseShellExecute = false });
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void ToolsFileExplorerHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "Explorer.exe", Arguments = $"\"{this.CurrentPath}\"", WorkingDirectory = this.CurrentPath, UseShellExecute = false });
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void ToolsLightFileExplorerHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = Application.ExecutablePath, WorkingDirectory = this.CurrentPath, UseShellExecute = false });
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }

        private void ToolsPowerShellConsoleHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "PowerShell.exe", WorkingDirectory = this.CurrentPath, UseShellExecute = false });
            }
            catch (Exception ex)
            {
                this.MainWindow_ReportError(ex);
            }
        }
    }
}