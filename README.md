<h1 align="center">
  LightFileExplorer
</h1>

<h4 align="center">A lightweight application for moving through folders and working with files as fast as possible</h4>

<h4 align="center">
  <a href="#features">Features</a>&nbsp;|&nbsp;
  <a href="#download">Download</a>&nbsp;|&nbsp;
  <a href="#installation">Installation</a>&nbsp;|&nbsp;
  <a href="#configuration">Configuration</a>&nbsp;|&nbsp;
  <a href="#license">License</a>
</h4>

![Main screenshot](https://raw.githubusercontent.com/mayakron/lightfileexplorer/main/resources/LightFileExplorerMainScreenshot.png)

## Features

LightFileExplorer is a lightweight application designed to let you move through your folders and work with your files as fast as possible. It does not try to do other things like handling editing, archives, images, videos, synchronization, or encryption, as I believe it is best to leave these responsibilities to other more specialized software.

I know there are many other file explorers out there, but this is a project that I developed just for the fun of learning.

## Download

All versions of LightFileExplorer can be downloaded from [here](https://github.com/mayakron/lightfileexplorer/releases).

## Installation

LightFileExplorer is portable: just expand the archive to a directory of your choice and run the LightFileExplorer.exe file.

## Configuration

LightFileExplorer can be configured by editing its "LightFileExplorer.exe.config" file. This is an example of it:

```
<configuration>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8.1" />
  </startup>
  <appSettings>
    <add key="OpenWith" value="LibreOffice>..\LibreOffice-7.4.5.1\LibreOfficePortable.exe|Gimp>..\Gimp-2.10.32\GIMPPortable.exe" />
    <add key="TextViewer" value="..\NotepadPP-8.4.5\notepad++.exe" />
    <add key="BinaryViewer" value="..\HxD-2.0.0\HxD64.exe" />
    <add key="GotoFavorites" value="Windows Folder>C:\Windows|Windows Temp Folder>C:\Windows\Temp" />
  </appSettings>
</configuration>
```

## License

[GPLv3](https://www.gnu.org/licenses/gpl-3.0.en.html)