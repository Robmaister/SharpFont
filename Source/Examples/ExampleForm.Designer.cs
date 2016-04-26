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
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mainMenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuEditSharpFont = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuEditSystemDrawing = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
			this.foregroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuView = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuViewDetails = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.pictureBoxText = new System.Windows.Forms.PictureBox();
			this.listBoxFont = new System.Windows.Forms.ListBox();
			this.openFontDialog = new System.Windows.Forms.OpenFileDialog();
			this.mainMenu.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxText)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuFile,
            this.mainMenuEdit,
            this.mainMenuView,
            this.mainMenuHelp});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(506, 24);
			this.mainMenu.TabIndex = 1;
			this.mainMenu.Text = "menuStrip1";
			// 
			// mainMenuFile
			// 
			this.mainMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuFileOpen,
            this.toolStripSeparator1,
            this.mainMenuFileExit});
			this.mainMenuFile.Name = "mainMenuFile";
			this.mainMenuFile.Size = new System.Drawing.Size(37, 20);
			this.mainMenuFile.Text = "File";
			// 
			// mainMenuFileOpen
			// 
			this.mainMenuFileOpen.Name = "mainMenuFileOpen";
			this.mainMenuFileOpen.Size = new System.Drawing.Size(152, 22);
			this.mainMenuFileOpen.Text = "Open...";
			this.mainMenuFileOpen.Click += new System.EventHandler(this.mainMenuFileOpen_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// mainMenuFileExit
			// 
			this.mainMenuFileExit.Name = "mainMenuFileExit";
			this.mainMenuFileExit.Size = new System.Drawing.Size(152, 22);
			this.mainMenuFileExit.Text = "Exit";
			this.mainMenuFileExit.Click += new System.EventHandler(this.mainMenuFileExit_Click);
			// 
			// mainMenuEdit
			// 
			this.mainMenuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuEditSharpFont,
            this.mainMenuEditSystemDrawing,
            this.toolStripSeparator2,
            this.toolStripComboBox1,
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
			// toolStripComboBox1
			// 
			this.toolStripComboBox1.Name = "toolStripComboBox1";
			this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
			this.toolStripComboBox1.Text = "Font Size";
			this.toolStripComboBox1.TextUpdate += new System.EventHandler(this.toolStripComboBox1_TextUpdate);
			// 
			// foregroundColorToolStripMenuItem
			// 
			this.foregroundColorToolStripMenuItem.Name = "foregroundColorToolStripMenuItem";
			this.foregroundColorToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.foregroundColorToolStripMenuItem.Text = "Foreground Color...";
			// 
			// backgroundColorToolStripMenuItem
			// 
			this.backgroundColorToolStripMenuItem.Name = "backgroundColorToolStripMenuItem";
			this.backgroundColorToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.backgroundColorToolStripMenuItem.Text = "Background Color...";
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
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.listBoxFont);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pictureBoxText);
			this.splitContainer1.Size = new System.Drawing.Size(506, 288);
			this.splitContainer1.SplitterDistance = 176;
			this.splitContainer1.TabIndex = 2;
			// 
			// pictureBoxText
			// 
			this.pictureBoxText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxText.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxText.Name = "pictureBoxText";
			this.pictureBoxText.Size = new System.Drawing.Size(326, 288);
			this.pictureBoxText.TabIndex = 0;
			this.pictureBoxText.TabStop = false;
			this.pictureBoxText.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxText_Paint);
			// 
			// listBoxFont
			// 
			this.listBoxFont.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxFont.FormattingEnabled = true;
			this.listBoxFont.Location = new System.Drawing.Point(0, 0);
			this.listBoxFont.Name = "listBoxFont";
			this.listBoxFont.Size = new System.Drawing.Size(176, 288);
			this.listBoxFont.TabIndex = 0;
			this.listBoxFont.SelectedIndexChanged += new System.EventHandler(this.listBoxFont_SelectedIndexChanged);
			// 
			// openFontDialog
			// 
			this.openFontDialog.Filter = "All Font Files|*.ttf;*.otf|TrueType Fonts|*.ttf|OpenType Fonts|*.otf";
			// 
			// ExampleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(506, 312);
			this.Controls.Add(this.mainMenu);
			this.Controls.Add(this.splitContainer1);
			this.MainMenuStrip = this.mainMenu;
			this.Name = "ExampleForm";
			this.Text = "SharpFont Example";
			this.Load += new System.EventHandler(this.ExampleForm_Load);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxText)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
		private System.Windows.Forms.ToolStripMenuItem foregroundColorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem backgroundColorToolStripMenuItem;
		private System.Windows.Forms.ListBox listBoxFont;
		private System.Windows.Forms.OpenFileDialog openFontDialog;
	}
}
