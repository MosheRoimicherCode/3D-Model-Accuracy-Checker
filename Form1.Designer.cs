namespace _3D_Model_Accuracy_Checker
{
    partial class Main
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
            points_dataGridView = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            Delete = new DataGridViewTextBoxColumn();
            OriginalX = new DataGridViewTextBoxColumn();
            OriginalY = new DataGridViewTextBoxColumn();
            OriginalZ = new DataGridViewTextBoxColumn();
            MeasureX = new DataGridViewTextBoxColumn();
            MeasureY = new DataGridViewTextBoxColumn();
            MeasureZ = new DataGridViewTextBoxColumn();
            OffsetX = new DataGridViewTextBoxColumn();
            OffsetY = new DataGridViewTextBoxColumn();
            OffsetZ = new DataGridViewTextBoxColumn();
            OffMinusAverage_X = new DataGridViewTextBoxColumn();
            OffMinusAverage_Y = new DataGridViewTextBoxColumn();
            OffMinusAverage_Z = new DataGridViewTextBoxColumn();
            LoadFile_btn = new Button();
            Calc_btn = new Button();
            Export_btn = new Button();
            resultTxtBox = new TextBox();
            Result_lbl = new Label();
            shapefile_btn = new Button();
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
            // points_dataGridView
            // 
            points_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            points_dataGridView.Columns.AddRange(new DataGridViewColumn[] { ID, Delete, OriginalX, OriginalY, OriginalZ, MeasureX, MeasureY, MeasureZ, OffsetX, OffsetY, OffsetZ, OffMinusAverage_X, OffMinusAverage_Y, OffMinusAverage_Z });
            points_dataGridView.Location = new Point(0, 0);
            points_dataGridView.Name = "points_dataGridView";
            points_dataGridView.Size = new Size(779, 384);
            points_dataGridView.TabIndex = 0;
            points_dataGridView.CellClick += points_dataGridView_CellClick;
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.Name = "ID";
            // 
            // Delete
            // 
            Delete.HeaderText = "Delete Point";
            Delete.Name = "Delete";
            // 
            // OriginalX
            // 
            OriginalX.HeaderText = "OriginalX";
            OriginalX.Name = "OriginalX";
            OriginalX.ReadOnly = true;
            OriginalX.Visible = false;
            // 
            // OriginalY
            // 
            OriginalY.HeaderText = "OriginalY";
            OriginalY.Name = "OriginalY";
            OriginalY.ReadOnly = true;
            OriginalY.Visible = false;
            // 
            // OriginalZ
            // 
            OriginalZ.HeaderText = "OriginalZ";
            OriginalZ.Name = "OriginalZ";
            OriginalZ.ReadOnly = true;
            OriginalZ.Visible = false;
            // 
            // MeasureX
            // 
            MeasureX.HeaderText = "MeasureX";
            MeasureX.Name = "MeasureX";
            MeasureX.ReadOnly = true;
            MeasureX.Visible = false;
            // 
            // MeasureY
            // 
            MeasureY.HeaderText = "MeasureY";
            MeasureY.Name = "MeasureY";
            MeasureY.ReadOnly = true;
            MeasureY.Visible = false;
            // 
            // MeasureZ
            // 
            MeasureZ.HeaderText = "MeasureZ";
            MeasureZ.Name = "MeasureZ";
            MeasureZ.ReadOnly = true;
            MeasureZ.Visible = false;
            // 
            // OffsetX
            // 
            OffsetX.HeaderText = "OffsetX";
            OffsetX.Name = "OffsetX";
            // 
            // OffsetY
            // 
            OffsetY.HeaderText = "OffsetY";
            OffsetY.Name = "OffsetY";
            // 
            // OffsetZ
            // 
            OffsetZ.HeaderText = "OffsetZ";
            OffsetZ.Name = "OffsetZ";
            // 
            // OffMinusAverage_X
            // 
            OffMinusAverage_X.HeaderText = "OffMinusAverage_X";
            OffMinusAverage_X.Name = "OffMinusAverage_X";
            OffMinusAverage_X.Visible = false;
            // 
            // OffMinusAverage_Y
            // 
            OffMinusAverage_Y.HeaderText = "OffMinusAverage_Y";
            OffMinusAverage_Y.Name = "OffMinusAverage_Y";
            OffMinusAverage_Y.Visible = false;
            // 
            // OffMinusAverage_Z
            // 
            OffMinusAverage_Z.HeaderText = "OffMinusAverage_Z";
            OffMinusAverage_Z.Name = "OffMinusAverage_Z";
            OffMinusAverage_Z.Visible = false;
            // 
            // LoadFile_btn
            // 
            LoadFile_btn.Enabled = false;
            LoadFile_btn.Location = new Point(127, 11);
            LoadFile_btn.Name = "LoadFile_btn";
            LoadFile_btn.Size = new Size(101, 39);
            LoadFile_btn.TabIndex = 0;
            LoadFile_btn.Text = "Load File";
            LoadFile_btn.UseVisualStyleBackColor = true;
            LoadFile_btn.Click += LoadFile_btn_Click;
            // 
            // Calc_btn
            // 
            Calc_btn.Enabled = false;
            Calc_btn.Location = new Point(385, 11);
            Calc_btn.Name = "Calc_btn";
            Calc_btn.Size = new Size(96, 39);
            Calc_btn.TabIndex = 1;
            Calc_btn.Text = "Calculate";
            Calc_btn.UseVisualStyleBackColor = true;
            Calc_btn.Click += Calc_btn_Click;
            // 
            // Export_btn
            // 
            Export_btn.Location = new Point(685, 12);
            Export_btn.Name = "Export_btn";
            Export_btn.Size = new Size(103, 39);
            Export_btn.TabIndex = 2;
            Export_btn.Text = "Export Pdf";
            Export_btn.UseVisualStyleBackColor = true;
            Export_btn.Click += Export_btn_Click;
            // 
            // resultTxtBox
            // 
            resultTxtBox.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            resultTxtBox.Location = new Point(571, 12);
            resultTxtBox.Multiline = true;
            resultTxtBox.Name = "resultTxtBox";
            resultTxtBox.Size = new Size(108, 39);
            resultTxtBox.TabIndex = 3;
            // 
            // Result_lbl
            // 
            Result_lbl.AutoSize = true;
            Result_lbl.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Result_lbl.Location = new Point(487, 18);
            Result_lbl.Name = "Result_lbl";
            Result_lbl.Size = new Size(78, 32);
            Result_lbl.TabIndex = 4;
            Result_lbl.Text = "Result";
            // 
            // shapefile_btn
            // 
            shapefile_btn.Location = new Point(12, 11);
            shapefile_btn.Name = "shapefile_btn";
            shapefile_btn.Size = new Size(109, 40);
            shapefile_btn.TabIndex = 5;
            shapefile_btn.Text = "Create ShapeFile";
            shapefile_btn.UseVisualStyleBackColor = true;
            shapefile_btn.Click += shapefile_btn_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(shapefile_btn);
            Controls.Add(Result_lbl);
            Controls.Add(resultTxtBox);
            Controls.Add(Export_btn);
            Controls.Add(Calc_btn);
            Controls.Add(LoadFile_btn);
            Controls.Add(Table_pabel);
            Name = "Main";
            Text = "Accuracy check - Kav Medida";
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
        private TextBox resultTxtBox;
        private Label Result_lbl;
        private DataGridView points_dataGridView;
        private Button shapefile_btn;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Delete;
        private DataGridViewTextBoxColumn OriginalX;
        private DataGridViewTextBoxColumn OriginalY;
        private DataGridViewTextBoxColumn OriginalZ;
        private DataGridViewTextBoxColumn MeasureX;
        private DataGridViewTextBoxColumn MeasureY;
        private DataGridViewTextBoxColumn MeasureZ;
        private DataGridViewTextBoxColumn OffsetX;
        private DataGridViewTextBoxColumn OffsetY;
        private DataGridViewTextBoxColumn OffsetZ;
        private DataGridViewTextBoxColumn OffMinusAverage_X;
        private DataGridViewTextBoxColumn OffMinusAverage_Y;
        private DataGridViewTextBoxColumn OffMinusAverage_Z;
    }
}
