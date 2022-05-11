using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* ******************************************************
* CIS 123: Introduction to Object-Oriented Programming  *
* Murach C# 7th ed                                      *
* Chapter 7: How to handle exception and validate data  *
* Exercise 7-2 Enhance the Future Value application     *
*       Base code and form design provided by Murach    *
*       Exercise Instructions: pg. 221                  *
* Dominique Tepper, 10MAY2022                           *
* ******************************************************/

namespace FutureValue
{
    public partial class frmFutureValue : Form
    {
        public frmFutureValue()
        {
            InitializeComponent();
        }

/* *******************************************************************
 *  2. Add a try-catch block statement to the btnCalculate_Click()   *
 *     method that catches and handles the following occurences:     *
 *              A. FormatException                                   *
 *              B. OverflowException                                 *
 *                                                                   *
 *     <!> Display message boxes that show the appropriate message   *
 *         for each exception.                                       *
 * ********************************************************* Tepper */
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                decimal monthlyInvestment = Convert.ToDecimal(txtMonthlyInvestment.Text);
                decimal yearlyInterestRate = Convert.ToDecimal(txtInterestRate.Text);
                int years = Convert.ToInt32(txtYears.Text);

                int months = years * 12;
                decimal monthlyInterestRate = yearlyInterestRate / 12 / 100;

                decimal futureValue = this.CalculateFutureValue(
                    monthlyInvestment, monthlyInterestRate, months);
                txtFutureValue.Text = futureValue.ToString("c");
                txtMonthlyInvestment.Focus();
            }

/* ******************************************
*  2A. Catch block for FormatException      *
* ********************************* Tepper */
            catch (FormatException)
            {
                MessageBox.Show("A format exception has occured. Please check all entries.",
                    "Invalid Data Type");
                txtMonthlyInvestment.Focus();
            }
/* ******************************************
*  2B. Catch block for OverflowException    *
* ********************************* Tepper */
            catch (OverflowException)
            {
                MessageBox.Show("An overflow exception has occured. Please enter a smaller value.",
                    "Invalid Value");
                txtMonthlyInvestment.Focus();
            }
        }

        private decimal CalculateFutureValue(decimal monthlyInvestment,
            decimal monthlyInterestRate, int months)
        {
            decimal futureValue = 0m;
            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + monthlyInvestment)
                            * (1 + monthlyInterestRate);
            }
            return futureValue;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
