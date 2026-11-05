using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LivingCorner.Gui
{
    public partial class Form5 : Form
    {
        private List<ItemDto> coffins = new();
        private List<ItemDto> tiers = new();
        private List<ItemDto> priests = new();

        public Form5()
        {
            InitializeComponent();
            btnCheckout.Click += BtnCheckout_Click;
        }

        private async void Form5_Load(object sender, EventArgs e)
        {
            UpdateBalanceDisplay();
            await LoadShopData();
        }

        public void UpdateBalanceDisplay()
        {
            lblChop.Text = $"Баланс: {Many.Coin} $";
        }

        private async System.Threading.Tasks.Task LoadShopData()
        {
            var shop = await ApiClient.GetAsync<ShopResponseDto>("/api/shop");
            if (shop != null && shop.Coffins != null && shop.Coffins.Count > 0)
            {
                coffins = shop.Coffins;
                tiers = shop.Tiers;
                priests = shop.Priests;
            }
            else
            {
                coffins = new List<ItemDto>
                {
                    new ItemDto { Id = 1, Name = "Сосновий", Price = 100 },
                    new ItemDto { Id = 2, Name = "Дубовий", Price = 400 },
                    new ItemDto { Id = 3, Name = "Золотий", Price = 1500 }
                };
                tiers = new List<ItemDto>
                {
                    new ItemDto { Id = 1, Name = "Бесплатный", Price = 0 },
                    new ItemDto { Id = 2, Name = "Обычный", Price = 500 },
                    new ItemDto { Id = 3, Name = "Премиум", Price = 2000 }
                };
                priests = new List<ItemDto>
                {
                    new ItemDto { Id = 1, Name = "Отец Фёдор", Price = 50 },
                    new ItemDto { Id = 2, Name = "Отец Иоанн", Price = 200 },
                    new ItemDto { Id = 3, Name = "Архиепископ", Price = 1000 }
                };
            }

            comboCoffin.Items.Clear();
            foreach (var c in coffins) comboCoffin.Items.Add($"{c.Name} ({c.Price} $)");
            if (comboCoffin.Items.Count > 0) comboCoffin.SelectedIndex = 0;

            comboPriest.Items.Clear();
            foreach (var p in priests) comboPriest.Items.Add($"{p.Name} ({p.Price} $)");
            if (comboPriest.Items.Count > 0) comboPriest.SelectedIndex = 0;

            comboTier.Items.Clear();
            foreach (var t in tiers) comboTier.Items.Add($"{t.Name} ({t.Price} $)");
            if (comboTier.Items.Count > 0) comboTier.SelectedIndex = 0;
        }

        private async void BtnCheckout_Click(object? sender, EventArgs e)
        {
            if (comboCoffin.SelectedIndex < 0 || comboPriest.SelectedIndex < 0 || comboTier.SelectedIndex < 0)
            {
                MessageBox.Show("Будь ласка, оберіть труну, священика та рівень похорону!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedCoffin = coffins[comboCoffin.SelectedIndex];
            var selectedPriest = priests[comboPriest.SelectedIndex];
            var selectedTier = tiers[comboTier.SelectedIndex];
            int totalCost = selectedCoffin.Price + selectedPriest.Price + selectedTier.Price;

            if (Many.Coin < totalCost)
            {
                MessageBox.Show($"Недостатньо грошей!\nРазом до сплати: {totalCost} $\nВаш баланс: {Many.Coin} $\nІдіть заробіть на кладовищі!", "Банкрут", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ApiClient.CurrentPersonId.HasValue)
            {
                var payload = new
                {
                    personId = ApiClient.CurrentPersonId.Value,
                    coffinId = selectedCoffin.Id,
                    tierId = selectedTier.Id,
                    priestId = selectedPriest.Id
                };
                var (ok, resp, err) = await ApiClient.PostAsync<GenericResponse>("/api/shop/buy", payload);
                if (ok && resp != null)
                {
                    Many.Coin = resp.Balance;
                    UpdateBalanceDisplay();
                    MessageBox.Show($"Замовлення успішно оформлено на сервері!\nВитрачено: {resp.Spent} $\nЗалишок: {resp.Balance} $", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show(err, "Помилка замовлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            Many.Coin -= totalCost;
            UpdateBalanceDisplay();
            MessageBox.Show($"Вітаємо! Ритуальний пакет оформлено!\nТруна: {selectedCoffin.Name}\nСвященик: {selectedPriest.Name}\nРівень: {selectedTier.Name}\nСписано: {totalCost} $\nЗалишок: {Many.Coin} $", "Замовлення оформлено", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtBekStore_Click(object sender, EventArgs e)
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

        private void ButMany_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }

        private void lblChop_Click(object sender, EventArgs e) { }
        private void ButBuyGrob_Click(object sender, EventArgs e) { }
        private void ButBuySvat_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}
