using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LivingCorner.Gui
{
    public partial class Form6 : Form
    {
        private Button[] grobs;
        private bool[] isBomb = new bool[9];
        private Random rnd = new Random();
        private int? serverGameId = null;

        public Form6()
        {
            InitializeComponent();
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 2500;
            timer1.Tick += DeathTimer_Tick;
            grobs = new Button[] { ButGrob1, ButGrob2, ButGrob3, ButGrob4, ButGrob5, ButGrob6, ButGrob7, ButGrob8, ButGrob9 };

            for (int i = 0; i < grobs.Length; i++)
            {
                grobs[i].Tag = i;
                grobs[i].Click += Grob_Click;
                grobs[i].BackgroundImage = Properties.Resources.Grob1;
                grobs[i].BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private async void Form6_Load(object sender, EventArgs e)
        {
            UpdateCoins();
            await InitGame();
        }

        private async System.Threading.Tasks.Task InitGame()
        {
            isBomb = new bool[9];
            int placed = 0;
            while (placed < 2)
            {
                int idx = rnd.Next(0, 9);
                if (!isBomb[idx])
                {
                    isBomb[idx] = true;
                    placed++;
                }
            }

            if (ApiClient.CurrentPersonId.HasValue)
            {
                var (ok, data, _) = await ApiClient.PostAsync<StartGameResponseDto>("/api/game/start", new { personId = ApiClient.CurrentPersonId.Value });
                if (ok && data != null)
                {
                    serverGameId = data.GameId;
                }
            }
        }

        private void UpdateCoins()
        {
            label1.Text = $"$: {Money.Coin}";
        }

        private async void Run_Click(object sender, EventArgs e)
        {
            if (serverGameId.HasValue)
            {
                await ApiClient.PostAsync<object>($"/api/game/{serverGameId.Value}/cashout", new { });
            }

            foreach (Form form in Application.OpenForms)
            {
                if (form is Form5 form5)
                {
                    form5.UpdateBalanceDisplay();
                    form.Show();
                    break;
                }
            }
            this.Close();
        }

        private async void Grob_Click(object sender, EventArgs e)
        {
            Button? clickBut = sender as Button;
            if (clickBut == null) return;
            int index = (int)clickBut.Tag;

            if (serverGameId.HasValue)
            {
                var (ok, digData, _) = await ApiClient.PostAsync<DigResponseDto>($"/api/game/{serverGameId.Value}/dig", new { cell = index });
                if (ok && digData != null)
                {
                    if (digData.Exploded)
                    {
                        TriggerDeath(clickBut);
                        return;
                    }

                    Money.AddCoins(10);
                    UpdateCoins();
                    clickBut.BackgroundImage = Properties.Resources.Grob2;
                    clickBut.BackgroundImageLayout = ImageLayout.Stretch;
                    clickBut.Enabled = false;

                    if (digData.Finished)
                    {
                        MessageBox.Show($"ВІТАЄМО! Ви викопали всі безпечні гроби і виграли супер-приз {digData.Reward} $!", "Перемога!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Run_Click(Run, EventArgs.Empty);
                    }
                    return;
                }
            }

            if (isBomb[index])
            {
                TriggerDeath(clickBut);
            }
            else
            {
                Money.AddCoins(10);
                UpdateCoins();
                clickBut.BackgroundImage = Properties.Resources.Grob2;
                clickBut.BackgroundImageLayout = ImageLayout.Stretch;
                clickBut.Enabled = false;
            }
        }

        private void TriggerDeath(Button explodedBtn)
        {
            explodedBtn.BackgroundImage = Properties.Resources.Grob3;
            explodedBtn.BackgroundImageLayout = ImageLayout.Stretch;
            foreach (Button btn in grobs)
            {
                btn.Enabled = false;
            }
            Run.Enabled = false;
            timer1.Start();
        }

        private async void DeathTimer_Tick(object? sender, EventArgs e)
        {
            timer1.Stop();

            if (ApiClient.CurrentPersonId.HasValue)
            {
                var choice = MessageBox.Show(
                    "БА-БАХ! Ви знайшли ДІДА З БОМБОЮ!\n\nБажаєте воскреснути за 100 $?\n(Якщо 'Ні' — ви потрапите в морг)",
                    "Вибух",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (choice == DialogResult.Yes)
                {
                    var (resOk, resData, resErr) = await ApiClient.PostAsync<GenericResponse>($"/api/persons/{ApiClient.CurrentPersonId.Value}/resurrect", new { });
                    if (resOk && resData != null)
                    {
                        Money.Coin = resData.Balance;
                        MessageBox.Show("ЧУДО! Вас успішно воскресили!", "Воскресіння", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Run_Click(Run, EventArgs.Empty);
                        return;
                    }
                    else
                    {
                        MessageBox.Show(string.IsNullOrEmpty(resErr) ? "Не вистачило коштів на воскресіння!" : resErr, "Невдача", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                await ApiClient.PostAsync<GenericResponse>($"/api/persons/{ApiClient.CurrentPersonId.Value}/abandon", new { });
                MessageBox.Show("Вас оформлено у морг як безкоштовного мерця.", "RIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
                return;
            }

            MessageBox.Show("Ви знайшли ДІДА з БОМБОЮ\nВИ ВМЕРЛИ", "Смерть", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        private void ButGrob1_Click(object sender, EventArgs e) { }
        private void ButGrob2_Click(object sender, EventArgs e) { }
        private void ButGrob3_Click(object sender, EventArgs e) { }
        private void ButGrob4_Click(object sender, EventArgs e) { }
        private void ButGrob5_Click(object sender, EventArgs e) { }
        private void ButGrob6_Click(object sender, EventArgs e) { }
        private void ButGrob7_Click(object sender, EventArgs e) { }
        private void ButGrob8_Click(object sender, EventArgs e) { }
        private void ButGrob9_Click(object sender, EventArgs e) { }
        private void timer1_Tick(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}
