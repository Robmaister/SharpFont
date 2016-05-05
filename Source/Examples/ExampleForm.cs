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
		private Color foreColor;
		private Color backColor;

		private FontService fontService;

		#region Constructor

		public ExampleForm()
		{
			InitializeComponent();

			fontService = new FontService();
			fontFolder = "Fonts/";
			sampleText = "SharpFont";
			fontService.Size = 62f;
			mainMenuFontSize.Text = fontService.Size.ToString("0.0");
			foreColor = Color.Black;
			backColor = Color.White;
		}

		#endregion

		#region Helper methods

		private void LoadFolder(string path)
		{
			fontFolder = path;
			RebuildFontList();
		}

		private void RebuildFontList()
		{
			if (!Directory.Exists(fontFolder))
				return;

			listBoxFont.Items.Clear();

			//HACK only checking for ttf even though FreeType supports far more formats.
			foreach (var file in Directory.GetFiles(fontFolder, "*.ttf"))
				listBoxFont.Items.Add(Path.GetFileName(file));
			foreach (var file in Directory.GetFiles(fontFolder, "*.otf"))
				listBoxFont.Items.Add(Path.GetFileName(file));

			listBoxFont.SelectedIndex = 0;
		}

		private void DisplayFont(string filename)
		{
			try
			{
				fontService.SetFont(filename);
			}
			catch { }
			RedrawFont();
		}

		private void RedrawFont()
		{
			pictureBoxText.BackColor = backColor;
			try
			{
				pictureBoxText.Image = fontService.RenderString(sampleText, foreColor);
				statusLabel.Text = "";
			}
			catch (Exception ex)
			{
				statusLabel.Text = ex.Message;
				pictureBoxText.Image = null;
			}
		}

		#endregion // Helper methods

		#region Handlers

		private void ExampleForm_Load(object sender, EventArgs e)
		{
			statusLabel.Text = "";
			RebuildFontList();
			if (listBoxFont.Items.Count > 0)
				listBoxFont.SelectedIndex = 0;
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

		private void listBoxFont_SelectedIndexChanged(object sender, EventArgs e)
		{
			string filename = Path.Combine(Path.GetFullPath(fontFolder), (string)listBoxFont.SelectedItem);
			DisplayFont(filename);
		}

		private void mainMenuFileOpen_Click(object sender, EventArgs e)
		{
			if (openFontDialog.ShowDialog() == DialogResult.OK)
			{
				string filename = openFontDialog.FileName;
				if (!listBoxFont.Items.Contains(filename))
				{
					listBoxFont.Items.Add(filename);
				}
				listBoxFont.SelectedIndex = listBoxFont.FindString(filename);
			}
		}

		private void mainMenuFolderOpen_Click(object sender, EventArgs e)
		{
			using (var dlg = new FolderBrowserDialog())
			{
				dlg.Description = "Select Font Folder";
				dlg.RootFolder = Environment.SpecialFolder.MyComputer;
				dlg.SelectedPath = @"C:\";
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					LoadFolder(dlg.SelectedPath);
				}
			}
		}

		private void mainMenuFileExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void mainMenuFontSize_TextUpdate(object sender, EventArgs e)
		{
			float value = 62.0f;
			if (float.TryParse(mainMenuFontSize.Text, out value))
			{
				fontService.Size = value;
				RedrawFont();
			}
		}

		private void foregroundColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var dlg = new ColorDialog())
			{
				dlg.Color = foreColor;
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					foreColor = dlg.Color;
				}
			}
			RedrawFont();
		}

		private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var dlg = new ColorDialog())
			{
				dlg.Color = backColor;
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					backColor = dlg.Color;
				}
			}
			pictureBoxText.BackColor = backColor;
		}

		#endregion // Handlers

	}
}
