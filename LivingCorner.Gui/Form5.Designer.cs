namespace LivingCore.Gui
{
    partial class Form5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            ButtBekStore = new Button();
            ButMany = new Button();
            lblChop = new Label();
            ButBuyGrob = new Button();
            ButBuySvat = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // ButtBekStore
            // 
            ButtBekStore.BackgroundImage = (Image)resources.GetObject("ButtBekStore.BackgroundImage");
            ButtBekStore.BackgroundImageLayout = ImageLayout.Stretch;
            ButtBekStore.Location = new Point(12, 402);
            ButtBekStore.Name = "ButtBekStore";
            ButtBekStore.Size = new Size(106, 36);
            ButtBekStore.TabIndex = 0;
            ButtBekStore.Text = "Назад";
            ButtBekStore.UseVisualStyleBackColor = true;
            ButtBekStore.Click += ButtBekStore_Click;
            // 
            // ButMany
            // 
            ButMany.BackgroundImage = (Image)resources.GetObject("ButMany.BackgroundImage");
            ButMany.BackgroundImageLayout = ImageLayout.Stretch;
            ButMany.Location = new Point(627, 381);
            ButMany.Name = "ButMany";
            ButMany.Size = new Size(161, 57);
            ButMany.TabIndex = 1;
            ButMany.Text = "Заробити гроші";
            ButMany.UseVisualStyleBackColor = true;
            ButMany.Click += ButMany_Click;
            // 
            // lblChop
            // 
            lblChop.Location = new Point(12, 9);
            lblChop.Name = "lblChop";
            lblChop.Size = new Size(85, 25);
            lblChop.TabIndex = 2;
            lblChop.Text = "$:";
            lblChop.Click += lblChop_Click;
            // 
            // ButBuyGrob
            // 
            ButBuyGrob.BackgroundImage = (Image)resources.GetObject("ButBuyGrob.BackgroundImage");
            ButBuyGrob.BackgroundImageLayout = ImageLayout.Stretch;
            ButBuyGrob.Location = new Point(189, 64);
            ButBuyGrob.Name = "ButBuyGrob";
            ButBuyGrob.Size = new Size(172, 172);
            ButBuyGrob.TabIndex = 3;
            ButBuyGrob.UseVisualStyleBackColor = true;
            ButBuyGrob.Click += ButBuyGrob_Click;
            // 
            // ButBuySvat
            // 
            ButBuySvat.BackgroundImage = (Image)resources.GetObject("ButBuySvat.BackgroundImage");
            ButBuySvat.BackgroundImageLayout = ImageLayout.Stretch;
            ButBuySvat.Location = new Point(441, 64);
            ButBuySvat.Name = "ButBuySvat";
            ButBuySvat.Size = new Size(172, 172);
            ButBuySvat.TabIndex = 4;
            ButBuySvat.UseVisualStyleBackColor = true;
            ButBuySvat.Click += ButBuySvat_Click;
            // 
            // label1
            // 
            label1.Location = new Point(189, 239);
            label1.Name = "label1";
            label1.Size = new Size(172, 27);
            label1.TabIndex = 5;
            label1.Text = "Купити гроб";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.Location = new Point(441, 239);
            label2.Name = "label2";
            label2.Size = new Size(172, 27);
            label2.TabIndex = 6;
            label2.Text = "Купити священика";
            label2.Click += label2_Click;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}