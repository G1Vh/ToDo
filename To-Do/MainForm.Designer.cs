namespace To_Do
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ToDoList = new System.Windows.Forms.DataGridView();
            this.modifyButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.repeatBox = new System.Windows.Forms.ComboBox();
            this.deadlineCalendar = new System.Windows.Forms.MonthCalendar();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.addBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.showAllButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.fromTextBox = new System.Windows.Forms.MaskedTextBox();
            this.toTextBox = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ToDoList)).BeginInit();
            this.addBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToDoList
            // 
            this.ToDoList.AllowUserToAddRows = false;
            this.ToDoList.AllowUserToDeleteRows = false;
            this.ToDoList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ToDoList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ToDoList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ToDoList.Location = new System.Drawing.Point(113, 25);
            this.ToDoList.MultiSelect = false;
            this.ToDoList.Name = "ToDoList";
            this.ToDoList.ReadOnly = true;
            this.ToDoList.RowHeadersVisible = false;
            this.ToDoList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ToDoList.Size = new System.Drawing.Size(358, 236);
            this.ToDoList.TabIndex = 0;
            // 
            // modifyButton
            // 
            this.modifyButton.Location = new System.Drawing.Point(12, 42);
            this.modifyButton.Name = "modifyButton";
            this.modifyButton.Size = new System.Drawing.Size(91, 23);
            this.modifyButton.TabIndex = 2;
            this.modifyButton.Text = "Изменить";
            this.modifyButton.UseVisualStyleBackColor = true;
            this.modifyButton.Click += new System.EventHandler(this.modifyButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(12, 71);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(91, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(12, 193);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(91, 23);
            this.filterButton.TabIndex = 4;
            this.filterButton.Text = "Фильтр";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Периодичность";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Крайний срок";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Наименование";
            // 
            // repeatBox
            // 
            this.repeatBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.repeatBox.FormattingEnabled = true;
            this.repeatBox.Items.AddRange(new object[] {
            "Нет",
            "Каждый день",
            "Каждую неделю",
            "Каждый месяц"});
            this.repeatBox.Location = new System.Drawing.Point(66, 123);
            this.repeatBox.MaxLength = 13;
            this.repeatBox.Name = "repeatBox";
            this.repeatBox.Size = new System.Drawing.Size(121, 21);
            this.repeatBox.TabIndex = 12;
            // 
            // deadlineCalendar
            // 
            this.deadlineCalendar.Location = new System.Drawing.Point(253, 72);
            this.deadlineCalendar.MaxSelectionCount = 1;
            this.deadlineCalendar.Name = "deadlineCalendar";
            this.deadlineCalendar.TabIndex = 11;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(11, 72);
            this.nameTextBox.MaxLength = 100;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(230, 20);
            this.nameTextBox.TabIndex = 10;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(89, 194);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 8;
            this.addButton.Text = "Добавить";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // addBox
            // 
            this.addBox.Controls.Add(this.nameTextBox);
            this.addBox.Controls.Add(this.addButton);
            this.addBox.Controls.Add(this.deadlineCalendar);
            this.addBox.Controls.Add(this.label2);
            this.addBox.Controls.Add(this.label3);
            this.addBox.Controls.Add(this.repeatBox);
            this.addBox.Controls.Add(this.label1);
            this.addBox.Location = new System.Drawing.Point(477, -1);
            this.addBox.Name = "addBox";
            this.addBox.Size = new System.Drawing.Size(429, 262);
            this.addBox.TabIndex = 18;
            this.addBox.TabStop = false;
            this.addBox.Text = "Добавление дела";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(245, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Список дел";
            // 
            // showAllButton
            // 
            this.showAllButton.Location = new System.Drawing.Point(12, 226);
            this.showAllButton.Name = "showAllButton";
            this.showAllButton.Size = new System.Drawing.Size(91, 23);
            this.showAllButton.TabIndex = 19;
            this.showAllButton.Text = "Показать все";
            this.showAllButton.UseVisualStyleBackColor = true;
            this.showAllButton.Click += new System.EventHandler(this.showAllButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Начальная дата";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Конечная дата";
            // 
            // fromTextBox
            // 
            this.fromTextBox.Location = new System.Drawing.Point(29, 133);
            this.fromTextBox.Mask = "00.00.0000";
            this.fromTextBox.Name = "fromTextBox";
            this.fromTextBox.Size = new System.Drawing.Size(62, 20);
            this.fromTextBox.TabIndex = 21;
            // 
            // toTextBox
            // 
            this.toTextBox.Location = new System.Drawing.Point(28, 167);
            this.toTextBox.Mask = "00.00.0000";
            this.toTextBox.Name = "toTextBox";
            this.toTextBox.Size = new System.Drawing.Size(62, 20);
            this.toTextBox.TabIndex = 22;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 261);
            this.Controls.Add(this.toTextBox);
            this.Controls.Add(this.fromTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.showAllButton);
            this.Controls.Add(this.addBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.modifyButton);
            this.Controls.Add(this.ToDoList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Список дел";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ToDoList)).EndInit();
            this.addBox.ResumeLayout(false);
            this.addBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ToDoList;
        private System.Windows.Forms.Button modifyButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox repeatBox;
        private System.Windows.Forms.MonthCalendar deadlineCalendar;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.GroupBox addBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button showAllButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox fromTextBox;
        private System.Windows.Forms.MaskedTextBox toTextBox;
    }
}

