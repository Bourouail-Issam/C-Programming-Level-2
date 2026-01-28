using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleEventWithPrameters
{
    public partial class MyUserControl : UserControl
    {

        // Define a custom event handler delegate with parameters
        public event Action<double> OnCalculationComplete;
        // Create a protected method to raise the event with a parameter
        protected virtual void CalculationComplete(double Result)
        {
            Action<double> handler = OnCalculationComplete;
            if (handler != null)
            {
                handler(Result); // Raise the event with the parameter
            }
        }

        public MyUserControl()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren()) 
                return;

            double Result =  Convert.ToDouble(txtNumber1.Text) + Convert.ToDouble(txtNumber2.Text);
            lblResult.Text = Result.ToString();

            if (OnCalculationComplete != null)
                // Raise the event with a parameter
                CalculationComplete(Result);

        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // Always allow control keys (Backspace, Delete, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Allow digits
            if (char.IsDigit(e.KeyChar))
                return;

            // Allow ONE dot, not as first char
            if (e.KeyChar == '.' && !txt.Text.Contains(".") && txt.SelectionStart > 0)
                return;

            // Block anything else
            e.Handled = true;
        }

        private void txtNumber_Validating(object sender, CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (string.IsNullOrEmpty(txt.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError((TextBox)sender, "Field cannot be empty!");
                return;
            }
            else
                errorProvider1.SetError((TextBox)sender, null);
        }
    }
}
