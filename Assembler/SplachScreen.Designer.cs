namespace Assembler
{
    partial class SplachScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplachScreen));
            this.assembler = new System.Windows.Forms.Label();
            this.version = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.rectangleShape1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.rectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // assembler
            // 
            this.assembler.AutoSize = true;
            this.assembler.BackColor = System.Drawing.Color.Transparent;
            this.assembler.Font = new System.Drawing.Font("Arial Rounded MT Bold", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assembler.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.assembler.Location = new System.Drawing.Point(12, 103);
            this.assembler.Name = "assembler";
            this.assembler.Size = new System.Drawing.Size(230, 46);
            this.assembler.TabIndex = 0;
            this.assembler.Text = "Assembler";
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.BackColor = System.Drawing.Color.Transparent;
            this.version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.version.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.version.Location = new System.Drawing.Point(234, 130);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(22, 13);
            this.version.TabIndex = 1;
            this.version.Text = "v.1";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.rectangleShape2,
            this.rectangleShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(681, 280);
            this.shapeContainer1.TabIndex = 2;
            this.shapeContainer1.TabStop = false;
            // 
            // rectangleShape1
            // 
            this.rectangleShape1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.rectangleShape1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.rectangleShape1.BorderWidth = 3;
            this.rectangleShape1.CornerRadius = 8;
            this.rectangleShape1.Location = new System.Drawing.Point(82, 194);
            this.rectangleShape1.Name = "rectangleShape1";
            this.rectangleShape1.Size = new System.Drawing.Size(530, 17);
            // 
            // rectangleShape2
            // 
            this.rectangleShape2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.rectangleShape2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
            this.rectangleShape2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.rectangleShape2.BorderWidth = 3;
            this.rectangleShape2.CornerRadius = 3;
            this.rectangleShape2.Location = new System.Drawing.Point(88, 199);
            this.rectangleShape2.Name = "rectangleShape2";
            this.rectangleShape2.Size = new System.Drawing.Size(25, 7);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SplachScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(681, 280);
            this.Controls.Add(this.version);
            this.Controls.Add(this.assembler);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplachScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplachScreen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label assembler;
        private System.Windows.Forms.Label version;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape2;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape rectangleShape1;
        private System.Windows.Forms.Timer timer1;
    }
}