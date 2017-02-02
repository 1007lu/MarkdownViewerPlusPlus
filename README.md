# MarkdownViewerPlusPlus [![Build status](https://ci.appveyor.com/api/projects/status/jkuuth039vioms74?svg=true)](https://ci.appveyor.com/project/nea/markdownviewerplusplus)
A Notepad++ Plugin to view a Markdown file rendered on-the-fly

## Features
* Dockable panel (toggle) with a rendered HTML of the currently selected file/tab
* CommonMark compliant ([0.27][4])
* Links open in a separate Browser
* Basic HTML Export
* Basic PDF Export
* Unicode Notepad++ 32-bit plugin

### Planned
* Highlight the current cursor row
* Export as PDF with properties/templates
* x64 support
* Improved performance with large documents
* Scroll to selected content
* BugFixes ^^'

## Latest Versions
* 0.4.1
  * Fixed an issue of double header/footer in the HTML export
* 0.4.0
  * Changed to NuGet dependencies merged via ILMerge
  * Added support for rendering SVG images
* 0.3.1
  * Added an "About" dialog
  
Download the latest [release here][9]. For a full version history go [here][10].

## Installation
Download a [release version][9] and copy the included **MarkdownViewerPlusPlus.dll** to the *plugins* sub-folder at your Notepad++ installation directory. The plugin adds a small Markdown icon ![Markdown icon](https://github.com/nea/MarkdownViewerPlusPlus/blob/master/MarkdownViewerPlusPlus/Resources/markdown-16x16-solid.png?raw=true) to the toolbar to toggle the viewer as dockable panel.

### Compatibility
This plugin requires at least
* Notepad++ 32-bit (won't work with 64-bit)
* Windows
* .NET Framework 4.0 or above

It has been tested under the following conditions
* Notepad++ 7.2.2 (32-bit)
* Windows 10 Professional (64-bit)

## Usage
To open the MarkdownViewer++ you can 
* click the toolbar icon ![Markdown icon](https://github.com/nea/MarkdownViewerPlusPlus/raw/master/MarkdownViewerPlusPlus/Resources/markdown-16x16-solid.png), 
* use the shortcut _Ctrl+Shift+M_
* or open it via the **Plugins** sub-menu

To synchronize the scrolling between the Notepad++ editor view and the rendered markdown, you can enable the option via the **Plugins** sub-menu. The made selection will be stored and loaded in future sessions.

![MarkdownViewer++](https://github.com/nea/MarkdownViewerPlusPlus/blob/master/MarkdownViewerPlusPlus.png?raw=true)

## License and Credits
The MarkdownViewerPlusPlus is released under the MIT license.

This Notepad++ plugin integrates the sources of multiple other libraries, because of issues with the library merging process. Credits and thanks to all the developers working on these great projects:
* The plugin is based on the [Notepad++ PluginPack.net][2] by kbilsted provided under the Apache-2.0 license.
* The renderer uses 
  * [CommonMark.NET][3] by Knagis provided under the BSD-3-Clause license
  * [HTMLRenderer.WinForms][6] by ArthurHub provided under the BSD-3-Clause license
* The PDF Exporter uses 
  * [PDFSharp][5] by empira Software GmbH provided under the MIT license
  * [HTMLRenderer.PdfSharp][6] by ArthurHub provided under the BSD-3-Clause license
* The SVG renderer uses [SVG.NET][11] by vvvv provided under the Microsoft Public License
* The menu icons are by [FontAwesome][7] provided under the SIL OFL 1.1 license
* The Markdown icon is by [dcurtis][8] provided under the CC0-1.0 license

## Disclaimer
This source and the whole package comes without warranty. It may or may not harm your computer or cell phone. Please use with care. Any damage cannot be related back to the author. The source has been tested on a virtual environment and scanned for viruses and has passed all tests.

## Personal Note
*I don't know if this is very useful for a lot of people but I wanted something in private to quickly write and see some formatted Markdown documents. As I was not able to find something similar very quickly I created this project. I hope this proves useful to you... with all its Bugs and Issues ;) If you like it you can give me a shout at [INsanityDesign][1] or let me know via this repository.*

  [1]: http://www.insanitydesign.com/wp/
  [2]: https://github.com/kbilsted/NotepadPlusPlusPluginPack.Net
  [3]: https://github.com/Knagis/CommonMark.NET
  [4]: http://spec.commonmark.org/
  [5]: http://www.pdfsharp.net/
  [6]: https://htmlrenderer.codeplex.com/
  [7]: http://fontawesome.io/
  [8]: https://github.com/dcurtis/markdown-mark
  [9]: https://github.com/nea/MarkdownViewerPlusPlus/releases
  [10]: https://github.com/nea/MarkdownViewerPlusPlus/wiki/Version-History
  [11]: https://github.com/vvvv/SVG