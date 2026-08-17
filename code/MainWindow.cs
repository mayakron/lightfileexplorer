using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal partial class MainWindow : Form
    {
        private readonly ConcurrentDictionary<string, byte> FileSystemWatcherDictionary = new ConcurrentDictionary<string, byte>();

        private readonly FileViewColumnAttributesSorter FileViewColumnAttributesSorter = new FileViewColumnAttributesSorter();

        private readonly FileViewColumnExtensionSorter FileViewColumnExtensionSorter = new FileViewColumnExtensionSorter();

        private readonly FileViewColumnDateModifiedSorter FileViewColumnDateModifiedSorter = new FileViewColumnDateModifiedSorter();

        private readonly FileViewColumnNameSorter FileViewColumnNameSorter = new FileViewColumnNameSorter();

        private readonly FileViewColumnSizeSorter FileViewColumnSizeSorter = new FileViewColumnSizeSorter();

        private string CurrentPath;

        private FileSystemWatcher FileSystemWatcher = null;

        private Regex FindRegex;

        private int NumberOfRunningAsyncOperations = 0;

        public MainWindow()
        {
            var keysConverter = new KeysConverter();

            ToolStripMenuItem CreateFilesOpenWithMenuItem(string text, string path, string shortcutKeys)
            {
                var menuItem = new ToolStripMenuItem();

                menuItem.Text = text;

                if (!string.IsNullOrEmpty(shortcutKeys) && !shortcutKeys.Equals("-"))
                {
                    menuItem.ShortcutKeys = (Keys)keysConverter.ConvertFromString(shortcutKeys);
                }

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
                        this.MainWindowReportError(ex);
                    }
                };

                return menuItem;
            }

            ToolStripMenuItem CreateGotoKnownFolderMenuItem(string text, string path, string shortcutKeys)
            {
                var menuItem = new ToolStripMenuItem();

                menuItem.Text = text;

                if (!string.IsNullOrEmpty(shortcutKeys) && !shortcutKeys.Equals("-"))
                {
                    menuItem.ShortcutKeys = (Keys)keysConverter.ConvertFromString(shortcutKeys);
                }

                menuItem.Click += (sender, e) =>
                {
                    try
                    {
                        this.MainWindowGotoFolder(path);
                    }
                    catch (Exception ex)
                    {
                        this.MainWindowReportError(ex);
                    }
                };

                return menuItem;
            }

            ToolStripMenuItem CreateToolsCustomToolMenuItem(string text, string path, string shortcutKeys)
            {
                var menuItem = new ToolStripMenuItem();

                menuItem.Text = text;

                if (!string.IsNullOrEmpty(shortcutKeys) && !shortcutKeys.Equals("-"))
                {
                    menuItem.ShortcutKeys = (Keys)keysConverter.ConvertFromString(shortcutKeys);
                }

                menuItem.Click += (sender, e) =>
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo { FileName = path, WorkingDirectory = this.CurrentPath, UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        this.MainWindowReportError(ex);
                    }
                };

                return menuItem;
            }

            InitializeComponent();

            this.Icon = IconUtility.GetExeIcon();

            this.CurrentPath = Environment.CurrentDirectory;

            this.FileView.ListViewItemSorter = this.FileViewColumnNameSorter;

            if ((ConfigurationUtility.OpenWith != null) && (ConfigurationUtility.OpenWith.Count > 0))
            {
                var filesOpenWithMenuItem = new ToolStripMenuItem();

                filesOpenWithMenuItem.Text = "Open &With...";

                for (int index = 0; index < ConfigurationUtility.OpenWith.Count; index++)
                {
                    var item = ConfigurationUtility.OpenWith[index];

                    filesOpenWithMenuItem.DropDownItems.Add(CreateFilesOpenWithMenuItem(item.Item1, item.Item2, item.Item3));
                }

                this.FilesToolStripMenuItem.DropDownItems.Insert(3, filesOpenWithMenuItem);
            }

            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"A:\", @"A:\", "Ctrl+Alt+A"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"B:\", @"B:\", "Ctrl+Alt+B"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"C:\", @"C:\", "Ctrl+Alt+C"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"D:\", @"D:\", "Ctrl+Alt+D"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"E:\", @"E:\", "Ctrl+Alt+E"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"F:\", @"F:\", "Ctrl+Alt+F"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"G:\", @"G:\", "Ctrl+Alt+G"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"H:\", @"H:\", "Ctrl+Alt+H"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"I:\", @"I:\", "Ctrl+Alt+I"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"J:\", @"J:\", "Ctrl+Alt+J"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"K:\", @"K:\", "Ctrl+Alt+K"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"L:\", @"L:\", "Ctrl+Alt+L"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"M:\", @"M:\", "Ctrl+Alt+M"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"N:\", @"N:\", "Ctrl+Alt+N"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"O:\", @"O:\", "Ctrl+Alt+O"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"P:\", @"P:\", "Ctrl+Alt+P"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"Q:\", @"Q:\", "Ctrl+Alt+Q"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"R:\", @"R:\", "Ctrl+Alt+R"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"S:\", @"S:\", "Ctrl+Alt+S"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"T:\", @"T:\", "Ctrl+Alt+T"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"U:\", @"U:\", "Ctrl+Alt+U"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"V:\", @"V:\", "Ctrl+Alt+V"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"W:\", @"W:\", "Ctrl+Alt+W"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"X:\", @"X:\", "Ctrl+Alt+X"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"Y:\", @"Y:\", "Ctrl+Alt+Y"));
            this.GotoLogicalDriveToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(@"Z:\", @"Z:\", "Ctrl+Alt+Z"));

            if ((ConfigurationUtility.GotoFavorites != null) && (ConfigurationUtility.GotoFavorites.Count > 0))
            {
                this.GotoToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

                for (int index = 0; index < ConfigurationUtility.GotoFavorites.Count; index++)
                {
                    var item = ConfigurationUtility.GotoFavorites[index];

                    this.GotoToolStripMenuItem.DropDownItems.Add(CreateGotoKnownFolderMenuItem(item.Item1, item.Item2, item.Item3));
                }
            }

            if ((ConfigurationUtility.CustomTools != null) && (ConfigurationUtility.CustomTools.Count > 0))
            {
                this.ToolsToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

                ToolStripMenuItem parentMenuItem = null;

                for (int index = 0; index < ConfigurationUtility.CustomTools.Count; index++)
                {
                    var item = ConfigurationUtility.CustomTools[index];

                    var splitMatch = Regex.Match(item.Item1, "^([^:]+):(.*)$", RegexOptions.None);

                    if (splitMatch.Success)
                    {
                        var part1 = splitMatch.Groups[1].Value;
                        var part2 = splitMatch.Groups[2].Value;

                        if ((parentMenuItem != null) && (parentMenuItem.Text.Equals(part1)))
                        {
                            parentMenuItem.DropDownItems.Add(CreateToolsCustomToolMenuItem(part2, item.Item2, item.Item3));
                        }
                        else
                        {
                            parentMenuItem = new ToolStripMenuItem();

                            parentMenuItem.Text = part1;

                            parentMenuItem.DropDownItems.Add(CreateToolsCustomToolMenuItem(part2, item.Item2, item.Item3));

                            this.ToolsToolStripMenuItem.DropDownItems.Add(parentMenuItem);
                        }
                    }
                    else
                    {
                        parentMenuItem = null;

                        this.ToolsToolStripMenuItem.DropDownItems.Add(CreateToolsCustomToolMenuItem(item.Item1, item.Item2, item.Item3));
                    }
                }
            }
        }

        private void FileSystemWatcherTimerTick(object sender, EventArgs e)
        {
            if (!(this.FileSystemWatcherDictionary.Count > 0))
            {
                return;
            }

            try
            {
                try
                {
                    this.FileView.BeginUpdate();

                    try
                    {
                        foreach (var item in this.FileSystemWatcherDictionary)
                        {
                            var key = item.Key;

                            if (this.FileSystemWatcherDictionary.TryRemove(key, out byte _))
                            {
                                var viewItem = this.FileView.FindItemByKey(key);

                                if (viewItem != null)
                                {
                                    if
                                    (
                                        !FileUtility.ScanSingleItem
                                        (
                                            key,
                                            (name, lastWriteTime, attributes) => { FileViewUtility.SetFolder(viewItem, name, lastWriteTime, attributes); },
                                            (name, size, lastWriteTime, attributes) => { FileViewUtility.SetFile(viewItem, name, size, lastWriteTime, attributes); }
                                        )
                                    )
                                    {
                                        this.FileView.RemoveItemByKey(key);
                                    }
                                }
                                else
                                {
                                    if
                                    (
                                        !FileUtility.ScanSingleItem
                                        (
                                            key,
                                            (name, lastWriteTime, attributes) => { this.FileView.AddItem(FileViewUtility.BuildFolder(name, lastWriteTime, attributes)); },
                                            (name, size, lastWriteTime, attributes) => { this.FileView.AddItem(FileViewUtility.BuildFile(name, size, lastWriteTime, attributes)); }
                                        )
                                    )
                                    {
                                        // Forget the item.
                                    }
                                }
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
                    this.StatusStripUpdateMessage();
                }
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void FilesAdvancedSelectionToolStripMenuItemClick(object sender, EventArgs e)
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesCopyNameToolStripMenuItemClick(object sender, EventArgs e)
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesCopyPathToolStripMenuItemClick(object sender, EventArgs e)
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesCopyToolStripMenuItemClick(object sender, EventArgs e)
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesCopyToToolStripMenuItemClick(object sender, EventArgs e)
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

                            this.MainWindowRunAsyncOperation
                            (
                                (asyncOperationParameter) =>
                                {
                                    var exceptions = new List<Exception>();

                                    foreach (var item in asyncOperationParameter)
                                    {
                                        try
                                        {
                                            FileUtility.CopyPath(item.Item1, item.Item2, FileUtility.GetAttributes(item.Item1));
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesCutToolStripMenuItemClick(object sender, EventArgs e)
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesDeleteToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    this.MainWindowRunAsyncOperation
                    (
                        (asyncOperationParameter) =>
                        {
                            var exceptions = new List<Exception>();

                            foreach (var item in asyncOperationParameter)
                            {
                                try
                                {
                                    FileUtility.DeletePath(item, FileUtility.GetAttributes(item));
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FilesFindNextToolStripMenuItemClick(object sender, EventArgs e)
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesFindPreviousToolStripMenuItemClick(object sender, EventArgs e)
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesFindToolStripMenuItemClick(object sender, EventArgs e)
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesInvertSelectionToolStripMenuItemClick(object sender, EventArgs e)
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

        private void FilesMoveToToolStripMenuItemClick(object sender, EventArgs e)
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

                            this.MainWindowRunAsyncOperation
                            (
                                (asyncOperationParameter) =>
                                {
                                    var exceptions = new List<Exception>();

                                    foreach (var item in asyncOperationParameter)
                                    {
                                        try
                                        {
                                            FileUtility.MovePath(item.Item1, item.Item2);
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesNewFolderToolStripMenuItemClick(object sender, EventArgs e)
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

                            FileViewUtility.MoveTo(this.FileView.AddItem(FileViewUtility.BuildFolder(folderName, DateTime.Now, FileAttributes.Normal)));
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesNewShortcutToolStripMenuItemClick(object sender, EventArgs e)
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

                            FileViewUtility.MoveTo(this.FileView.AddItem(FileViewUtility.BuildFile(shortcutName, 0, DateTime.Now, FileAttributes.Normal)));
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesOpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    if (this.FileView.SelectedItems.Count > 1)
                    {
                        foreach (ListViewItem viewItem in this.FileView.SelectedItems)
                        {
                            // If more than one item is selected, directories get silently ignored. Is this the best way to handle the situation?

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
                            this.MainWindowGotoFolder(Path.Combine(this.CurrentPath, viewItem.Name));
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesPasteToolStripMenuItemClick(object sender, EventArgs e)
        {
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
                            if (fileDropEffectData is MemoryStream fileDropEffectStream)
                            {
                                var fileDropEffectDataBytes = fileDropEffectStream.ToArray();

                                if ((fileDropEffectDataBytes.Length >= 4) && (fileDropEffectDataBytes[0] == 2) && (fileDropEffectDataBytes[1] == 0) && (fileDropEffectDataBytes[2] == 0) && (fileDropEffectDataBytes[3] == 0))
                                {
                                    fileDropEffect = 2;
                                }
                            }
                        }

                        switch (fileDropEffect)
                        {
                            case 1: // Copy.

                                this.MainWindowRunAsyncOperation
                                (
                                    (asyncOperationParameter) =>
                                    {
                                        var exceptions = new List<Exception>();

                                        foreach (var item in asyncOperationParameter)
                                        {
                                            try
                                            {
                                                FileUtility.CopyPath(item.Item1, item.Item2, FileUtility.GetAttributes(item.Item1));
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

                            case 2: // Move.

                                this.MainWindowRunAsyncOperation
                                (
                                    (asyncOperationParameter) =>
                                    {
                                        var exceptions = new List<Exception>();

                                        foreach (var item in asyncOperationParameter)
                                        {
                                            try
                                            {
                                                FileUtility.MovePath(item.Item1, item.Item2);
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
                    else
                    {
                        throw new Exception($"I don't know how to handle these Clipboard data formats: {string.Join(", ", dataObject.GetFormats(false))}.");
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void FilesRefreshToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                this.MainWindowGotoFolder(this.CurrentPath, false, true);
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void FilesRenameToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    if (this.FileView.SelectedItems.Count > 1)
                    {
                        throw new Exception($"Performing this operation on more than one item is not supported.");
                    }
                    else
                    {
                        var viewItem = this.FileView.SelectedItems[0];

                        var inputWindow = new InputTextWindow("Rename", "New &Name:", viewItem.Name, -1, FileViewUtility.IsFile(viewItem) ? viewItem.Name.IndexOf('.') : -1);

                        if (inputWindow.ShowDialog() == DialogResult.OK)
                        {
                            if (!string.IsNullOrEmpty(inputWindow.Input1) && !inputWindow.Input1.Equals(viewItem.Name))
                            {
                                var newName = inputWindow.Input1;

                                FileUtility.ValidateName(newName);

                                // Not checking if the new name already exists to allow for changes only in casing.

                                var previousName = viewItem.Name;

                                FileUtility.RenamePath(viewItem.Name, newName);

                                FileViewUtility.Rename(viewItem, newName);

                                this.FileView.RenameItemKey(previousName, viewItem);
                            }
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesSelectAllToolStripMenuItemClick(object sender, EventArgs e)
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

        private void FilesViewAsBinaryToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    if (string.IsNullOrEmpty(ConfigurationUtility.BinaryViewer))
                    {
                        throw new Exception("No binary viewer has been specified in the application's configuration.");
                    }

                    foreach (ListViewItem viewItem in this.FileView.SelectedItems)
                    {
                        if (!FileViewUtility.IsDirectory(viewItem))
                        {
                            Process.Start(new ProcessStartInfo { FileName = ConfigurationUtility.BinaryViewer, Arguments = $"\"{Path.Combine(this.CurrentPath, viewItem.Name)}\"", WorkingDirectory = this.CurrentPath, UseShellExecute = false });
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
                this.MainWindowReportError(ex);
            }
        }

        private void FilesViewAsTextToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (this.FileView.SelectedItems.Count > 0)
                {
                    if (string.IsNullOrEmpty(ConfigurationUtility.TextViewer))
                    {
                        throw new Exception("No text viewer has been specified in the application's configuration.");
                    }

                    foreach (ListViewItem viewItem in this.FileView.SelectedItems)
                    {
                        if (!FileViewUtility.IsDirectory(viewItem))
                        {
                            Process.Start(new ProcessStartInfo { FileName = ConfigurationUtility.TextViewer, Arguments = $"\"{Path.Combine(this.CurrentPath, viewItem.Name)}\"", WorkingDirectory = this.CurrentPath, UseShellExecute = false });
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
                this.MainWindowReportError(ex);
            }
        }

        private void FileSystemWatcherOnChanged(object source, FileSystemEventArgs e)
        {
#if DEBUG
            Debug.Print($"FSW Change: Name \"{e.Name}\"");
#endif

            FileSystemWatcherDictionary.TryAdd(e.Name, 1);
        }

        private void FileSystemWatcherOnCreated(object source, FileSystemEventArgs e)
        {
#if DEBUG
            Debug.Print($"FSW Create: Name \"{e.Name}\"");
#endif

            FileSystemWatcherDictionary.TryAdd(e.Name, 1);
        }

        private void FileSystemWatcherOnDeleted(object source, FileSystemEventArgs e)
        {
#if DEBUG
            Debug.Print($"FSW Delete: Name \"{e.Name}\"");
#endif

            FileSystemWatcherDictionary.TryAdd(e.Name, 1);
        }

        private void FileSystemWatcherOnRenamed(object source, RenamedEventArgs e)
        {
#if DEBUG
            Debug.Print($"FSW Rename: OldName \"{e.OldName}\" NewName \"{e.Name}\"");
#endif

            FileSystemWatcherDictionary.TryAdd(e.OldName, 1);
            FileSystemWatcherDictionary.TryAdd(e.Name, 1);
        }

        private void FileViewColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                switch (e.Column)
                {
                    case 0:

                        this.ViewSortNameToolStripMenuItemClick(sender, e);

                        break;

                    case 1:

                        this.ViewSortExtensionToolStripMenuItemClick(sender, e);

                        break;

                    case 2:

                        this.ViewSortSizeToolStripMenuItemClick(sender, e);

                        break;

                    case 3:

                        this.ViewSortDateModifiedToolStripMenuItemClick(sender, e);

                        break;

                    case 4:

                        this.ViewSortAttributesToolStripMenuItemClick(sender, e);

                        break;
                }
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void FileViewDragDrop(object sender, DragEventArgs e)
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

                                this.MainWindowRunAsyncOperation
                                (
                                    (asyncOperationParameter) =>
                                    {
                                        var exceptions = new List<Exception>();

                                        foreach (var item in asyncOperationParameter)
                                        {
                                            try
                                            {
                                                FileUtility.CopyPath(item.Item1, item.Item2, FileUtility.GetAttributes(item.Item1));
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

                                this.MainWindowRunAsyncOperation
                                (
                                    (asyncOperationParameter) =>
                                    {
                                        var exceptions = new List<Exception>();

                                        foreach (var item in asyncOperationParameter)
                                        {
                                            try
                                            {
                                                FileUtility.MovePath(item.Item1, item.Item2);
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
                this.MainWindowReportError(ex);
            }
        }

        private void FileViewDragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? ((e.KeyState & 4) != 0 ? DragDropEffects.Move : DragDropEffects.Copy) & e.AllowedEffect : DragDropEffects.None;
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void FileViewItemDrag(object sender, ItemDragEventArgs e)
        {
            try
            {
                this.DoDragDrop(new DataObject(DataFormats.FileDrop, this.FileView.SelectedItems.Cast<ListViewItem>().Select(x => Path.Combine(this.CurrentPath, x.Name)).ToArray()), DragDropEffects.Copy | DragDropEffects.Move);
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void FileViewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Apps:

                    if (this.FileView.SelectedItems.Count > 0)
                    {
                        var firstItemPosition = this.FileView.SelectedItems[0].Position;

                        new ShellContextMenu().ShowContextMenu(this.CurrentPath, this.FileView.SelectedItems.Cast<ListViewItem>().Select(x => x.Name).ToList(), new Point(this.Location.X + Math.Max(firstItemPosition.X, 4) + 64, this.Location.Y + Math.Max(firstItemPosition.Y, 24) + 64));
                    }

                    e.SuppressKeyPress = true;

                    e.Handled = true;

                    break;

                case Keys.Back:
                case Keys.Left:

                    this.GotoParentFolderToolStripMenuItemClick(sender, null);

                    e.SuppressKeyPress = true;

                    e.Handled = true;

                    break;

                case Keys.Enter:
                case Keys.Right:

                    this.FilesOpenToolStripMenuItemClick(sender, null);

                    e.SuppressKeyPress = true;

                    e.Handled = true;

                    break;
            }
        }

        private void FileViewMouseClick(object sender, MouseEventArgs e)
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
                this.MainWindowReportError(ex);
            }
        }

        private void FileViewMouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:

                        this.FilesOpenToolStripMenuItemClick(sender, null);

                        break;
                }
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void FileViewSelectedIndexChanged(object sender, EventArgs e)
        {
            this.StatusStripUpdateMessage();
        }

        private void GotoCustomFolderToolStripMenuItemClick(object sender, EventArgs e)
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

                        this.MainWindowGotoFolder(gotoWindow.SelectedPath);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void GotoDownloadsFolderToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                this.MainWindowGotoFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"));
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void GotoParentFolderToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (!string.Equals(Path.GetPathRoot(this.CurrentPath).TrimEnd(Path.DirectorySeparatorChar), this.CurrentPath.TrimEnd(Path.DirectorySeparatorChar), StringComparison.OrdinalIgnoreCase))
                {
                    this.MainWindowGotoFolder(Path.GetDirectoryName(this.CurrentPath), true);
                }
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void GotoUserFolderToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                this.MainWindowGotoFolder(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void GotoWindowsFolderToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                this.MainWindowGotoFolder(Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void HelpAboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show($"A file explorer application designed for speed. (v. {Application.ProductVersion})", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HelpProjectPageToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/mayakron/lightfileexplorer");
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void MainWindowFormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.FileSystemWatcher != null)
            {
                this.FileSystemWatcher.EnableRaisingEvents = false;

                this.FileSystemWatcher.Changed -= FileSystemWatcherOnChanged;
                this.FileSystemWatcher.Created -= FileSystemWatcherOnCreated;
                this.FileSystemWatcher.Deleted -= FileSystemWatcherOnDeleted;
                this.FileSystemWatcher.Renamed -= FileSystemWatcherOnRenamed;

                this.FileSystemWatcher.Dispose();

                this.FileSystemWatcher = null;

                this.FileSystemWatcherDictionary.Clear();
            }
        }

        private void MainWindowFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.NumberOfRunningAsyncOperations > 0)
            {
                if (MessageBox.Show($"An operation is still in progress.{Environment.NewLine}Are you sure that you want to quit?", "Exiting... - LFE", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void MainWindowGotoFolder(string path, bool goingToParentFolder = false, bool refreshingCurrentFolder = false)
        {
            if (this.FileSystemWatcher != null)
            {
                this.FileSystemWatcher.EnableRaisingEvents = false;

                this.FileSystemWatcher.Changed -= FileSystemWatcherOnChanged;
                this.FileSystemWatcher.Created -= FileSystemWatcherOnCreated;
                this.FileSystemWatcher.Deleted -= FileSystemWatcherOnDeleted;
                this.FileSystemWatcher.Renamed -= FileSystemWatcherOnRenamed;

                this.FileSystemWatcher.Dispose();

                this.FileSystemWatcher = null;

                this.FileSystemWatcherDictionary.Clear();
            }

            string selectedViewItemName = null;

            if (!string.Equals(this.CurrentPath, path, StringComparison.OrdinalIgnoreCase))
            {
                if (goingToParentFolder)
                {
                    selectedViewItemName = Path.GetFileName(this.CurrentPath);
                }

                Environment.CurrentDirectory = path;

                this.CurrentPath = path;
            }
            else
            {
                if (refreshingCurrentFolder)
                {
                    // With one selected item we keep the selection, while with more than one we loose it.
                    // Also, we do not try to keep the top item in place due to the complexity of that.
                    // This is consistent with how Windows Explorer behaves.

                    if (this.FileView.SelectedItems.Count == 1)
                    {
                        var viewItem = this.FileView.SelectedItems[0];

                        selectedViewItemName = viewItem.Name;
                    }
                }
            }

            this.Text = $"{this.CurrentPath} - LFE";

            this.StatusStripUpdateMessage("Getting folder content, please wait...", true);

            try
            {
                this.FileView.BeginUpdate();

                try
                {
                    var listViewItemSorter = this.FileView.ListViewItemSorter;

                    this.FileView.ListViewItemSorter = null;

                    try
                    {
                        this.FileView.ClearItems();

                        FileUtility.ScanMultipleItems
                        (
                            this.CurrentPath,
                            (name, lastWriteTime, attributes) => { this.FileView.AddItem(FileViewUtility.BuildFolder(name, lastWriteTime, attributes)); },
                            (name, size, lastWriteTime, attributes) => { this.FileView.AddItem(FileViewUtility.BuildFile(name, size, lastWriteTime, attributes)); }
                        );
                    }
                    finally
                    {
                        this.FileView.ListViewItemSorter = listViewItemSorter;
                    }

                    if (!string.IsNullOrEmpty(selectedViewItemName))
                    {
                        var viewItem = this.FileView.FindItemByKey(selectedViewItemName);

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
                this.StatusStripUpdateMessage();
            }

            this.FileSystemWatcher = new FileSystemWatcher(this.CurrentPath)
            {
                InternalBufferSize = 65536
            };

            this.FileSystemWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;

            this.FileSystemWatcher.Changed += FileSystemWatcherOnChanged;
            this.FileSystemWatcher.Created += FileSystemWatcherOnCreated;
            this.FileSystemWatcher.Deleted += FileSystemWatcherOnDeleted;
            this.FileSystemWatcher.Renamed += FileSystemWatcherOnRenamed;

            this.FileSystemWatcher.IncludeSubdirectories = false;

            this.FileSystemWatcher.EnableRaisingEvents = true;
        }

        private void MainWindowLoad(object sender, EventArgs e)
        {
            // Since we are intercepting the Windows Forms draw handler so that it never runs, we need to use the Windows API here.
            WindowsApi.SendMessage(this.FileView.Handle, WindowsApi.LVM_SETTEXTBKCOLOR, IntPtr.Zero, unchecked((IntPtr)(int)0xFFFFFF));

            try
            {
                this.MainWindowGotoFolder(this.CurrentPath);
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void MainWindowReportError(string operation, Exception ex)
        {
            new ErrorWindow(this, operation, ex).ShowDialog();
        }

        private void MainWindowReportError(Exception ex)
        {
            new ErrorWindow(this, ex).ShowDialog();
        }

        private void MainWindowRunAsyncOperation<T>(Action<T> action, T parameter, string description)
        {
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

                        if (!workerThread.Join(ConfigurationUtility.ProgressWindowWaitTime))
                        {
                            ProgressWindow progressWindow = null;

                            this.Invoke
                            (
                                new Action
                                (
                                    () =>
                                    {
                                        progressWindow = new ProgressWindow(this, description, workerThread);

                                        progressWindow.Show(this);
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
                                        try
                                        {
                                            progressWindow.Close();
                                        }
                                        catch
                                        {
                                        }
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
                                            this.MainWindowReportError(description, workerEx);
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
                                            this.MainWindowReportError(description, workerEx);
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

        private void StatusStripUpdateMessage(string message = null, bool forceRefresh = false)
        {
            this.StatusStripMessageLabel.Text = message ?? $"{this.FileView.Items.Count} items{((this.FileView.ListViewItemSorter != null) ? $" ordered by {((IHasName)this.FileView.ListViewItemSorter).Name}" : string.Empty)}, {this.FileView.SelectedItems.Count} selected.";

            if (forceRefresh)
            {
                this.StatusStrip.Refresh();
            }
        }

        private void ToolsCommandPromptToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "Cmd.exe", WorkingDirectory = this.CurrentPath, UseShellExecute = false });
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void ToolsFileExplorerToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "Explorer.exe", Arguments = $"\"{this.CurrentPath}\"", WorkingDirectory = this.CurrentPath, UseShellExecute = false });
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void ToolsLightFileExplorerToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = Application.ExecutablePath, WorkingDirectory = this.CurrentPath, UseShellExecute = false });
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void ToolsPowerShellConsoleToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "PowerShell.exe", WorkingDirectory = this.CurrentPath, UseShellExecute = false });
            }
            catch (Exception ex)
            {
                this.MainWindowReportError(ex);
            }
        }

        private void ViewSortAttributesToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.FileView.ListViewItemSorter = this.FileViewColumnAttributesSorter;

            FileViewUtility.SetViewSortIndication(this.FileView, 4);

            this.StatusStripUpdateMessage();
        }

        private void ViewSortExtensionToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.FileView.ListViewItemSorter = this.FileViewColumnExtensionSorter;

            FileViewUtility.SetViewSortIndication(this.FileView, 1);

            this.StatusStripUpdateMessage();
        }

        private void ViewSortDateModifiedToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.FileView.ListViewItemSorter = this.FileViewColumnDateModifiedSorter;

            FileViewUtility.SetViewSortIndication(this.FileView, 3);

            this.StatusStripUpdateMessage();
        }

        private void ViewSortNameToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.FileView.ListViewItemSorter = this.FileViewColumnNameSorter;

            FileViewUtility.SetViewSortIndication(this.FileView, 0);

            this.StatusStripUpdateMessage();
        }

        private void ViewSortSizeToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.FileView.ListViewItemSorter = this.FileViewColumnSizeSorter;

            FileViewUtility.SetViewSortIndication(this.FileView, 2);

            this.StatusStripUpdateMessage();
        }
    }
}