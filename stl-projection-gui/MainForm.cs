using System;
using System.Drawing;
using System.Windows.Forms;

namespace stl_projection_gui
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			btnEmbed.Enabled = false;
		}

		public void RefreshPlot()
		{
			imgPlot.Image = Image.FromFile(STLProjectionGUI.pngPath);
			Refresh();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			inputId.Value = STLProjectionGUI.id;
			inputMargin.Value = (decimal) STLProjectionGUI.margin;
			inputSize.Value = (decimal) STLProjectionGUI.size;
			inputSubd.Value = (decimal) STLProjectionGUI.subdivisions;
		}

		private void btnInputBrowse_Click(object sender, EventArgs e)
		{
			var fd = new OpenFileDialog();
			fd.Multiselect = false;
			fd.CheckFileExists = true;
			fd.Filter = "STL files (*.stl)|*.stl";

			var result = fd.ShowDialog();

			if (result == DialogResult.OK)
			{
				labelInputPath.Text = fd.FileName;
				STLProjectionGUI.inputPath = fd.FileName;
				CheckEmbedButtonState();
			}
		}

		private void btnOutputBrowse_Click(object sender, EventArgs e)
		{
			var fd = new OpenFileDialog();
			fd.Multiselect = false;
			fd.CheckFileExists = false;
			fd.Filter = "STL files (*.stl)|*.stl";

			var result = fd.ShowDialog();

			if (result == DialogResult.OK)
			{
				labelOutputPath.Text = fd.FileName;
				STLProjectionGUI.outputPath = fd.FileName;
				CheckEmbedButtonState();
			}
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			STLProjectionGUI.id = (int) inputId.Value;
		}

		private void inputSize_ValueChanged(object sender, EventArgs e)
		{
			STLProjectionGUI.size = (double) inputSize.Value;
		}

		private void inputMargin_ValueChanged(object sender, EventArgs e)
		{
			STLProjectionGUI.margin = (double) inputMargin.Value;
		}

		private void inputSubd_ValueChanged(object sender, EventArgs e)
		{
			STLProjectionGUI.subdivisions = (int) inputSubd.Value;
		}

		private void btnEmbed_Click(object sender, EventArgs e)
		{
			Console.WriteLine("Embed");

			btnEmbed.Enabled = false;

			STLProjectionGUI.Embed(progressBar,
			                       () =>
			                       {
				                       progressBar.Invoke((MethodInvoker) delegate { btnEmbed.Enabled = true; });
			                       });
		}

		private void CheckEmbedButtonState()
		{
			if (string.IsNullOrEmpty(STLProjectionGUI.inputPath) == false &&
			    string.IsNullOrEmpty(STLProjectionGUI.outputPath) == false)
			{
				btnEmbed.Enabled = true;
			}
			else
			{
				btnEmbed.Enabled = false;
			}
		}
	}
}
