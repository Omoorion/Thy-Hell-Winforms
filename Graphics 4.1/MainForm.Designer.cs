
namespace Graphics_4._1
{
    partial class MainForm
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
            this.Screen = new System.Windows.Forms.PictureBox();
            this.Crosshair = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Crosshair)).BeginInit();
            this.SuspendLayout();
            // 
            // Screen
            // 
            this.Screen.Location = new System.Drawing.Point(0, 0);
            this.Screen.Margin = new System.Windows.Forms.Padding(0);
            this.Screen.Name = "Screen";
            this.Screen.Size = new System.Drawing.Size(800, 450);
            this.Screen.TabIndex = 0;
            this.Screen.TabStop = false;
            this.Screen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Screen_MouseMove);
            // 
            // Crosshair
            // 
            this.Crosshair.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Crosshair.Location = new System.Drawing.Point(298, 187);
            this.Crosshair.Name = "Crosshair";
            this.Crosshair.Size = new System.Drawing.Size(100, 50);
            this.Crosshair.TabIndex = 1;
            this.Crosshair.TabStop = false;
            this.Crosshair.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Crosshair_MouseClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Crosshair);
            this.Controls.Add(this.Screen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "FPS BE LIKE";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Screen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Crosshair)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Screen;
        private System.Windows.Forms.PictureBox Crosshair;
    }
}

