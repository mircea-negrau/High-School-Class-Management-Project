namespace Catalog.Forms.User
{
    partial class FormClassroom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClassroom));
            this.buttonExit1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelChildForm = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonExit1
            // 
            this.buttonExit1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonExit1.BackgroundImage")));
            this.buttonExit1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonExit1.FlatAppearance.BorderSize = 0;
            this.buttonExit1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit1.Location = new System.Drawing.Point(860, -1);
            this.buttonExit1.Name = "buttonExit1";
            this.buttonExit1.Size = new System.Drawing.Size(32, 32);
            this.buttonExit1.TabIndex = 17;
            this.buttonExit1.UseVisualStyleBackColor = true;
            this.buttonExit1.Click += new System.EventHandler(this.buttonExit1_Click);
            this.buttonExit1.MouseEnter += new System.EventHandler(this.buttonExit1_MouseEnter);
            this.buttonExit1.MouseLeave += new System.EventHandler(this.buttonExit1_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(184)))), ((int)(((byte)(206)))));
            this.panel1.Controls.Add(this.buttonExit1);
            this.panel1.Location = new System.Drawing.Point(-2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(907, 35);
            this.panel1.TabIndex = 15;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 5;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelChildForm
            // 
            this.panelChildForm.Location = new System.Drawing.Point(199, 32);
            this.panelChildForm.Name = "panelChildForm";
            this.panelChildForm.Size = new System.Drawing.Size(703, 500);
            this.panelChildForm.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 64);
            this.button1.TabIndex = 17;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(199, 633);
            this.panel2.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.panel3.Location = new System.Drawing.Point(196, 528);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(709, 105);
            this.panel3.TabIndex = 18;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(42, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 64);
            this.button2.TabIndex = 18;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormClassroom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(902, 633);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelChildForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormClassroom";
            this.Text = "AnyDesk";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExit1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelChildForm;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
    }
}