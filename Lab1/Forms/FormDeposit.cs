using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1.Forms
{
    public partial class FormDeposit : Form
    {
        public FormDeposit()
        {
            InitializeComponent();
            //LoadTheme();
        }
        public void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }

        }
        private void calculateButton_Click(object sender, EventArgs e)
        {
            // Перевірка на правильність введених даних
            if (!double.TryParse(principalTextBox.Text, out double principal) || principal <= 0)
            {
                MessageBox.Show("Початкова сума має бути додатнім числом.");
                principalTextBox.Focus();
                principalTextBox.SelectAll();
                return;
            }

            if (!double.TryParse(interestRateTextBox.Text, out double interestRate) || interestRate <= 0)
            {
                MessageBox.Show("Річна процентна ставка має бути додатнім числом.");
                interestRateTextBox.Focus();
                interestRateTextBox.SelectAll();
                return;
            }

            if (!int.TryParse(termTextBox.Text, out int term) || term <= 0)
            {
                MessageBox.Show("Термін депозиту має бути додатнім цілим числом.");
                termTextBox.Focus();
                termTextBox.SelectAll();
                return;
            }

           
            double interestRateFraction = interestRate / 100.0;
            double totalAmount = principal * Math.Pow(1 + interestRateFraction / 12, term * 12);

            Saver.SaveToFile("totalAmount: " + totalAmount, FormCredit.path, true);

            
            totalAmountLabel.Text = string.Format("Разом сума виплат: {0:C}", totalAmount);
            totalAmountLabel.Visible = true;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

       
}
