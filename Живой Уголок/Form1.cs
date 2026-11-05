namespace Живой_Уголок
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ButtReg_Click(object sender, EventArgs e)
        {

        }

        private void ButtDeat_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void ButtVOjid_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void ButtOrders_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void ButtStore_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void textLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textData_TextChanged(object sender, EventArgs e)
        {

        }

        private void textYear_TextChanged(object sender, EventArgs e)
        {

        }

        private void BattDellReg_Click(object sender, EventArgs e)
        {

        }
    }
}
