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
         *                                                                   *
         *  7. Modify the btnCalculate_Click() event handler so it uses the  *
         *     IsValidData() method to validate data before processing.      *
         * ********************************************************* Tepper */
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidData()) // Step 7: input validated using IsValidData()
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
            }
/* **************************************************************************************************
 *  3. Add a catch block that catches any other exceptions that might occur. It should display:     *
 *              A. a dialog box                                                                     *
 *              B. a message contained in the exception object                                      *
 *              C. exception type                                                                   *
 *              D. stack trace                                                                      *
 * **************************************************************************************** Tepper */
            catch (Exception ex)
            {
                MessageBox.Show(                       // 3-A
                    ex.Message + "\n\n" +              // 3-B
                    ex.GetType().ToString() + "\n" +   // 3-C
                    ex.StackTrace,                     // 3-D
                    "Exception");
            }
        }
/* * Tepper ********************************* Simple input range check ******************************************************
               
               if (monthlyInvestment <= 0 || monthlyInvestment >= 1001)
               {
                 MessageBox.Show("Monthly Investment must be between 1 - 1000", "Invalid Entry");
               }

               if (yearlyInterestRate <= 0 || yearlyInterestRate >= 21)
               {
                    MessageBox.Show("Interest Rate must be between 1 - 20", "Invalid Entry");
               }

               if (years <= 0 || yearlyInterestRate >= 41)
               {
                   MessageBox.Show("Years to Invest must be between 1 - 40", "Invalid Entry");
               }
// 2A. Catch block for FormatException

            catch (FormatException)
            {
                MessageBox.Show("A format exception has occured. Please check all entries.",
                    "Invalid Data Type");
                txtMonthlyInvestment.Focus();
            }

// 2B. Catch block for OverflowException

            catch (OverflowException)
            {
                MessageBox.Show("An overflow exception has occured. Please enter a smaller value.",
                    "Invalid Value");
                txtMonthlyInvestment.Focus();
            }
**************************************************************************************************************** Tepper */


/* ***************************************************************************************
 *  4. Add a throw statement before the return statement in the CalculateFutureValue()   *
 *     method that throws a new exception of the Exception class regardless of the       *
 *     result of the calculation to test the enhancements in step 3.                     *
 * ***************************************************************************** Tepper */
        private decimal CalculateFutureValue(decimal monthlyInvestment,
            decimal monthlyInterestRate, int months)
        {
            decimal futureValue = 0m;

            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + monthlyInvestment)
                            * (1 + monthlyInterestRate);
            }
            
         // throw new Exception("An unknown exception has occured."); // Step 4
            return futureValue;
        }

/* ********************************************************************************
 *  5. Code generic validation methods that will test for valid inputs.           *
 *        A. IsDecimal()        txtMonthlyInvestment, txtInterestRate             *
 *        B. IsInt32()          txtYears                                          *
 *        C. IsWithinRange()    all input                                         *
 *                                                                                *
 *     Unsuccessful validation should return an an error message that includes    *
 *     the textbox name that's being validated.                                   *
 * ********************************************************************** Tepper */

        // 5-A. Checks if input is a valid decimal
        private string IsDecimal(string value, string name)
        {
            string message = "";
            if (!Decimal.TryParse(value, out _))
            {
                message += name + " must be a valid decimal value. \n";
            }
            return message;
        }

        // 5-B. Checks if input is a valid integer
        private string IsInt32(string value, string name)
        {
            string message = "";
            if (!Int32.TryParse(value, out _))
            {
                message += name + " must be a valid integer value. \n";
            }
            return message;
        }

        // 5-C. Checks if input is within range
        private string IsWithinRange(string value, string name, decimal min, decimal max)
        {
            string message = "";
            if (Decimal.TryParse(value, out decimal number))
            {
                if (number < min || number > max)
                {
                    message += name + " must be between " + min + " and "  + max + ". \n";
                }
            }
            return message;
        }

/* ********************************************************************************
 * 6. Code an IsValidData() method that calls the three generic methods created   *
 *    in Step 5 and returns a Boolean value that indicates a successful check.    *
 *    Each text box should be tested for:                                         *
 *          A. invalid format                                                     *
 *          B. invalid range                                                      *
 *                                                                                *
 *    Display a dialog box with any error that occurs.                            *
 * ********************************************************************** Tepper */

        private bool IsValidData()
        {
            bool success = true;
            string errorMessage = "";

            // 6A & 6B Validation for txtMonthlyInvestment input
            errorMessage += IsDecimal(txtMonthlyInvestment.Text,
                txtMonthlyInvestment.Tag.ToString());
            errorMessage += IsWithinRange(txtMonthlyInvestment.Text, 
                txtMonthlyInvestment.Tag.ToString(), 1, 1000);

            // 6A & 6B Validation for txtInterestRate input
            errorMessage += IsDecimal(txtInterestRate.Text,
                txtInterestRate.Tag.ToString());
            errorMessage += IsWithinRange(txtInterestRate.Text,
                txtInterestRate.Tag.ToString(), 1, 20);

            // 6A & 6B Validation for txtYears input
            errorMessage += IsInt32(txtYears.Text,
                txtYears.Tag.ToString());
            errorMessage += IsWithinRange(txtYears.Text,
                txtYears.Tag.ToString(), 1, 40);

            if (errorMessage != "")
            {
                success = false;
                MessageBox.Show("errorMessage", "Entry Error");
            }
            return success;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
