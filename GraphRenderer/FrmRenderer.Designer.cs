
namespace GraphRenderer {
	partial class FrmRenderer {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.LblInfo = new System.Windows.Forms.Label();
			this.SkiaCanvas = new SkiaSharp.Views.Desktop.SKControl();
			this.GLCanvas = new SkiaSharp.Views.Desktop.SKGLControl();
			this.SuspendLayout();
			// 
			// LblInfo
			// 
			this.LblInfo.AutoSize = true;
			this.LblInfo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LblInfo.Location = new System.Drawing.Point(12, 9);
			this.LblInfo.Name = "LblInfo";
			this.LblInfo.Size = new System.Drawing.Size(105, 45);
			this.LblInfo.TabIndex = 1;
			this.LblInfo.Text = "label1";
			// 
			// SkiaCanvas
			// 
			this.SkiaCanvas.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.SkiaCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SkiaCanvas.Location = new System.Drawing.Point(0, 0);
			this.SkiaCanvas.Name = "SkiaCanvas";
			this.SkiaCanvas.Size = new System.Drawing.Size(1991, 1316);
			this.SkiaCanvas.TabIndex = 2;
			this.SkiaCanvas.Text = "skControl1";
			// 
			// GLCanvas
			// 
			this.GLCanvas.BackColor = System.Drawing.Color.Black;
			this.GLCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GLCanvas.Location = new System.Drawing.Point(0, 0);
			this.GLCanvas.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			this.GLCanvas.Name = "GLCanvas";
			this.GLCanvas.Size = new System.Drawing.Size(1991, 1316);
			this.GLCanvas.TabIndex = 3;
			this.GLCanvas.VSync = true;
			// 
			// FrmRenderer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1991, 1316);
			this.Controls.Add(this.LblInfo);
			this.Controls.Add(this.GLCanvas);
			this.Controls.Add(this.SkiaCanvas);
			this.Name = "FrmRenderer";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label LblInfo;
		private SkiaSharp.Views.Desktop.SKControl SkiaCanvas;
		private SkiaSharp.Views.Desktop.SKGLControl GLCanvas;
	}
}

