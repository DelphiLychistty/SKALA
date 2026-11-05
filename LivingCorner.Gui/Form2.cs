using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LivingCorner.Gui
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.Load += Form2_Load;
        }

        private async void Form2_Load(object? sender, EventArgs e)
        {
            DGVDead.AutoGenerateColumns = true;
            var deadList = await ApiClient.GetAsync<List<DeadRowDto>>("/api/dead");
            if (deadList != null && deadList.Count > 0)
            {
                DGVDead.DataSource = deadList;
            }
            else
            {
                var sample = new List<DeadRowDto>
                {
                    new DeadRowDto { Name = "Дід Панас", BirthDate = "1945-05-09", DeathDate = "2026-11-01", Tier = "Премиум" },
                    new DeadRowDto { Name = "Кент Саня", BirthDate = "2000-01-01", DeathDate = "2026-11-04", Tier = "Обычный" },
                    new DeadRowDto { Name = "Невідомий копач", BirthDate = "1999-12-31", DeathDate = "2026-11-05", Tier = "Бесплатный" }
                };
                DGVDead.DataSource = sample;
            }
        }

        private void ButtBekDead_Click(object sender, EventArgs e)
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

        private void DGVDead_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
