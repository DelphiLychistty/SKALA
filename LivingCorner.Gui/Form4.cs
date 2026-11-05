using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LivingCorner.Gui
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.Load += Form4_Load;
        }

        private async void Form4_Load(object? sender, EventArgs e)
        {
            DGVOrders.AutoGenerateColumns = true;
            var orders = await ApiClient.GetAsync<List<OrderRowDto>>("/api/orders");
            if (orders != null && orders.Count > 0)
            {
                DGVOrders.DataSource = orders;
            }
            else
            {
                var sample = new List<OrderRowDto>
                {
                    new OrderRowDto { Id = 1, Name = "Іван Саперов", BirthDate = "1990-01-01", Coffin = "Дубовий", Tier = "Обычный", Priest = "Отец Иоанн", Status = "Pending" },
                    new OrderRowDto { Id = 2, Name = "Міша Олігарх", BirthDate = "1985-05-12", Coffin = "Золотой", Tier = "Премиум", Priest = "Архиепископ", Status = "Done" }
                };
                DGVOrders.DataSource = sample;
            }
        }

        private void ButtBekOrders_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is Form1)
                {
                    form.Show();
                    break;
                }
            }
            this.Close();
        }
    }
}
