using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MyOwnUserControls
{
    public partial class MyCustomTextBox : TextBox
    {
        #region Enumerations

        /// <summary>
        /// Defines the allowed input type for the TextBox.
        /// </summary>
        public enum InputTypeOption
        {
            DefaultTextBox = 0,
            LetterText = 1,
            Email = 2,
            Numbers = 3,
            Phone = 4
        }

        #endregion


        #region Private Fields

        private InputTypeOption _inputType;
        private bool _isRequired;
        private KeyPressEventHandler _activeKeyPressHandler;

        #endregion


        #region Constructor

        public MyCustomTextBox()
        {
            InitializeComponent();
        }

        #endregion


        #region Overrides

        /// <summary>
        /// Reserved for future custom painting.
        /// Currently delegates entirely to the base implementation.
        /// Override this method to add custom drawing logic when needed.
        /// </summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        #endregion


        #region Designer Properties

        /// <summary>
        /// Gets or sets the input type that controls which characters are accepted.
        /// </summary>
        [Category("Validation")]
        [Description("Specifies the type of input accepted: letters, numbers, email, or phone.")]
        public InputTypeOption InputType
        {
            get => _inputType;
            set
            {
                _inputType = value;
                ApplyInputTypeHandler(_inputType);
            }
        }

        /// <summary>
        /// Gets or sets whether a non-empty value is required when focus leaves the control.
        /// </summary>
        [Category("Validation")]
        [Description("When true, shows a validation error if the field is left empty.")]
        public bool IsRequired
        {
            get => _isRequired;
            set
            {
                _isRequired = value;
                this.Validating -= OnRequiredFieldValidating;

                if (_isRequired)
                    this.Validating += OnRequiredFieldValidating;
            }
        }

        #endregion


        #region Private Helper Methods

        /// <summary>
        /// Detaches the currently active KeyPress handler, then attaches
        /// the correct one for the selected <paramref name="inputType"/>.
        /// Prevents duplicate event subscriptions when the property is set more than once.
        /// </summary>
        /// <param name="inputType">The newly selected input type.</param>
        private void ApplyInputTypeHandler(InputTypeOption inputType)
        {
            if (_activeKeyPressHandler != null)
            {
                this.KeyPress -= _activeKeyPressHandler;
                _activeKeyPressHandler = null;
            }

            switch (inputType)
            {
                case InputTypeOption.DefaultTextBox:
                    break;
                case InputTypeOption.LetterText:
                    _activeKeyPressHandler = OnLetterTextKeyPress;
                    break;
                case InputTypeOption.Email:
                    _activeKeyPressHandler = OnEmailKeyPress;
                    break;
                case InputTypeOption.Numbers:
                    _activeKeyPressHandler = OnNumberKeyPress;
                    break;
                case InputTypeOption.Phone:
                    _activeKeyPressHandler = OnPhoneKeyPress;
                    break;
            }

            if (_activeKeyPressHandler != null)
                this.KeyPress += _activeKeyPressHandler;
        }

        #endregion


        #region Validation Event Handlers

        /// <summary>
        /// Fires when the control loses focus and IsRequired is true.
        /// Cancels the focus change if the field is empty.
        /// </summary>
        private void OnRequiredFieldValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                MessageBox.Show(
                    "This field is required. Please enter a value.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                e.Cancel = true;
            }
        }

        #endregion


        #region KeyPress Filter Handlers

        /// <summary>
        /// Allows only digits and a single optional decimal point.
        /// </summary>
        private void OnNumberKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;

            if (e.KeyChar == '.' && !this.Text.Contains(".") && this.SelectionStart > 0)
                return;

            e.Handled = true;
        }

        /// <summary>
        /// Allows only alphabetic characters (A–Z, a–z).
        /// </summary>
        private void OnLetterTextKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if (!char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Allows only characters that are valid inside an email address.
        /// </summary>
        private void OnEmailKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            const string allowedChars =
                "abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "0123456789" +
                ".!#$%&'*+-/=?^_`{|}~@-";

            if (allowedChars.IndexOf(e.KeyChar) < 0)
                e.Handled = true;
        }

        /// <summary>
        /// Allows only digits, an optional leading '+', spaces, hyphens, and parentheses.
        /// </summary>
        private void OnPhoneKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            bool isAllowedChar = char.IsDigit(e.KeyChar)
                              || e.KeyChar == ' '
                              || e.KeyChar == '-'
                              || e.KeyChar == '('
                              || e.KeyChar == ')';

            bool isPlusAtStart = e.KeyChar == '+' && this.SelectionStart == 0;

            if (!isAllowedChar && !isPlusAtStart)
                e.Handled = true;
        }

        #endregion


        #region Instance Validation Helpers

        /// <summary>Returns true if the current text is a valid number.</summary>
        public bool ValidateAsNumber() => IsValidNumber(this.Text);

        /// <summary>Returns true if the current text contains only letters.</summary>
        public bool ValidateAsLetterText() => IsValidLetterText(this.Text);

        /// <summary>Returns true if the current text is a valid email address.</summary>
        public bool ValidateAsEmail() => IsValidEmail(this.Text);

        /// <summary>Returns true if the current text is a valid phone number.</summary>
        public bool ValidateAsPhone() => IsValidPhoneNumber(this.Text);

        #endregion


        #region Static Validation Methods

        /// <summary>
        /// Determines whether <paramref name="text"/> contains only letters.
        /// </summary>
        /// <param name="text">The string to test.</param>
        /// <returns>True if every character is a letter; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="text"/> is null.
        /// </exception>
        public static bool IsValidLetterText(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));

            foreach (char c in text)
                if (!char.IsLetter(c)) return false;

            return true;
        }

        /// <summary>
        /// Determines whether <paramref name="text"/> is a valid integer or decimal number.
        /// </summary>
        /// <param name="text">The string to test.</param>
        /// <returns>True if the text is a valid number; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="text"/> is null.
        /// </exception>
        public static bool IsValidNumber(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));

            return ValidateInteger(text) || ValidateFloat(text);
        }

        /// <summary>
        /// Determines whether <paramref name="emailAddress"/> is a valid email address.
        /// </summary>
        /// <param name="emailAddress">The string to test.</param>
        /// <returns>True if the email format is valid; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="emailAddress"/> is null.
        /// </exception>
        public static bool IsValidEmail(string emailAddress)
        {
            if (emailAddress == null) throw new ArgumentNullException(nameof(emailAddress));

            const string pattern =
                @"^[a-zA-Z0-9.!#$%&'*+\-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            return Regex.IsMatch(emailAddress, pattern);
        }

        /// <summary>
        /// Determines whether <paramref name="phoneNumber"/> matches
        /// the international phone format (8–15 digits, optional leading +).
        /// </summary>
        /// <param name="phoneNumber">The string to test.</param>
        /// <returns>True if the phone number is valid; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="phoneNumber"/> is null.
        /// </exception>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null) throw new ArgumentNullException(nameof(phoneNumber));

            const int minLength = 8;
            string pattern = $@"^\+?[1-9]\d{{{minLength - 1},14}}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        /// <summary>
        /// Determines whether <paramref name="number"/> is a valid integer (digits only, no dot).
        /// </summary>
        /// <param name="number">The string to test.</param>
        /// <returns>True if the string is a non-empty sequence of digits; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="number"/> is null.
        /// </exception>
        public static bool ValidateInteger(string number)
        {
            if (number == null) throw new ArgumentNullException(nameof(number));

            return Regex.IsMatch(number, @"^[0-9]+$");
        }

        /// <summary>
        /// Determines whether <paramref name="number"/> is a valid decimal number
        /// (digits with an optional single dot followed by more digits).
        /// </summary>
        /// <param name="number">The string to test.</param>
        /// <returns>True if the string is a valid float; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="number"/> is null.
        /// </exception>
        public static bool ValidateFloat(string number)
        {
            if (number == null) throw new ArgumentNullException(nameof(number));

            return Regex.IsMatch(number, @"^[0-9]*(?:\.[0-9]+)?$");
        }

        #endregion
    }
}
