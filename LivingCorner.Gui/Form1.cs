using System;
using System.Drawing;
using System.Windows.Forms;

namespace LivingCorner.Gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void BtnReg_Click(object sender, EventArgs e)
        {
            string firstName = textName.Text.Trim();
            string lastName = textLastName.Text.Trim();
            if (string.IsNullOrEmpty(firstName) || firstName == "Ім'я")
            {
                MessageBox.Show("Введіть нормальне ім'я!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int day, month, year;
            if (!int.TryParse(textData.Text.Trim(), out day) ||
                !int.TryParse(textMonth.Text.Trim(), out month) ||
                !int.TryParse(textYear.Text.Trim(), out year))
            {
                MessageBox.Show("Дата народження повинна бути числами: ДД, ММ, РРРР", "Помилка дати", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fullName = string.IsNullOrEmpty(lastName) || lastName == "Прізвище" 
                ? firstName 
                : $"{firstName} {lastName}";

            var req = new
            {
                name = fullName,
                day = day,
                month = month,
                year = year
            };

            var (success, person, error) = await ApiClient.PostAsync<PersonDto>("/api/register", req);
            if (success && person != null)
            {
                ApiClient.CurrentPersonId = person.Id;
                ApiClient.CurrentPersonName = person.Name;
                Money.Coin = person.Balance;
                MessageBox.Show($"Вітаємо у Живому Куточку, {person.Name}!\nТвій ID: {person.Id}\nБаланс: {person.Balance} $", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ApiClient.CurrentPersonName = fullName;
                MessageBox.Show(string.IsNullOrEmpty(error) ? "Клієнт зареєстрований локально (сервер відпочиває)" : error, "Реєстрація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BattDellReg_Click(object sender, EventArgs e)
        {
            textName.Text = "Ім'я";
            textLastName.Text = "Прізвище";
            textData.Text = "ДД";
            textMonth.Text = "ММ";
            textYear.Text = "РРРР";
            ApiClient.CurrentPersonId = null;
            ApiClient.CurrentPersonName = "";
            Money.ResetSession();
            MessageBox.Show("Форму очищено. Тепер ви ніхто.", "Скидання", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDeat_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void BtnVOjid_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void BtnOrders_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void BtnStore_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void textName_TextChanged(object sender, EventArgs e) { }
        private void textLastName_TextChanged(object sender, EventArgs e) { }
        private void textData_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textYear_TextChanged(object sender, EventArgs e) { }
    }
}
