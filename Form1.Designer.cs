namespace _3D_Model_Accuracy_Checker
{
    partial class Form1
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
            Table_pabel = new Panel();
            LoadFile_btn = new Button();
            Calc_btn = new Button();
            Export_btn = new Button();
            textBox1 = new TextBox();
            Result_lbl = new Label();
            points_dataGridView = new DataGridView();
            Table_pabel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)points_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // Table_pabel
            // 
            Table_pabel.Controls.Add(points_dataGridView);
            Table_pabel.Location = new Point(9, 57);
            Table_pabel.Name = "Table_pabel";
            Table_pabel.Size = new Size(779, 384);
            Table_pabel.TabIndex = 0;
            // 
            // LoadFile_btn
            // 
            LoadFile_btn.Location = new Point(9, 12);
            LoadFile_btn.Name = "LoadFile_btn";
            LoadFile_btn.Size = new Size(101, 39);
            LoadFile_btn.TabIndex = 0;
            LoadFile_btn.Text = "Load File";
            LoadFile_btn.UseVisualStyleBackColor = true;
            // 
            // Calc_btn
            // 
            Calc_btn.Location = new Point(116, 12);
            Calc_btn.Name = "Calc_btn";
            Calc_btn.Size = new Size(96, 39);
            Calc_btn.TabIndex = 1;
            Calc_btn.Text = "Calculate";
            Calc_btn.UseVisualStyleBackColor = true;
            // 
            // Export_btn
            // 
            Export_btn.Location = new Point(685, 12);
            Export_btn.Name = "Export_btn";
            Export_btn.Size = new Size(103, 39);
            Export_btn.TabIndex = 2;
            Export_btn.Text = "Export Result";
            Export_btn.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(388, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(108, 39);
            textBox1.TabIndex = 3;
            // 
            // Result_lbl
            // 
            Result_lbl.AutoSize = true;
            Result_lbl.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Result_lbl.Location = new Point(304, 12);
            Result_lbl.Name = "Result_lbl";
            Result_lbl.Size = new Size(78, 32);
            Result_lbl.TabIndex = 4;
            Result_lbl.Text = "Result";
            // 
            // points_dataGridView
            // 
            points_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            points_dataGridView.Dock = DockStyle.Fill;
            points_dataGridView.Location = new Point(0, 0);
            points_dataGridView.Name = "points_dataGridView";
            points_dataGridView.Size = new Size(779, 384);
            points_dataGridView.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Result_lbl);
            Controls.Add(textBox1);
            Controls.Add(Export_btn);
            Controls.Add(Calc_btn);
            Controls.Add(LoadFile_btn);
            Controls.Add(Table_pabel);
            Name = "Form1";
            Text = "Form1";
            Table_pabel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)points_dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel Table_pabel;
        private Button LoadFile_btn;
        private Button Calc_btn;
        private Button Export_btn;
        private TextBox textBox1;
        private Label Result_lbl;
        private DataGridView points_dataGridView;
    }
}
