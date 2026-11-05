namespace LivingCorner.Gui
{
    partial class Form5
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            ButtBekStore = new Button();
            ButMany = new Button();
            lblChop = new Label();
            ButBuyGrob = new Button();
            ButBuySvat = new Button();
            label1 = new Label();
            label2 = new Label();
            comboCoffin = new ComboBox();
            comboPriest = new ComboBox();
            comboTier = new ComboBox();
            labelTier = new Label();
            btnCheckout = new Button();
            SuspendLayout();
            ButtBekStore.BackgroundImage = (Image)resources.GetObject("ButtBekStore.BackgroundImage");
            ButtBekStore.BackgroundImageLayout = ImageLayout.Stretch;
            ButtBekStore.Location = new Point(12, 402);
            ButtBekStore.Name = "ButtBekStore";
            ButtBekStore.Size = new Size(106, 36);
            ButtBekStore.TabIndex = 0;
            ButtBekStore.Text = "Назад";
            ButtBekStore.UseVisualStyleBackColor = true;
            ButtBekStore.Click += ButtBekStore_Click;
            ButMany.BackgroundImage = (Image)resources.GetObject("ButMany.BackgroundImage");
            ButMany.BackgroundImageLayout = ImageLayout.Stretch;
            ButMany.Location = new Point(627, 381);
            ButMany.Name = "ButMany";
            ButMany.Size = new Size(161, 57);
            ButMany.TabIndex = 1;
            ButMany.Text = "Заробити гроші";
            ButMany.UseVisualStyleBackColor = true;
            ButMany.Click += ButMany_Click;
            lblChop.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblChop.Location = new Point(12, 9);
            lblChop.Name = "lblChop";
            lblChop.Size = new Size(300, 35);
            lblChop.TabIndex = 2;
            lblChop.Text = "$: 0";
            ButBuyGrob.BackgroundImage = (Image)resources.GetObject("ButBuyGrob.BackgroundImage");
            ButBuyGrob.BackgroundImageLayout = ImageLayout.Stretch;
            ButBuyGrob.Location = new Point(70, 60);
            ButBuyGrob.Name = "ButBuyGrob";
            ButBuyGrob.Size = new Size(160, 150);
            ButBuyGrob.TabIndex = 3;
            ButBuyGrob.UseVisualStyleBackColor = true;
            ButBuySvat.BackgroundImage = (Image)resources.GetObject("ButBuySvat.BackgroundImage");
            ButBuySvat.BackgroundImageLayout = ImageLayout.Stretch;
            ButBuySvat.Location = new Point(310, 60);
            ButBuySvat.Name = "ButBuySvat";
            ButBuySvat.Size = new Size(160, 150);
            ButBuySvat.TabIndex = 4;
            ButBuySvat.UseVisualStyleBackColor = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(70, 218);
            label1.Name = "label1";
            label1.Size = new Size(160, 25);
            label1.TabIndex = 5;
            label1.Text = "Оберіть труну:";
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(310, 218);
            label2.Name = "label2";
            label2.Size = new Size(160, 25);
            label2.TabIndex = 6;
            label2.Text = "Оберіть батюшку:";
            comboCoffin.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCoffin.FormattingEnabled = true;
            comboCoffin.Location = new Point(70, 246);
            comboCoffin.Name = "comboCoffin";
            comboCoffin.Size = new Size(180, 28);
            comboCoffin.TabIndex = 7;
            comboPriest.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPriest.FormattingEnabled = true;
            comboPriest.Location = new Point(310, 246);
            comboPriest.Name = "comboPriest";
            comboPriest.Size = new Size(180, 28);
            comboPriest.TabIndex = 8;
            labelTier.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTier.Location = new Point(540, 218);
            labelTier.Name = "labelTier";
            labelTier.Size = new Size(160, 25);
            labelTier.TabIndex = 9;
            labelTier.Text = "Рівень похорону:";
            comboTier.DropDownStyle = ComboBoxStyle.DropDownList;
            comboTier.FormattingEnabled = true;
            comboTier.Location = new Point(540, 246);
            comboTier.Name = "comboTier";
            comboTier.Size = new Size(180, 28);
            comboTier.TabIndex = 10;
            btnCheckout.BackColor = Color.Gold;
            btnCheckout.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCheckout.Location = new Point(240, 310);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(300, 48);
            btnCheckout.TabIndex = 11;
            btnCheckout.Text = "Оформити ритуальний пакет";
            btnCheckout.UseVisualStyleBackColor = false;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCheckout);
            Controls.Add(comboTier);
            Controls.Add(labelTier);
            Controls.Add(comboPriest);
            Controls.Add(comboCoffin);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(ButBuySvat);
            Controls.Add(ButBuyGrob);
            Controls.Add(lblChop);
            Controls.Add(ButMany);
            Controls.Add(ButtBekStore);
            Name = "Form5";
            Text = "Магазин";
            Load += Form5_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button ButtBekStore;
        private Button ButMany;
        private Label lblChop;
        private Button ButBuyGrob;
        private Button ButBuySvat;
        private Label label1;
        private Label label2;
        private ComboBox comboCoffin;
        private ComboBox comboPriest;
        private ComboBox comboTier;
        private Label labelTier;
        private Button btnCheckout;
    }
}
