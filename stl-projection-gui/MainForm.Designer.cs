namespace stl_projection_gui
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
			this.labelInputPath = new System.Windows.Forms.Label();
			this.btnInputBrowse = new System.Windows.Forms.Button();
			this.labelOutputPath = new System.Windows.Forms.Label();
			this.btnOutputBrowse = new System.Windows.Forms.Button();
			this.inputId = new System.Windows.Forms.NumericUpDown();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.inputSubd = new System.Windows.Forms.NumericUpDown();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.btnEmbed = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.inputSize = new System.Windows.Forms.NumericUpDown();
			this.inputMargin = new System.Windows.Forms.NumericUpDown();
			this.imgPlot = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize) (this.inputId)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.inputSubd)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.inputSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.inputMargin)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.imgPlot)).BeginInit();
			this.SuspendLayout();
			// 
			// labelInputPath
			// 
			this.labelInputPath.ForeColor = System.Drawing.SystemColors.GrayText;
			this.labelInputPath.Location = new System.Drawing.Point(4, 1);
			this.labelInputPath.Name = "labelInputPath";
			this.labelInputPath.Size = new System.Drawing.Size(250, 27);
			this.labelInputPath.TabIndex = 0;
			this.labelInputPath.Text = "Browse to input STL file...";
			this.labelInputPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnInputBrowse
			// 
			this.btnInputBrowse.Location = new System.Drawing.Point(263, 2);
			this.btnInputBrowse.Name = "btnInputBrowse";
			this.btnInputBrowse.Size = new System.Drawing.Size(96, 26);
			this.btnInputBrowse.TabIndex = 1;
			this.btnInputBrowse.Text = "Browse";
			this.btnInputBrowse.UseVisualStyleBackColor = true;
			this.btnInputBrowse.Click += new System.EventHandler(this.btnInputBrowse_Click);
			// 
			// labelOutputPath
			// 
			this.labelOutputPath.ForeColor = System.Drawing.SystemColors.GrayText;
			this.labelOutputPath.Location = new System.Drawing.Point(4, 37);
			this.labelOutputPath.Name = "labelOutputPath";
			this.labelOutputPath.Size = new System.Drawing.Size(250, 27);
			this.labelOutputPath.TabIndex = 2;
			this.labelOutputPath.Text = "Browse to output STL file...";
			this.labelOutputPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnOutputBrowse
			// 
			this.btnOutputBrowse.Location = new System.Drawing.Point(264, 38);
			this.btnOutputBrowse.Name = "btnOutputBrowse";
			this.btnOutputBrowse.Size = new System.Drawing.Size(96, 26);
			this.btnOutputBrowse.TabIndex = 3;
			this.btnOutputBrowse.Text = "Browse";
			this.btnOutputBrowse.UseVisualStyleBackColor = true;
			this.btnOutputBrowse.Click += new System.EventHandler(this.btnOutputBrowse_Click);
			// 
			// inputId
			// 
			this.inputId.Location = new System.Drawing.Point(157, 156);
			this.inputId.Maximum = new decimal(new int[] {244140625, 0, 0, 0});
			this.inputId.Name = "inputId";
			this.inputId.Size = new System.Drawing.Size(173, 20);
			this.inputId.TabIndex = 4;
			this.inputId.ThousandsSeparator = true;
			this.inputId.Value = new decimal(new int[] {165971234, 0, 0, 0});
			this.inputId.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.inputSubd);
			this.panel1.Controls.Add(this.progressBar);
			this.panel1.Controls.Add(this.btnEmbed);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.inputSize);
			this.panel1.Controls.Add(this.inputMargin);
			this.panel1.Controls.Add(this.imgPlot);
			this.panel1.Controls.Add(this.inputId);
			this.panel1.Controls.Add(this.btnOutputBrowse);
			this.panel1.Controls.Add(this.labelOutputPath);
			this.panel1.Controls.Add(this.btnInputBrowse);
			this.panel1.Controls.Add(this.labelInputPath);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(360, 437);
			this.panel1.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label4.Location = new System.Drawing.Point(28, 78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(126, 20);
			this.label4.TabIndex = 14;
			this.label4.Text = "Subdivisions";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// inputSubd
			// 
			this.inputSubd.Location = new System.Drawing.Point(157, 78);
			this.inputSubd.Maximum = new decimal(new int[] {6, 0, 0, 0});
			this.inputSubd.Name = "inputSubd";
			this.inputSubd.Size = new System.Drawing.Size(173, 20);
			this.inputSubd.TabIndex = 13;
			this.inputSubd.ThousandsSeparator = true;
			this.inputSubd.ValueChanged += new System.EventHandler(this.inputSubd_ValueChanged);
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(3, 408);
			this.progressBar.MarqueeAnimationSpeed = 10;
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(250, 25);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar.TabIndex = 12;
			// 
			// btnEmbed
			// 
			this.btnEmbed.Location = new System.Drawing.Point(261, 408);
			this.btnEmbed.Name = "btnEmbed";
			this.btnEmbed.Size = new System.Drawing.Size(96, 26);
			this.btnEmbed.TabIndex = 11;
			this.btnEmbed.Text = "Embed";
			this.btnEmbed.UseVisualStyleBackColor = true;
			this.btnEmbed.Click += new System.EventHandler(this.btnEmbed_Click);
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label3.Location = new System.Drawing.Point(28, 156);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(126, 20);
			this.label3.TabIndex = 10;
			this.label3.Text = "ID";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label2.Location = new System.Drawing.Point(28, 130);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126, 20);
			this.label2.TabIndex = 9;
			this.label2.Text = "Margin";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label1.Location = new System.Drawing.Point(28, 104);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(126, 20);
			this.label1.TabIndex = 8;
			this.label1.Text = "Size";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// inputSize
			// 
			this.inputSize.DecimalPlaces = 2;
			this.inputSize.Increment = new decimal(new int[] {5, 0, 0, 65536});
			this.inputSize.Location = new System.Drawing.Point(157, 104);
			this.inputSize.Maximum = new decimal(new int[] {500, 0, 0, 0});
			this.inputSize.Name = "inputSize";
			this.inputSize.Size = new System.Drawing.Size(173, 20);
			this.inputSize.TabIndex = 7;
			this.inputSize.ThousandsSeparator = true;
			this.inputSize.Value = new decimal(new int[] {500, 0, 0, 0});
			this.inputSize.ValueChanged += new System.EventHandler(this.inputSize_ValueChanged);
			// 
			// inputMargin
			// 
			this.inputMargin.DecimalPlaces = 2;
			this.inputMargin.Increment = new decimal(new int[] {1, 0, 0, 65536});
			this.inputMargin.Location = new System.Drawing.Point(157, 130);
			this.inputMargin.Maximum = new decimal(new int[] {5, 0, 0, 0});
			this.inputMargin.Name = "inputMargin";
			this.inputMargin.Size = new System.Drawing.Size(173, 20);
			this.inputMargin.TabIndex = 6;
			this.inputMargin.ThousandsSeparator = true;
			this.inputMargin.Value = new decimal(new int[] {5, 0, 0, 0});
			this.inputMargin.ValueChanged += new System.EventHandler(this.inputMargin_ValueChanged);
			// 
			// imgPlot
			// 
			this.imgPlot.Location = new System.Drawing.Point(28, 191);
			this.imgPlot.Name = "imgPlot";
			this.imgPlot.Size = new System.Drawing.Size(302, 211);
			this.imgPlot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.imgPlot.TabIndex = 5;
			this.imgPlot.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 461);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MainForm";
			this.Text = "STL Symbol Projection";
			((System.ComponentModel.ISupportInitialize) (this.inputId)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (this.inputSubd)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.inputSize)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.inputMargin)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.imgPlot)).EndInit();
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.NumericUpDown inputSubd;
		private System.Windows.Forms.Label label4;

		private System.Windows.Forms.Button btnEmbed;
		private System.Windows.Forms.ProgressBar progressBar;

		private System.Windows.Forms.NumericUpDown inputId;
		private System.Windows.Forms.NumericUpDown inputMargin;
		private System.Windows.Forms.NumericUpDown inputSize;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;

		private System.Windows.Forms.PictureBox imgPlot;

		private System.Windows.Forms.Button btnOutputBrowse;
		private System.Windows.Forms.Label labelOutputPath;

		private System.Windows.Forms.Button btnInputBrowse;

		private System.Windows.Forms.Label labelInputPath;

		private System.Windows.Forms.Panel panel1;

#endregion
	}
}
