namespace LivingCorner.Gui
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            DGVVOjid = new DataGridView();
            BtnBackVOjid = new Button();
            ((System.ComponentModel.ISupportInitialize)DGVVOjid).BeginInit();
            SuspendLayout();
            DGVVOjid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVVOjid.Location = new Point(12, 12);
            DGVVOjid.Name = "DGVVOjid";
            DGVVOjid.RowHeadersWidth = 51;
            DGVVOjid.Size = new Size(557, 385);
            DGVVOjid.TabIndex = 0;
            DGVVOjid.CellContentClick += DGVVOjid_CellContentClick;
            BtnBackVOjid.Location = new Point(12, 403);
            BtnBackVOjid.Name = "BtnBackVOjid";
            BtnBackVOjid.Size = new Size(115, 35);
            BtnBackVOjid.TabIndex = 1;
            BtnBackVOjid.Text = "Назад";
            BtnBackVOjid.UseVisualStyleBackColor = true;
            BtnBackVOjid.Click += BtnBackVOjid_Click;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(BtnBackVOjid);
            Controls.Add(DGVVOjid);
            DoubleBuffered = true;
            Name = "Form3";
            Text = "В Очікуванні";
            ((System.ComponentModel.ISupportInitialize)DGVVOjid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView DGVVOjid;
        private Button BtnBackVOjid;
    }
}