<h1 align="center">
  LightFileExplorer
</h1>

<h4 align="center">A lightweight file explorer application designed for speed</h4>

<h4 align="center">
  <a href="#features">Features</a>&nbsp;|&nbsp;
  <a href="#download">Download</a>&nbsp;|&nbsp;
  <a href="#installation">Installation</a>&nbsp;|&nbsp;
  <a href="#configuration">Configuration</a>&nbsp;|&nbsp;
  <a href="#license">License</a>
</h4>

![Main screenshot](https://raw.githubusercontent.com/mayakron/lightfileexplorer/main/resources/LightFileExplorerMainScreenshot.png)

## Features

LightFileExplorer is a lightweight file explorer application designed for speed. It does not try to do other things like handling editing, archives, images, videos, synchronization, or encryption, as I believe it is best to leave these responsibilities to other more specialized software.

## Download

All versions of LightFileExplorer can be downloaded from [here](https://github.com/mayakron/lightfileexplorer/releases).

## Installation

LightFileExplorer is portable: just expand the archive and run "LightFileExplorer.exe".

## Configuration

LightFileExplorer can be configured by editing the "LightFileExplorer.exe.config" file. Here is an example of it:

```
<configuration>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8.1" />
  </startup>
  <appSettings>
    <add key="OpenWith" value="Notepad>Notepad.exe" />
    <add key="TextViewer" value="Notepad.exe" />
    <add key="BinaryViewer" value="" />
    <add key="GotoFavorites" value="Windows Folder>C:\Windows|System Folder>C:\Windows\System32" />
    <add key="CustomTools" value="&amp;Task Manager>TaskMgr.exe|&amp;Resource Monitor>ResMon.exe"/>
  </appSettings>
</configuration>
```

## License

[GPLv3](https://www.gnu.org/licenses/gpl-3.0.en.html)