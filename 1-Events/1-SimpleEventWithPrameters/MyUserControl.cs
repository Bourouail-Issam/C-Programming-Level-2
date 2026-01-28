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

        //// Define a custom event handler delegate with parameters
        //public event Action<double> OnCalculationComplete;
        //// Create a protected method to raise the event with a parameter
        //protected virtual void CalculationComplete(double Result)
        //{
        //    Action<double> handler = OnCalculationComplete;
        //    if (handler != null)
        //    {
        //        handler(Result); // Raise the event with the parameter
        //    }
        //}

        public MyUserControl()
        {
            InitializeComponent();
        }

        public class CalculationCompleteEventArgs
        {
            public double Value1 { get; set; }
            public double value2 { get; set; }
            public double Result { get; set; }

            public CalculationCompleteEventArgs(double value1, double value2,double result)
            {
                this.Value1 = value1;
                this.value2 = value2;
                this.Result = result;
            }
        }

        public event EventHandler<CalculationCompleteEventArgs> OnCalculationComplete;

        public  void RaiseOnCalulationComplete(double value1, double value2, double result)
        {
            RaiseOnCalulationComplete(new CalculationCompleteEventArgs(value1, value2, result));
        }

        protected virtual void RaiseOnCalulationComplete(CalculationCompleteEventArgs e)
        {
            OnCalculationComplete?.Invoke(this, e);
        }


        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren()) 
                return;

            double Val1, Val2, Result;

            Val1 = Convert.ToDouble(txtNumber1.Text);
            Val2 = Convert.ToDouble(txtNumber2.Text);
            Result = Val1 + Val2;

            lblResult.Text = Result.ToString();

            if (OnCalculationComplete != null)
                // Raise the event with a parameter
                RaiseOnCalulationComplete(Val1,Val2,Result);

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
