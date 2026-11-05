namespace LivingCorner.Gui
{
    partial class Form4
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
            ButtBekOrders = new Button();
            DGVOrders = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)DGVOrders).BeginInit();
            SuspendLayout();
            ButtBekOrders.Location = new Point(12, 400);
            ButtBekOrders.Name = "ButtBekOrders";
            ButtBekOrders.Size = new Size(107, 38);
            ButtBekOrders.TabIndex = 0;
            ButtBekOrders.Text = "Назад";
            ButtBekOrders.UseVisualStyleBackColor = true;
            ButtBekOrders.Click += ButtBekOrders_Click;
            DGVOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DGVOrders.Location = new Point(12, 12);
            DGVOrders.Name = "DGVOrders";
            DGVOrders.RowHeadersWidth = 51;
            DGVOrders.Size = new Size(776, 375);
            DGVOrders.TabIndex = 1;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DGVOrders);
            Controls.Add(ButtBekOrders);
            Name = "Form4";
            Text = "Замовлення";
            ((System.ComponentModel.ISupportInitialize)DGVOrders).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button ButtBekOrders;
        private DataGridView DGVOrders;
    }
}
