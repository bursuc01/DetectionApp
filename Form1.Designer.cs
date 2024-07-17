namespace WinFormsApp3
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
            detectBtn = new Button();
            pictureBox1 = new PictureBox();
            comboBox2 = new ComboBox();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            selectBtn = new Button();
            coordLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // detectBtn
            // 
            detectBtn.Location = new Point(308, 28);
            detectBtn.Name = "detectBtn";
            detectBtn.Size = new Size(92, 23);
            detectBtn.TabIndex = 0;
            detectBtn.Text = "Detect";
            detectBtn.UseVisualStyleBackColor = true;
            detectBtn.Click += detectBtn_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Cursor = Cursors.Cross;
            pictureBox1.Location = new Point(12, 57);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(560, 562);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "yolov5", "yolov8" });
            comboBox2.Location = new Point(12, 28);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(173, 23);
            comboBox2.TabIndex = 5;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 10);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 8;
            label2.Text = "CNN";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(578, 57);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.Size = new Size(315, 201);
            dataGridView1.TabIndex = 10;
            // 
            // selectBtn
            // 
            selectBtn.Location = new Point(191, 28);
            selectBtn.Name = "selectBtn";
            selectBtn.Size = new Size(111, 23);
            selectBtn.TabIndex = 11;
            selectBtn.Text = "Select Image";
            selectBtn.UseVisualStyleBackColor = true;
            selectBtn.Click += selectBtn_Click;
            // 
            // coordLabel
            // 
            coordLabel.AutoSize = true;
            coordLabel.Location = new Point(578, 32);
            coordLabel.Name = "coordLabel";
            coordLabel.Size = new Size(77, 15);
            coordLabel.TabIndex = 12;
            coordLabel.Text = "Coordinates: ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1066, 641);
            Controls.Add(coordLabel);
            Controls.Add(selectBtn);
            Controls.Add(dataGridView1);
            Controls.Add(label2);
            Controls.Add(comboBox2);
            Controls.Add(pictureBox1);
            Controls.Add(detectBtn);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button detectBtn;
        private PictureBox pictureBox1;
        private ComboBox comboBox2;
        private Label label2;
        private DataGridView dataGridView1;
        private Button selectBtn;
        private Label coordLabel;
    }
}