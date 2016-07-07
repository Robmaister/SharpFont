#region MIT License
/*Copyright (c) 2015-2016 Robert Rouhani <robert.rouhani@gmail.com>

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

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using SharpFont;

namespace Examples
{
	public partial class ExampleForm : Form
	{
		//TODO implement System.Drawing alongside SharpFont
		private bool useSharpFont = true;

		private string fontFolder;
		private string sampleText;
		private float fontSize;
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
			// Some variations of the character set shown by the Windows Font Viewer
			//sampleText = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890.:,;'\"(!?)+-*//=";
			//sampleText = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890";
			sampleText = "abcdefghijklmnopqrstuvwxyz";
			fontService.Size = 62f;
			fontSize = 62f;
			mainMenuFontSize.Text = fontService.Size.ToString("0.0");
			foreColor = Color.Black;
			backColor = Color.Transparent;

			decimal testValue = 12.1234567890123456M;
			Debug.Print("testValue  : {0}", (double)testValue);
			// none of these keep the precision I was expecting...
			var f26 = new Fixed26Dot6(testValue);
			Debug.Print("Fixed 26.6 : {0}", (double)f26);
			Debug.Print("Fixed 26.6 : {0}", f26);
			var f16 = new Fixed16Dot16(testValue);
			Debug.Print("Fixed 16.16: {0}", (double)f16);
			Debug.Print("Fixed 16.16: {0}", f16);
			var f2 = new Fixed2Dot14((double)testValue); // decimal constructor crashes here
			Debug.Print("Fixed  2.14: {0}", (double)f2);
			Debug.Print("Fixed  2.14: {0}", f2);
		}

		#endregion

		#region Status messages

		private void ClearStatus()
		{
			statusLabel.Text = "";
			statusLabel.Image = null;
		}

		private void ShowInfo(string msg, params object[] args)
		{
			statusLabel.Text = string.Format(msg, args);
			statusLabel.Image = SystemIcons.Information.ToBitmap();
		}

		private void ShowError(string msg, params object[] args)
		{
			statusLabel.Text = string.Format(msg, args);
			statusLabel.Image = SystemIcons.Warning.ToBitmap();
		}

		private void ShowException(Exception ex)
		{
			statusLabel.Text = ex.Message;
			if (ex.GetType() == typeof(FreeTypeException))
			{
				fontService = new FontService();
				fontService.Size = fontSize;
			}
			statusLabel.Image = SystemIcons.Error.ToBitmap();
		}

		#endregion // Status messages

		#region Helper methods

		private void LoadFolder(string path)
		{
			fontFolder = path;
			RebuildFontList();
		}

		private void RebuildFontList()
		{
			ClearStatus();
			var di = new DirectoryInfo(fontFolder);
			if (!di.Exists)
			{
				ShowError("{0} doesn't exist.", di.FullName);
				return;
			}

			listBoxFont.Items.Clear();

			try
			{
				foreach (var file in fontService.GetFontFiles(di, false))
				{
					listBoxFont.Items.Add(Path.GetFileName(file.FullName));
				}
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}

			listBoxFont.SelectedIndex = 0;
		}

		private void DisplayFont(string filename)
		{
			try
			{
				fontService.SetFont(filename);
			}
			catch (Exception ex)
			{
				ShowException(ex);
			}
			RedrawFont();
		}

		private void RedrawFont()
		{
			ClearStatus();
			pictureBoxText.BackColor = backColor;
			panel1.BackColor = backColor;
			try
			{
				pictureBoxText.Image = fontService.RenderString(sampleText, foreColor, backColor);
				ClearStatus();
				if (pictureBoxText.Image == null)
				{
					ShowInfo("Nothing was rendered. Perhaps the selected font doesn't include the characters you need.");
				}
			}
			catch (Exception ex)
			{
				ShowException(ex);
				pictureBoxText.Image = null;
			}
			pictureBoxText.Visible = pictureBoxText.Image != null;
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
				var di = new DirectoryInfo(fontFolder);
				dlg.SelectedPath = di.FullName;
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
				fontSize = value;
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
			RedrawFont();
		}

		#endregion // Handlers

	}
}
