namespace LivingCorner.Gui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            BtnReg = new Button();
            textLastName = new TextBox();
            textName = new TextBox();
            textMonth = new TextBox();
            textData = new TextBox();
            textYear = new TextBox();
            BattDellReg = new Button();
            BtnDeat = new Button();
            BtnVOjid = new Button();
            BtnOrders = new Button();
            BtnStore = new Button();
            SuspendLayout();
            BtnReg.Location = new Point(439, 39);
            BtnReg.Name = "BtnReg";
            BtnReg.Size = new Size(176, 27);
            BtnReg.TabIndex = 0;
            BtnReg.Text = "Реєстрація";
            BtnReg.UseVisualStyleBackColor = true;
            BtnReg.Click += BtnReg_Click;
            textLastName.ForeColor = SystemColors.GrayText;
            textLastName.Location = new Point(230, 39);
            textLastName.Name = "textLastName";
            textLastName.Size = new Size(203, 27);
            textLastName.TabIndex = 1;
            textLastName.Text = "Прізвище";
            textLastName.TextChanged += textLastName_TextChanged;
            textName.Location = new Point(12, 39);
            textName.Name = "textName";
            textName.Size = new Size(212, 27);
            textName.TabIndex = 2;
            textName.Text = "Ім'я";
            textName.TextChanged += textName_TextChanged;
            textMonth.Location = new Point(137, 70);
            textMonth.Name = "textMonth";
            textMonth.Size = new Size(97, 27);
            textMonth.TabIndex = 3;
            textMonth.Text = "ММ";
            textMonth.TextChanged += textBox1_TextChanged;
            textData.Location = new Point(12, 70);
            textData.Name = "textData";
            textData.Size = new Size(97, 27);
            textData.TabIndex = 4;
            textData.Text = "ДД";
            textData.TextChanged += textData_TextChanged;
            textYear.Location = new Point(258, 70);
            textYear.Name = "textYear";
            textYear.Size = new Size(175, 27);
            textYear.TabIndex = 5;
            textYear.Text = "РРРР";
            textYear.TextChanged += textYear_TextChanged;
            BattDellReg.Location = new Point(439, 68);
            BattDellReg.Name = "BattDellReg";
            BattDellReg.Size = new Size(176, 29);
            BattDellReg.TabIndex = 6;
            BattDellReg.Text = "Скасування Реєстрація";
            BattDellReg.UseVisualStyleBackColor = true;
            BattDellReg.Click += BattDellReg_Click;
            BtnDeat.BackgroundImage = (Image)resources.GetObject("BtnDeat.BackgroundImage");
            BtnDeat.BackgroundImageLayout = ImageLayout.Stretch;
            BtnDeat.Location = new Point(12, 193);
            BtnDeat.Name = "BtnDeat";
            BtnDeat.Size = new Size(164, 141);
            BtnDeat.TabIndex = 7;
            BtnDeat.Text = "Мертві";
            BtnDeat.UseVisualStyleBackColor = true;
            BtnDeat.Click += BtnDeat_Click;
            BtnVOjid.BackColor = SystemColors.Control;
            BtnVOjid.BackgroundImage = (Image)resources.GetObject("BtnVOjid.BackgroundImage");
            BtnVOjid.BackgroundImageLayout = ImageLayout.Stretch;
            BtnVOjid.Location = new Point(216, 193);
            BtnVOjid.Name = "BtnVOjid";
            BtnVOjid.Size = new Size(164, 141);
            BtnVOjid.TabIndex = 8;
            BtnVOjid.Text = "В Очікуванні";
            BtnVOjid.UseVisualStyleBackColor = false;
            BtnVOjid.Click += BtnVOjid_Click;
            BtnOrders.BackgroundImage = (Image)resources.GetObject("BtnOrders.BackgroundImage");
            BtnOrders.BackgroundImageLayout = ImageLayout.Stretch;
            BtnOrders.Location = new Point(420, 193);
            BtnOrders.Name = "BtnOrders";
            BtnOrders.Size = new Size(164, 141);
            BtnOrders.TabIndex = 9;
            BtnOrders.Text = "Замовлення";
            BtnOrders.UseVisualStyleBackColor = true;
            BtnOrders.Click += BtnOrders_Click;
            BtnStore.BackgroundImage = (Image)resources.GetObject("BtnStore.BackgroundImage");
            BtnStore.BackgroundImageLayout = ImageLayout.Stretch;
            BtnStore.Location = new Point(624, 193);
            BtnStore.Name = "BtnStore";
            BtnStore.Size = new Size(164, 141);
            BtnStore.TabIndex = 10;
            BtnStore.Text = "Магазин";
            BtnStore.UseVisualStyleBackColor = true;
            BtnStore.Click += BtnStore_Click;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(BtnStore);
            Controls.Add(BtnOrders);
            Controls.Add(BtnVOjid);
            Controls.Add(BtnDeat);
            Controls.Add(BattDellReg);
            Controls.Add(textYear);
            Controls.Add(textData);
            Controls.Add(textMonth);
            Controls.Add(textName);
            Controls.Add(textLastName);
            Controls.Add(BtnReg);
            Name = "Form1";
            Text = "Живий Куточок";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnReg;
        private TextBox textLastName;
        private TextBox textName;
        private TextBox textMonth;
        private TextBox textData;
        private TextBox textYear;
        private Button BattDellReg;
        private Button BtnDeat;
        private Button BtnVOjid;
        private Button BtnOrders;
        private Button BtnStore;
    }
}
