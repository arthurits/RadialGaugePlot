﻿
namespace TestControl
{
    partial class FrmTestControl
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTestControl));
            this.plot1 = new RadialGaugeControl.Plot();
            this.SuspendLayout();
            // 
            // plot1
            // 
            this.plot1.Center = ((System.Drawing.PointF)(resources.GetObject("plot1.Center")));
            this.plot1.Location = new System.Drawing.Point(12, 12);
            this.plot1.Name = "plot1";
            this.plot1.RectData = ((System.Drawing.RectangleF)(resources.GetObject("plot1.RectData")));
            this.plot1.RectTitle = ((System.Drawing.RectangleF)(resources.GetObject("plot1.RectTitle")));
            this.plot1.Size = new System.Drawing.Size(760, 437);
            this.plot1.TabIndex = 0;
            // 
            // FrmTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.plot1);
            this.Name = "FrmTestControl";
            this.Text = "Test control";
            this.ResumeLayout(false);

        }

        #endregion

        private RadialGaugeControl.Plot plot1;
    }
}
