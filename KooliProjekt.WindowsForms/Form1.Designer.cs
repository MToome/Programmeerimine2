namespace KooliProjekt.WindowsForms
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
            CustomerGridView1 = new DataGridView();
            NameBox = new TextBox();
            AddressBox = new TextBox();
            CityBox = new TextBox();
            PhoneBox = new TextBox();
            EmailBox = new TextBox();
            SubmitBtn = new Button();
            DicountUpDown = new NumericUpDown();
            DeleteBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)CustomerGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DicountUpDown).BeginInit();
            SuspendLayout();
            // 
            // CustomerGridView1
            // 
            CustomerGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CustomerGridView1.Dock = DockStyle.Top;
            CustomerGridView1.Location = new Point(0, 0);
            CustomerGridView1.Name = "CustomerGridView1";
            CustomerGridView1.Size = new Size(800, 150);
            CustomerGridView1.TabIndex = 0;
            CustomerGridView1.CellContentClick += CustomerGridView1_CellContentClick;
            // 
            // NameBox
            // 
            NameBox.Location = new Point(12, 219);
            NameBox.Name = "NameBox";
            NameBox.PlaceholderText = "Enter name";
            NameBox.Size = new Size(201, 23);
            NameBox.TabIndex = 1;
            NameBox.TextChanged += NameBox_TextChanged;
            // 
            // AddressBox
            // 
            AddressBox.Location = new Point(12, 248);
            AddressBox.Name = "AddressBox";
            AddressBox.PlaceholderText = "Enter address";
            AddressBox.Size = new Size(201, 23);
            AddressBox.TabIndex = 2;
            AddressBox.TextChanged += AddressBox_TextChanged;
            // 
            // CityBox
            // 
            CityBox.Location = new Point(12, 277);
            CityBox.Name = "CityBox";
            CityBox.PlaceholderText = "Enter city";
            CityBox.Size = new Size(201, 23);
            CityBox.TabIndex = 3;
            CityBox.TextChanged += CityBox_TextChanged;
            // 
            // PhoneBox
            // 
            PhoneBox.Location = new Point(12, 306);
            PhoneBox.Name = "PhoneBox";
            PhoneBox.PlaceholderText = "Enter phone number";
            PhoneBox.Size = new Size(201, 23);
            PhoneBox.TabIndex = 4;
            PhoneBox.TextChanged += PhoneBox_TextChanged;
            // 
            // EmailBox
            // 
            EmailBox.Location = new Point(12, 335);
            EmailBox.Name = "EmailBox";
            EmailBox.PlaceholderText = "Enter email";
            EmailBox.Size = new Size(201, 23);
            EmailBox.TabIndex = 5;
            EmailBox.TextChanged += EmailBox_TextChanged;
            // 
            // SubmitBtn
            // 
            SubmitBtn.Location = new Point(38, 393);
            SubmitBtn.Name = "SubmitBtn";
            SubmitBtn.Size = new Size(75, 23);
            SubmitBtn.TabIndex = 7;
            SubmitBtn.Text = "Submit";
            SubmitBtn.UseVisualStyleBackColor = true;
            SubmitBtn.Click += SubmitBtn_Click;
            // 
            // DicountUpDown
            // 
            DicountUpDown.DecimalPlaces = 1;
            DicountUpDown.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            DicountUpDown.Location = new Point(38, 364);
            DicountUpDown.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            DicountUpDown.Name = "DicountUpDown";
            DicountUpDown.Size = new Size(120, 23);
            DicountUpDown.TabIndex = 8;
            DicountUpDown.ValueChanged += DicountUpDown_ValueChanged;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Location = new Point(138, 393);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(75, 23);
            DeleteBtn.TabIndex = 9;
            DeleteBtn.Text = "Delete";
            DeleteBtn.UseVisualStyleBackColor = true;
            DeleteBtn.Click += DeleteBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DeleteBtn);
            Controls.Add(DicountUpDown);
            Controls.Add(SubmitBtn);
            Controls.Add(EmailBox);
            Controls.Add(PhoneBox);
            Controls.Add(CityBox);
            Controls.Add(AddressBox);
            Controls.Add(NameBox);
            Controls.Add(CustomerGridView1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Form1";
            Text = "Eshop";
            ((System.ComponentModel.ISupportInitialize)CustomerGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)DicountUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView CustomerGridView1;
        private TextBox NameBox;
        private TextBox AddressBox;
        private TextBox CityBox;
        private TextBox PhoneBox;
        private TextBox EmailBox;
        private Button SubmitBtn;
        private NumericUpDown DicountUpDown;
        private Button DeleteBtn;
    }
}
