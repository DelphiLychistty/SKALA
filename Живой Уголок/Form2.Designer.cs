namespace Живой_Уголок
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            ButtBekDead = new Button();
            DGVDead = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)DGVDead).BeginInit();
            SuspendLayout();
            ButtBekDead.Location = new Point(12, 404);
            ButtBekDead.Name = "ButtBekDead";
            ButtBekDead.Size = new Size(114, 34);
            ButtBekDead.TabIndex = 0;
            ButtBekDead.Text = "Назад";
            ButtBekDead.UseVisualStyleBackColor = true;
            ButtBekDead.Click += ButtBekDead_Click;
            DGVDead.BackgroundColor = Color.Gray;
            DGVDead.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVDead.Location = new Point(12, 12);
            DGVDead.Name = "DGVDead";
            DGVDead.RowHeadersWidth = 51;
            DGVDead.Size = new Size(561, 386);
            DGVDead.TabIndex = 1;
            DGVDead.CellContentClick += DGVDead_CellContentClick;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(DGVDead);
            Controls.Add(ButtBekDead);
            Name = "Form2";
            Text = "Мертві";
            ((System.ComponentModel.ISupportInitialize)DGVDead).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button ButtBekDead;
        private DataGridView DGVDead;
    }
}