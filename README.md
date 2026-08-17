<h1 align="center">
  LightFileExplorer
</h1>

<h4 align="center">A lightweight file explorer application designed for speed</h4>

## Features

LightFileExplorer is a lightweight file explorer application designed for speed. It does not try to do other things like handling editing, archives, images, videos, synchronization, or encryption, as I believe it is best to leave these responsibilities to other more specialized software.

![Main screenshot](https://raw.githubusercontent.com/mayakron/lightfileexplorer/main/resources/LightFileExplorerMainScreenshot.png)

## Download

All versions of LightFileExplorer can be downloaded from [here](https://github.com/mayakron/lightfileexplorer/releases).

## Installation

LightFileExplorer is portable: just expand the archive and run "LightFileExplorer.exe".

## Configuration

Configuration options, available in the "LightFileExplorer.exe.config" file, are:

- **FileSystemWatcherTimerInterval**: the interval the FileSystemWatcher timer uses to periodically update the file list.
- **ProgressWindowWaitTime**: how long to wait, when performing an operation, before a progress window is shown.
- **FileIcons**: associations between file icons and file extensions.
- **OpenWith**: set of favorite programs available in the "Open With" menu.
- **TextViewer**: path to the default text viewer.
- **BinaryViewer**: path to the default binary viewer.
- **GotoFavorites**: set of favorite directories available in the "Goto" menu.
- **CustomTools**: set of predefined tools available in the "Tools" menu.

## License

[GPLv3](https://www.gnu.org/licenses/gpl-3.0.en.html)