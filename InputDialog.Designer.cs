namespace _3D_Model_Accuracy_Checker
{
    partial class InputDialog
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
            Button button2;
            textBox1 = new TextBox();
            ok_btn = new Button();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaption;
            button2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = SystemColors.ButtonFace;
            button2.ImageAlign = ContentAlignment.TopCenter;
            button2.Location = new Point(220, 8);
            button2.Name = "button2";
            button2.Size = new Size(50, 35);
            button2.TabIndex = 4;
            button2.Text = "?";
            button2.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 65);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "A1 \nA2 \nA1, A2 \nA1 A2";
            textBox1.Size = new Size(254, 81);
            textBox1.TabIndex = 0;
            // 
            // ok_btn
            // 
            ok_btn.BackColor = Color.ForestGreen;
            ok_btn.ForeColor = SystemColors.ButtonFace;
            ok_btn.Location = new Point(12, 152);
            ok_btn.Name = "ok_btn";
            ok_btn.Size = new Size(126, 35);
            ok_btn.TabIndex = 1;
            ok_btn.Text = "OK";
            ok_btn.UseVisualStyleBackColor = false;
            ok_btn.Click += okButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(153, 45);
            label1.TabIndex = 2;
            label1.Text = "Enter your control points \nname separated by \ncomma, new line or spaces.";
            // 
            // button1
            // 
            button1.BackColor = Color.IndianRed;
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(144, 152);
            button1.Name = "button1";
            button1.Size = new Size(122, 35);
            button1.TabIndex = 3;
            button1.Text = "Do not filter";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // InputDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 259);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(ok_btn);
            Controls.Add(textBox1);
            Name = "InputDialog";
            Text = "InputDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button ok_btn;
        private Label label1;
        private Button button1;
    }
}