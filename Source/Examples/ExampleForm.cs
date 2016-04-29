using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using SharpFont;

namespace Examples
{
	public partial class ExampleForm : Form
	{
		private bool useSharpFont = true;

		private string fontFolder;
		private string sampleText;

		private Library lib;
		private Face fontFace;

		public ExampleForm()
		{
			InitializeComponent();

			fontFolder = "Fonts/";
			sampleText = "SharpFont";
		}

		private void ExampleForm_Load(object sender, EventArgs e)
		{
			lib = new Library();

			RebuildFontList();
			if (listBoxFont.Items.Count > 0)
				listBoxFont.SelectedIndex = 0;
		}

		private void pictureBoxText_Paint(object sender, PaintEventArgs e)
		{
			if (lib == null || fontFace == null)
				return;

			pictureBoxText.Image = Program.RenderString(lib, fontFace, sampleText);
		}

		private void mainMenuEditSharpFont_Click(object sender, EventArgs e)
		{
			mainMenuEditSharpFont.Checked = true;
			mainMenuEditSystemDrawing.Checked = false;

			useSharpFont = true;
		}

		private void mainMenuEditSystemDrawing_Click(object sender, EventArgs e)
		{
			mainMenuEditSharpFont.Checked = false;
			mainMenuEditSystemDrawing.Checked = true;

			useSharpFont = false;
		}

		private void RebuildFontList()
		{
			if ( !Directory.Exists(fontFolder) )
				return;

			//HACK only checking for ttf even though FreeType supports far more formats.
			foreach (var file in Directory.GetFiles(fontFolder, "*.ttf"))
				listBoxFont.Items.Add(Path.GetFileName(file));
			foreach (var file in Directory.GetFiles(fontFolder, "*.otf"))
				listBoxFont.Items.Add(Path.GetFileName(file));
		}

		private void listViewFont_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			
		}

		private void listBoxFont_SelectedIndexChanged(object sender, EventArgs e)
		{
			fontFace = new Face(lib, Path.Combine(Path.GetFullPath(fontFolder), (string)listBoxFont.SelectedItem));
			fontFace.SetCharSize(0, 62, 0, 96);
			pictureBoxText.Invalidate();
			pictureBoxText.Image = Program.RenderString(lib, fontFace, sampleText);
		}

		private void mainMenuFileOpen_Click(object sender, EventArgs e)
		{
			if(openFontDialog.ShowDialog() == DialogResult.OK)
				{
					listBoxFont.Items.Add(openFontDialog.FileName);
				}
		}

		private void mainMenuFileExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void toolStripComboBox1_TextUpdate(object sender, EventArgs e)
		{

		}
	}
}
