namespace Exam
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
            button1 = new Button();
            button3 = new Button();
            dataGridView1 = new DataGridView();
            RefreshButton = new Button();
            dataGridView2 = new DataGridView();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(629, 134);
            button1.Name = "button1";
            button1.Size = new Size(200, 35);
            button1.TabIndex = 7;
            button1.Text = "Get PDF invoice";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button3
            // 
            button3.Location = new Point(629, 22);
            button3.Name = "button3";
            button3.Size = new Size(200, 34);
            button3.TabIndex = 10;
            button3.Text = "Invoice selection";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(20, 22);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 49;
            dataGridView1.RowTemplate.Height = 28;
            dataGridView1.Size = new Size(286, 384);
            dataGridView1.TabIndex = 13;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // RefreshButton
            // 
            RefreshButton.Location = new Point(629, 73);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(200, 35);
            RefreshButton.TabIndex = 14;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(334, 22);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 49;
            dataGridView2.RowTemplate.Height = 28;
            dataGridView2.Size = new Size(268, 384);
            dataGridView2.TabIndex = 15;
            // 
            // button4
            // 
            button4.Location = new Point(629, 371);
            button4.Name = "button4";
            button4.Size = new Size(200, 35);
            button4.TabIndex = 17;
            button4.Text = "Add a customer and items";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click_2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 416);
            Controls.Add(button4);
            Controls.Add(dataGridView2);
            Controls.Add(RefreshButton);
            Controls.Add(dataGridView1);
            Controls.Add(button3);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ListBox combobox1;
        private Button button1;
        private Button button3;
        private DataGridView dataGridView1;
        private Button RefreshButton;
        private DataGridView dataGridView2;
        private Button button4;
    }
}