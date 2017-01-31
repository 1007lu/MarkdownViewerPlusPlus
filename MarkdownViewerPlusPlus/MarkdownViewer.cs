﻿using com.insanitydesign.MarkdownViewerPlusPlus.Forms;
using com.insanitydesign.MarkdownViewerPlusPlus.Properties;
using CommonMark;
using Kbg.NppPluginNET;
using Kbg.NppPluginNET.PluginInfrastructure;
using System;
using System.Runtime.InteropServices;
using static Kbg.NppPluginNET.PluginInfrastructure.Win32;

/// <summary>
/// 
/// </summary>
namespace com.insanitydesign.MarkdownViewerPlusPlus
{
    /// <summary>
    /// 
    /// </summary>
    public class MarkdownViewer
    {
        /// <summary>
        /// 
        /// </summary>
        public IScintillaGateway Editor { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public INotepadPPGateway Notepad { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        protected AbstractRenderer renderer;

        /// <summary>
        /// 
        /// </summary>
        protected bool rendererVisible = false;

        /// <summary>
        /// 
        /// </summary>
        protected string oldEditorText = "";

        /// <summary>
        /// 
        /// </summary>
        public int commandId = 0;

        /// <summary>
        /// 
        /// </summary>
        public int commandIdSynchronize = 2;

        /// <summary>
        /// 
        /// </summary>
        protected MarkdownViewerConfiguration configuration;

        /// <summary>
        /// 
        /// </summary>
        public MarkdownViewer()
        {
            //Get some global references to the editor and Notepad++ engines
            this.Editor = new ScintillaGateway(PluginBase.GetCurrentScintilla());
            this.Notepad = new NotepadPPGateway();
            //Init the actual renderer
            this.renderer = new MarkdownViewerRenderer(this);
            //Set our custom formatter
            CommonMarkSettings.Default.OutputDelegate = (doc, output, settings) => new MarkdownViewerFormatter(output, settings).WriteDocument(doc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        public void OnNotification(ScNotification notification)
        {
            //Listen to any UI update to get informed about all file changes, chars added/removed etc.
            if (this.renderer.Visible)
            {
                //TODO: Limit to certain events
                if (notification.Header.Code == (uint)SciMsg.SCN_UPDATEUI)
                {
                    UpdateMarkdownViewer();
                    //Update the scroll bar of the Viewer Panel only in case of vertical scrolls
                    if (this.configuration.SynchronizeScrolling && notification.Updated == (uint)SciMsg.SC_UPDATE_V_SCROLL)
                    {
                        UpdateScrollBar();
                    }
                }
                //else if (notification.Header.Code == (uint)SciMsg.SCN_MODIFIED)
                //{
                //    UpdateMarkdownViewer();
                //}
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void UpdateScrollBar()
        {
            ScrollInfo scrollInfo = this.Editor.GetScrollInfo(ScrollInfoMask.SIF_RANGE | ScrollInfoMask.SIF_TRACKPOS | ScrollInfoMask.SIF_PAGE, ScrollInfoBar.SB_VERT);
            var scrollRatio = (double)scrollInfo.nTrackPos / (scrollInfo.nMax - scrollInfo.nPage);
            this.renderer.ScrollByRatioVertically(scrollRatio);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CommandMenuInit()
        {
            this.configuration = new MarkdownViewerConfiguration();

            //Register our commands
            PluginBase.SetCommand(this.commandId, Main.PluginName, MarkdownViewerCommand, new ShortcutKey(true, false, true, System.Windows.Forms.Keys.M));
            //Separator
            PluginBase.SetCommand(this.commandId + 1, "---", null);
            //Synchronized scrolling
            PluginBase.SetCommand(this.commandIdSynchronize, "Synchronize scrolling (Editor -> Viewer)", SynchronizeScrollingCommand, this.configuration.SynchronizeScrolling);
        }

        /// <summary>
        /// Check/Uncheck the configuration item
        /// </summary>
        protected void SynchronizeScrollingCommand()
        {
            this.configuration.SynchronizeScrolling = !this.configuration.SynchronizeScrolling;
            Win32.CheckMenuItem(Win32.GetMenu(PluginBase.nppData._nppHandle), PluginBase._funcItems.Items[this.commandIdSynchronize]._cmdID, Win32.MF_BYCOMMAND | (this.configuration.SynchronizeScrolling ? Win32.MF_CHECKED : Win32.MF_UNCHECKED));
            if(this.configuration.SynchronizeScrolling)
            {
                UpdateScrollBar();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetToolBarIcon()
        {
            toolbarIcons toolbarIcons = new toolbarIcons();
            toolbarIcons.hToolbarBmp = Resources.markdown_16x16_solid.GetHbitmap();
            IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(toolbarIcons));
            Marshal.StructureToPtr(toolbarIcons, pTbIcons, false);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_ADDTOOLBARICON, PluginBase._funcItems.Items[this.commandId]._cmdID, pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);
        }

        /// <summary>
        /// 
        /// </summary>
        public void MarkdownViewerCommand()
        {
            //Show
            if (!this.renderer.Visible)
            {
                UpdateMarkdownViewer();
                UpdateScrollBar();
            }
            ToggleToolbarIcon(!this.renderer.Visible);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="show"></param>
        /// <returns></returns>
        public void ToggleToolbarIcon(bool show = true)
        {
            //To show or not to show
            NppMsg msg = show ? NppMsg.NPPM_DMMSHOW : NppMsg.NPPM_DMMHIDE;
            //
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)msg, 0, this.renderer.Handle);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_SETMENUITEMCHECK, PluginBase._funcItems.Items[this.commandId]._cmdID, show ? 1 : 0);
            //
            this.rendererVisible = this.renderer.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateMarkdownViewer()
        {
            try
            {
                string editorText = this.Editor.GetText(this.Editor.GetLength());
                if (editorText != this.oldEditorText)
                {
                    this.oldEditorText = editorText;
                    this.renderer.Render(CommonMarkConverter.Convert(editorText));
                }
            }
            catch
            {
                this.renderer.Render("Couldn't render the currently selected file!");
            }
        }

        /// <summary>
        /// Save the configuration
        /// </summary>
        public void PluginCleanUp()
        {
            this.configuration.Save();
        }
    }
}
