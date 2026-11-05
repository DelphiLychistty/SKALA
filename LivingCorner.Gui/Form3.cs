using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LivingCore.Gui
{
    public partial class Form3 : Form
    {
        private Button? buttBury;

        public Form3()
        {
            InitializeComponent();
            SetupBuryButton();
            this.Load += Form3_Load;
        }

        private void SetupBuryButton()
        {
            buttBury = new Button
            {
                Text = "Поховати клієнта",
                Location = new Point(140, 403),
                Size = new Size(160, 35),
                UseVisualStyleBackColor = true
            };
            buttBury.Click += ButtBury_Click;
            this.Controls.Add(buttBury);
        }

        private async void Form3_Load(object? sender, EventArgs e)
        {
            await LoadWaiting();
        }

        private async System.Threading.Tasks.Task LoadWaiting()
        {
            DGVVOjid.AutoGenerateColumns = true;
            var list = await ApiClient.GetAsync<List<WaitingRowDto>>("/api/waiting");
            if (list != null && list.Count > 0)
            {
                DGVVOjid.DataSource = list;
            }
            else
            {
                var sample = new List<WaitingRowDto>
                {
                    new WaitingRowDto { PersonId = 1, Name = "Вася Сапер", Morgue = "Морг №1", DeathDate = "2026-11-05", Tier = "Обычный" },
                    new WaitingRowDto { PersonId = 2, Name = "Сєрьога Шахіст", Morgue = "Морг №2", DeathDate = "2026-11-05", Tier = "Бесплатный" }
                };
                DGVVOjid.DataSource = sample;
            }
        }

        private async void ButtBury_Click(object? sender, EventArgs e)
        {
            if (DGVVOjid.CurrentRow != null && DGVVOjid.CurrentRow.DataBoundItem is WaitingRowDto item)
            {
                var (ok, _, err) = await ApiClient.PostAsync<GenericResponse>($"/api/waiting/{item.PersonId}/bury", new { });
                if (ok)
                {
                    MessageBox.Show($"Клієнт {item.Name} успішно похований з почестями!", "Похорон завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadWaiting();
                }
                else
                {
                    MessageBox.Show(string.IsNullOrEmpty(err) ? "Поховано локально." : err, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Оберіть клієнта у списку для поховання!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtBackVOjid_Click(object sender, EventArgs e)
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

        private void DGVVOjid_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
