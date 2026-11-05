using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LivingCore.Gui
{
    public partial class Form6 : Form
    {
        private Button[] grobs;
        private bool[] isBomb = new bool[9];
        private Random rnd = new Random();
        public Form6()
        {
            InitializeComponent();
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 2500;
            timer1.Tick += DeathTimer_Tick;
            grobs = new Button[] { ButGrob1, ButGrob2, ButGrob3, ButGrob4, ButGrob5, ButGrob6, ButGrob7, ButGrob8, ButGrob9 };
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
            for (int i = 0; i < grobs.Length; i++)
            {
                grobs[i].Tag = i;
                grobs[i].Click += Grob_Click;
                grobs[i].BackgroundImage = Properties.Resources.Grob1;
                grobs[i].BackgroundImageLayout = ImageLayout.Stretch;
            }

        }
        private void Game_Load(object sender, EventArgs e)
        {
            UpdateCoins();
        }
        private void UpdateCoins()
        {
            label1.Text = $"$: {Many.Coin}";
        }
        private void Run_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is Form5 form5)
                {
                    Control[] foundLabel = form5.Controls.Find("lblChop", true);
                    if (foundLabel.Length > 0 && foundLabel[0] is Label lbl)
                    {
                        lbl.Text = $"$: {Many.Coin}";
                    }
                    form.Show();
                    break;
                }
            }
            this.Close();
        }

        private void Grob_Click(object sender, EventArgs e)
        {
            Button clickBut = sender as Button;
            if (clickBut == null) return;
            int index = (int)clickBut.Tag;
            if (isBomb[index])
            {
                clickBut.BackgroundImage = Properties.Resources.Grob3;
                clickBut.BackgroundImageLayout = ImageLayout.Stretch;
                foreach (Button btn in grobs)
                {
                    btn.Enabled = false;
                }
                Run.Enabled = false;
                timer1.Start();

            }
            else
            {
                Many.AddCoins(1);
                UpdateCoins();
                clickBut.BackgroundImage = Properties.Resources.Grob2;
                clickBut.BackgroundImageLayout = ImageLayout.Stretch;
                clickBut.Enabled = false;
            }
        }

        private void DeathTimer_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("Ви знайшли ДІДА з БОМБОЮ\nВИ ВМЕРЛИ", "Смерть", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        private void ButGrob1_Click(object sender, EventArgs e)
        {

        }

        private void ButGrob2_Click(object sender, EventArgs e)
        {

        }

        private void ButGrob3_Click(object sender, EventArgs e)
        {

        }

        private void ButGrob4_Click(object sender, EventArgs e)
        {

        }

        private void ButGrob5_Click(object sender, EventArgs e)
        {

        }

        private void ButGrob6_Click(object sender, EventArgs e)
        {

        }

        private void ButGrob7_Click(object sender, EventArgs e)
        {

        }

        private void ButGrob8_Click(object sender, EventArgs e)
        {

        }

        private void ButGrob9_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            label1.Text = $"$: {Many.Coin}";
        }
    }
}
