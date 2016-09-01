#region MIT License
/*Copyright (c) 2015-2016, Robert Rouhani <robert.rouhani@gmail.com>

SharpFont based on Tao.FreeType, Copyright (c) 2003-2007 Tao Framework Team

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/
#endregion

namespace Examples
{
	partial class ExampleForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.mainMenuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuFolderOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mainMenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuEditSharpFont = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuEditSystemDrawing = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mainMenuFontSize = new System.Windows.Forms.ToolStripComboBox();
			this.foregroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuView = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuViewDetails = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.listBoxFont = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBoxText = new System.Windows.Forms.PictureBox();
			this.openFontDialog = new System.Windows.Forms.OpenFileDialog();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.mainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxText)).BeginInit();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			//
			// mainMenu
			//
			this.mainMenu.Dock = System.Windows.Forms.DockStyle.None;
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mainMenuFile,
			this.mainMenuEdit,
			this.mainMenuView,
			this.mainMenuHelp});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(584, 24);
			this.mainMenu.TabIndex = 1;
			this.mainMenu.Text = "menuStrip1";
			//
			// mainMenuFile
			//
			this.mainMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mainMenuFileOpen,
			this.mainMenuFolderOpen,
			this.toolStripSeparator1,
			this.mainMenuFileExit});
			this.mainMenuFile.Name = "mainMenuFile";
			this.mainMenuFile.Size = new System.Drawing.Size(37, 20);
			this.mainMenuFile.Text = "File";
			//
			// mainMenuFileOpen
			//
			this.mainMenuFileOpen.Name = "mainMenuFileOpen";
			this.mainMenuFileOpen.Size = new System.Drawing.Size(160, 22);
			this.mainMenuFileOpen.Text = "Open Font File...";
			this.mainMenuFileOpen.Click += new System.EventHandler(this.mainMenuFileOpen_Click);
			//
			// mainMenuFolderOpen
			//
			this.mainMenuFolderOpen.Name = "mainMenuFolderOpen";
			this.mainMenuFolderOpen.Size = new System.Drawing.Size(160, 22);
			this.mainMenuFolderOpen.Text = "Open Folder...";
			this.mainMenuFolderOpen.Click += new System.EventHandler(this.mainMenuFolderOpen_Click);
			//
			// toolStripSeparator1
			//
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
			//
			// mainMenuFileExit
			//
			this.mainMenuFileExit.Name = "mainMenuFileExit";
			this.mainMenuFileExit.Size = new System.Drawing.Size(160, 22);
			this.mainMenuFileExit.Text = "Exit";
			this.mainMenuFileExit.Click += new System.EventHandler(this.mainMenuFileExit_Click);
			//
			// mainMenuEdit
			//
			this.mainMenuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mainMenuEditSharpFont,
			this.mainMenuEditSystemDrawing,
			this.toolStripSeparator2,
			this.mainMenuFontSize,
			this.foregroundColorToolStripMenuItem,
			this.backgroundColorToolStripMenuItem});
			this.mainMenuEdit.Name = "mainMenuEdit";
			this.mainMenuEdit.Size = new System.Drawing.Size(39, 20);
			this.mainMenuEdit.Text = "Edit";
			//
			// mainMenuEditSharpFont
			//
			this.mainMenuEditSharpFont.Checked = true;
			this.mainMenuEditSharpFont.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mainMenuEditSharpFont.Name = "mainMenuEditSharpFont";
			this.mainMenuEditSharpFont.Size = new System.Drawing.Size(181, 22);
			this.mainMenuEditSharpFont.Text = "Use SharpFont";
			this.mainMenuEditSharpFont.Click += new System.EventHandler(this.mainMenuEditSharpFont_Click);
			//
			// mainMenuEditSystemDrawing
			//
			this.mainMenuEditSystemDrawing.Enabled = false;
			this.mainMenuEditSystemDrawing.Name = "mainMenuEditSystemDrawing";
			this.mainMenuEditSystemDrawing.Size = new System.Drawing.Size(181, 22);
			this.mainMenuEditSystemDrawing.Text = "Use System.Drawing";
			this.mainMenuEditSystemDrawing.Click += new System.EventHandler(this.mainMenuEditSystemDrawing_Click);
			//
			// toolStripSeparator2
			//
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(178, 6);
			//
			// mainMenuFontSize
			//
			this.mainMenuFontSize.Name = "mainMenuFontSize";
			this.mainMenuFontSize.Size = new System.Drawing.Size(121, 23);
			this.mainMenuFontSize.Text = "Font Size";
			this.mainMenuFontSize.TextUpdate += new System.EventHandler(this.mainMenuFontSize_TextUpdate);
			//
			// foregroundColorToolStripMenuItem
			//
			this.foregroundColorToolStripMenuItem.Name = "foregroundColorToolStripMenuItem";
			this.foregroundColorToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.foregroundColorToolStripMenuItem.Text = "Foreground Color...";
			this.foregroundColorToolStripMenuItem.Click += new System.EventHandler(this.foregroundColorToolStripMenuItem_Click);
			//
			// backgroundColorToolStripMenuItem
			//
			this.backgroundColorToolStripMenuItem.Name = "backgroundColorToolStripMenuItem";
			this.backgroundColorToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.backgroundColorToolStripMenuItem.Text = "Background Color...";
			this.backgroundColorToolStripMenuItem.Click += new System.EventHandler(this.backgroundColorToolStripMenuItem_Click);
			//
			// mainMenuView
			//
			this.mainMenuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mainMenuViewDetails});
			this.mainMenuView.Name = "mainMenuView";
			this.mainMenuView.Size = new System.Drawing.Size(44, 20);
			this.mainMenuView.Text = "View";
			//
			// mainMenuViewDetails
			//
			this.mainMenuViewDetails.Name = "mainMenuViewDetails";
			this.mainMenuViewDetails.Size = new System.Drawing.Size(145, 22);
			this.mainMenuViewDetails.Text = "Font Details...";
			//
			// mainMenuHelp
			//
			this.mainMenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mainMenuHelpAbout});
			this.mainMenuHelp.Name = "mainMenuHelp";
			this.mainMenuHelp.Size = new System.Drawing.Size(44, 20);
			this.mainMenuHelp.Text = "Help";
			//
			// mainMenuHelpAbout
			//
			this.mainMenuHelpAbout.Name = "mainMenuHelpAbout";
			this.mainMenuHelpAbout.Size = new System.Drawing.Size(107, 22);
			this.mainMenuHelpAbout.Text = "About";
			//
			// splitContainer1
			//
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			//
			// splitContainer1.Panel1
			//
			this.splitContainer1.Panel1.Controls.Add(this.listBoxFont);
			//
			// splitContainer1.Panel2
			//
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Size = new System.Drawing.Size(584, 315);
			this.splitContainer1.SplitterDistance = 135;
			this.splitContainer1.TabIndex = 2;
			//
			// listBoxFont
			//
			this.listBoxFont.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxFont.FormattingEnabled = true;
			this.listBoxFont.Location = new System.Drawing.Point(0, 0);
			this.listBoxFont.Name = "listBoxFont";
			this.listBoxFont.Size = new System.Drawing.Size(135, 315);
			this.listBoxFont.TabIndex = 0;
			this.listBoxFont.SelectedIndexChanged += new System.EventHandler(this.listBoxFont_SelectedIndexChanged);
			//
			// panel1
			//
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.pictureBoxText);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(445, 315);
			this.panel1.TabIndex = 1;
			//
			// pictureBoxText
			//
			this.pictureBoxText.Location = new System.Drawing.Point(3, 3);
			this.pictureBoxText.Name = "pictureBoxText";
			this.pictureBoxText.Size = new System.Drawing.Size(369, 275);
			this.pictureBoxText.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxText.TabIndex = 0;
			this.pictureBoxText.TabStop = false;
			//
			// openFontDialog
			//
			this.openFontDialog.Filter = "All Font Files|*.ttf;*.otf|TrueType Fonts|*.ttf|OpenType Fonts|*.otf";
			//
			// toolStripContainer1
			//
			//
			// toolStripContainer1.BottomToolStripPanel
			//
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			//
			// toolStripContainer1.ContentPanel
			//
			this.toolStripContainer1.ContentPanel.AutoScroll = true;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(584, 315);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(584, 361);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			//
			// toolStripContainer1.TopToolStripPanel
			//
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.mainMenu);
			//
			// statusStrip1
			//
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.statusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(584, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			//
			// statusLabel
			//
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(66, 17);
			this.statusLabel.Text = "statusLabel";
			//
			// ExampleForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 361);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.mainMenu;
			this.Name = "ExampleForm";
			this.Text = "SharpFont Example";
			this.Load += new System.EventHandler(this.ExampleForm_Load);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxText)).EndInit();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem mainMenuFile;
		private System.Windows.Forms.ToolStripMenuItem mainMenuFileOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem mainMenuFileExit;
		private System.Windows.Forms.ToolStripMenuItem mainMenuView;
		private System.Windows.Forms.ToolStripMenuItem mainMenuHelp;
		private System.Windows.Forms.ToolStripMenuItem mainMenuHelpAbout;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStripMenuItem mainMenuViewDetails;
		private System.Windows.Forms.PictureBox pictureBoxText;
		private System.Windows.Forms.ToolStripMenuItem mainMenuEdit;
		private System.Windows.Forms.ToolStripMenuItem mainMenuEditSharpFont;
		private System.Windows.Forms.ToolStripMenuItem mainMenuEditSystemDrawing;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripComboBox mainMenuFontSize;
		private System.Windows.Forms.ToolStripMenuItem foregroundColorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem backgroundColorToolStripMenuItem;
		private System.Windows.Forms.ListBox listBoxFont;
		private System.Windows.Forms.OpenFileDialog openFontDialog;
		private System.Windows.Forms.ToolStripMenuItem mainMenuFolderOpen;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.Panel panel1;
	}
}
