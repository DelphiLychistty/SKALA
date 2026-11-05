namespace LivingCore.Gui
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
            ButtReg = new Button();
            textLastName = new TextBox();
            textName = new TextBox();
            textMonth = new TextBox();
            textData = new TextBox();
            textYear = new TextBox();
            BattDellReg = new Button();
            ButtDeat = new Button();
            ButtVOjid = new Button();
            ButtOrders = new Button();
            ButtStore = new Button();
            SuspendLayout();
            // 
            // ButtReg
            // 
            ButtReg.Location = new Point(439, 39);
            ButtReg.Name = "ButtReg";
            ButtReg.Size = new Size(176, 27);
            ButtReg.TabIndex = 0;
            ButtReg.Text = "Реєстрація";
            ButtReg.UseVisualStyleBackColor = true;
            ButtReg.Click += ButtReg_Click;
            // 
            // textLastName
            // 
            textLastName.ForeColor = SystemColors.GrayText;
            textLastName.Location = new Point(230, 39);
            textLastName.Name = "textLastName";
            textLastName.Size = new Size(203, 27);
            textLastName.TabIndex = 1;
            textLastName.Text = "Прізвище";
            textLastName.TextChanged += textLastName_TextChanged;
            // 
            // textName
            // 
            textName.Location = new Point(12, 39);
            textName.Name = "textName";
            textName.Size = new Size(212, 27);
            textName.TabIndex = 2;
            textName.Text = "Ім'я";
            textName.TextChanged += textName_TextChanged;
            // 
            // textMonth
            // 
            textMonth.Location = new Point(137, 70);
            textMonth.Name = "textMonth";
            textMonth.Size = new Size(97, 27);
            textMonth.TabIndex = 3;
            textMonth.Text = "ММ";
            textMonth.TextChanged += textBox1_TextChanged;
            // 
            // textData
            // 
            textData.Location = new Point(12, 70);
            textData.Name = "textData";
            textData.Size = new Size(97, 27);
            textData.TabIndex = 4;
            textData.Text = "ДД";
            textData.TextChanged += textData_TextChanged;
            // 
            // textYear
            // 
            textYear.Location = new Point(258, 70);
            textYear.Name = "textYear";
            textYear.Size = new Size(175, 27);
            textYear.TabIndex = 5;
            textYear.Text = "РРРР";
            textYear.TextChanged += textYear_TextChanged;
            // 
            // BattDellReg
            // 
            BattDellReg.Location = new Point(439, 68);
            BattDellReg.Name = "BattDellReg";
            BattDellReg.Size = new Size(176, 29);
            BattDellReg.TabIndex = 6;
            BattDellReg.Text = "Скасування Реєстрація";
            BattDellReg.UseVisualStyleBackColor = true;
            BattDellReg.Click += BattDellReg_Click;
            // 
            // ButtDeat
            // 
            ButtDeat.BackgroundImage = (Image)resources.GetObject("ButtDeat.BackgroundImage");
            ButtDeat.BackgroundImageLayout = ImageLayout.Stretch;
            ButtDeat.Location = new Point(12, 193);
            ButtDeat.Name = "ButtDeat";
            ButtDeat.Size = new Size(164, 141);
            ButtDeat.TabIndex = 7;
            ButtDeat.Text = "Мертві";
            ButtDeat.UseVisualStyleBackColor = true;
            ButtDeat.Click += ButtDeat_Click;
            // 
            // ButtVOjid
            // 
            ButtVOjid.BackColor = SystemColors.Control;
            ButtVOjid.BackgroundImage = (Image)resources.GetObject("ButtVOjid.BackgroundImage");
            ButtVOjid.BackgroundImageLayout = ImageLayout.Stretch;
            ButtVOjid.Location = new Point(216, 193);
            ButtVOjid.Name = "ButtVOjid";
            ButtVOjid.Size = new Size(164, 141);
            ButtVOjid.TabIndex = 8;
            ButtVOjid.Text = "В Очікуванні";
            ButtVOjid.UseVisualStyleBackColor = false;
            ButtVOjid.Click += ButtVOjid_Click;
            // 
            // ButtOrders
            // 
            ButtOrders.BackgroundImage = (Image)resources.GetObject("ButtOrders.BackgroundImage");
            ButtOrders.BackgroundImageLayout = ImageLayout.Stretch;
            ButtOrders.Location = new Point(420, 193);
            ButtOrders.Name = "ButtOrders";
            ButtOrders.Size = new Size(164, 141);
            ButtOrders.TabIndex = 9;
            ButtOrders.Text = "Замовлення";
            ButtOrders.UseVisualStyleBackColor = true;
            ButtOrders.Click += ButtOrders_Click;
            // 
            // ButtStore
            // 
            ButtStore.BackgroundImage = (Image)resources.GetObject("ButtStore.BackgroundImage");
            ButtStore.BackgroundImageLayout = ImageLayout.Stretch;
            ButtStore.Location = new Point(624, 193);
            ButtStore.Name = "ButtStore";
            ButtStore.Size = new Size(164, 141);
            ButtStore.TabIndex = 10;
            ButtStore.Text = "Магазин";
            ButtStore.UseVisualStyleBackColor = true;
            ButtStore.Click += ButtStore_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(ButtStore);
            Controls.Add(ButtOrders);
            Controls.Add(ButtVOjid);
            Controls.Add(ButtDeat);
            Controls.Add(BattDellReg);
            Controls.Add(textYear);
            Controls.Add(textData);
            Controls.Add(textMonth);
            Controls.Add(textName);
            Controls.Add(textLastName);
            Controls.Add(ButtReg);
            Name = "Form1";
            Text = "Живий Куточок";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ButtReg;
        private TextBox textLastName;
        private TextBox textName;
        private TextBox textMonth;
        private TextBox textData;
        private TextBox textYear;
        private Button BattDellReg;
        private Button ButtDeat;
        private Button ButtVOjid;
        private Button ButtOrders;
        private Button ButtStore;
    }
}
