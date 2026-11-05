namespace Живой_Уголок
{
    partial class Form4
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
            ButtBekOrders = new Button();
            SuspendLayout();
            // 
            // ButtBekOrders
            // 
            ButtBekOrders.Location = new Point(12, 400);
            ButtBekOrders.Name = "ButtBekOrders";
            ButtBekOrders.Size = new Size(107, 38);
            ButtBekOrders.TabIndex = 0;
            ButtBekOrders.Text = "Назад";
            ButtBekOrders.UseVisualStyleBackColor = true;
            ButtBekOrders.Click += ButtBekOrders_Click;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ButtBekOrders);
            Name = "Form4";
            Text = "Замовлення";
            ResumeLayout(false);
        }

        #endregion

        private Button ButtBekOrders;
    }
}