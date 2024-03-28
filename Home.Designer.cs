namespace DBproject
{
    partial class Home
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Studentbtn = new System.Windows.Forms.Button();
            this.Clobtn = new System.Windows.Forms.Button();
            this.Rubricbtn = new System.Windows.Forms.Button();
            this.assesmentbtn = new System.Windows.Forms.Button();
            this.RLbtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.IndianRed;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(9, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(753, 102);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Forte", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(231, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(332, 70);
            this.label1.TabIndex = 1;
            this.label1.Text = "Computer Science Lab\r\n Management System";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Studentbtn
            // 
            this.Studentbtn.BackColor = System.Drawing.Color.Silver;
            this.Studentbtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Studentbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Studentbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Studentbtn.Location = new System.Drawing.Point(57, 160);
            this.Studentbtn.Margin = new System.Windows.Forms.Padding(2);
            this.Studentbtn.Name = "Studentbtn";
            this.Studentbtn.Size = new System.Drawing.Size(269, 32);
            this.Studentbtn.TabIndex = 1;
            this.Studentbtn.Text = "Manage Student";
            this.Studentbtn.UseVisualStyleBackColor = false;
            this.Studentbtn.Click += new System.EventHandler(this.Studentbtn_Click);
            // 
            // Clobtn
            // 
            this.Clobtn.BackColor = System.Drawing.Color.Silver;
            this.Clobtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Clobtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clobtn.Location = new System.Drawing.Point(57, 228);
            this.Clobtn.Margin = new System.Windows.Forms.Padding(2);
            this.Clobtn.Name = "Clobtn";
            this.Clobtn.Size = new System.Drawing.Size(269, 32);
            this.Clobtn.TabIndex = 2;
            this.Clobtn.Text = "Manage CLO";
            this.Clobtn.UseVisualStyleBackColor = false;
            this.Clobtn.Click += new System.EventHandler(this.Clobtn_Click);
            // 
            // Rubricbtn
            // 
            this.Rubricbtn.BackColor = System.Drawing.Color.Silver;
            this.Rubricbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Rubricbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rubricbtn.Location = new System.Drawing.Point(422, 160);
            this.Rubricbtn.Margin = new System.Windows.Forms.Padding(2);
            this.Rubricbtn.Name = "Rubricbtn";
            this.Rubricbtn.Size = new System.Drawing.Size(269, 32);
            this.Rubricbtn.TabIndex = 3;
            this.Rubricbtn.Text = "Manage Rubrics";
            this.Rubricbtn.UseVisualStyleBackColor = false;
            this.Rubricbtn.Click += new System.EventHandler(this.Rubricbtn_Click);
            // 
            // assesmentbtn
            // 
            this.assesmentbtn.BackColor = System.Drawing.Color.Silver;
            this.assesmentbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.assesmentbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assesmentbtn.Location = new System.Drawing.Point(422, 228);
            this.assesmentbtn.Margin = new System.Windows.Forms.Padding(2);
            this.assesmentbtn.Name = "assesmentbtn";
            this.assesmentbtn.Size = new System.Drawing.Size(269, 32);
            this.assesmentbtn.TabIndex = 4;
            this.assesmentbtn.Text = "Manage Assesment";
            this.assesmentbtn.UseVisualStyleBackColor = false;
            this.assesmentbtn.Click += new System.EventHandler(this.assesmentbtn_Click);
            // 
            // RLbtn
            // 
            this.RLbtn.BackColor = System.Drawing.Color.Silver;
            this.RLbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RLbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RLbtn.Location = new System.Drawing.Point(246, 301);
            this.RLbtn.Margin = new System.Windows.Forms.Padding(2);
            this.RLbtn.Name = "RLbtn";
            this.RLbtn.Size = new System.Drawing.Size(269, 32);
            this.RLbtn.TabIndex = 5;
            this.RLbtn.Text = "Manage Rubric Level";
            this.RLbtn.UseVisualStyleBackColor = false;
            this.RLbtn.Click += new System.EventHandler(this.RLbtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.IndianRed;
            this.textBox1.Location = new System.Drawing.Point(0, 389);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(772, 104);
            this.textBox1.TabIndex = 6;
            this.textBox1.UseWaitCursor = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(771, 473);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.RLbtn);
            this.Controls.Add(this.assesmentbtn);
            this.Controls.Add(this.Rubricbtn);
            this.Controls.Add(this.Clobtn);
            this.Controls.Add(this.Studentbtn);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Studentbtn;
        private System.Windows.Forms.Button Clobtn;
        private System.Windows.Forms.Button Rubricbtn;
        private System.Windows.Forms.Button assesmentbtn;
        private System.Windows.Forms.Button RLbtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
    }
}

