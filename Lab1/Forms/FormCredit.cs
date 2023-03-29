using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab1;

namespace Lab1.Forms
{
    public partial class FormCredit : Form

    {
        public static string path = @"D:\Study\4_2\Development of user interfaces\Lab1\Calculate.txt";
        public FormCredit()
        {
            InitializeComponent();

            comboBox1.Items.Add("Аннуїтетний кредит");
            comboBox1.Items.Add("Диференційний кредит");
            comboBox1.SelectedIndex = 0;
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
            if (string.IsNullOrEmpty(principalTextBox.Text) ||
                string.IsNullOrEmpty(interestRateTextBox.Text) ||
                string.IsNullOrEmpty(yearsTextBox.Text))
            {
                MessageBox.Show("Введіть усі необхідні значення!", "Відсутні значення!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double loanAmount;
            if (!double.TryParse(principalTextBox.Text, out loanAmount) || loanAmount <= 0)
            {
                MessageBox.Show("Введіть дійсну суму кредиту!", "Недійсне значення!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double annualInterestRate;
            if (!double.TryParse(interestRateTextBox.Text, out annualInterestRate) ||
                annualInterestRate <= 0 || annualInterestRate > 100)
            {
                MessageBox.Show("Введіть дійсну річну процентну ставку!", "Недійсне значення!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int loanPeriod;
            if (!int.TryParse(yearsTextBox.Text, out loanPeriod) || loanPeriod <= 0)
            {
                MessageBox.Show("Будь ласка, введіть дійсний термін позики в роках!", "Недійсне значення!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            double monthlyPayment = 0;
            double totalAmount = 0;
            double totalInterest = 0;

            if (comboBox1.SelectedIndex == 0) // Аннуїтетний кредит
            {
                double monthlyInterestRate = annualInterestRate / 1200;
                int numberOfPayments = loanPeriod * 12;
                double interestQuotient = monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, numberOfPayments) /
                                          (Math.Pow(1 + monthlyInterestRate, numberOfPayments) - 1);
                monthlyPayment = loanAmount * interestQuotient;
                totalAmount = monthlyPayment * numberOfPayments;
                totalInterest = totalAmount - loanAmount;

            }
            else if (comboBox1.SelectedIndex == 1) // Диференційний кредит
            {
                double monthlyInterestRate = annualInterestRate / 1200;
                int numberOfPayments = loanPeriod * 12;
                double balance = loanAmount;
                for (int i = 1; i <= numberOfPayments; i++)
                {
                    double interest = balance * monthlyInterestRate;
                    double payment = loanAmount / numberOfPayments + interest;
                    balance -= loanAmount / numberOfPayments;
                    totalInterest += interest;
                    totalAmount += payment;
                }
            }

            Saver.SaveToFile(" totalPayment: " + totalAmount + "\ttotalInterest: " + totalInterest + "\tmonthlyPayment: " + monthlyPayment, path, false);

            totalPaymentsLabel.Text = string.Format("Разом сума виплат: {0:C}", totalAmount);
            totalInterestLabel.Text = string.Format("У тому числі % : {0:C}", totalInterest);
            monthlyPaymentLabel.Text = string.Format("Щомісячна сума виплат: {0:C}", monthlyPayment);
            totalPaymentsLabel.Visible = true;
            totalInterestLabel.Visible = true;
            monthlyPaymentLabel.Visible = true;

        }
    }
}
