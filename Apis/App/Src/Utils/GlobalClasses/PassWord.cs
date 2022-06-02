using System.Text.RegularExpressions;
using FluentValidator;
using FluentValidator.Validation;

namespace InteliSystem.Utils.GlobalClasses
{
    public class PassWord : Notifiable
    {
        private PassWord() { }
        public PassWord(string value)
        {
            PassWordValidate(value);
            if (this.Valid)
            {
                this.Value = value;
                return;
            }

            this.Value = string.Empty;
        }

        public string Value { get; private set; }

        private void PassWordValidate(string value)
        {
            // var regExpAux = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$";
            // var regExp = new Regex(regExpAux);

            var lenghtpass = value.Length;

            // AddNotifications(new ValidationContract().Requires()
            //     .IsTrue(regExp.IsMatch(value), "Customer.PassWord", "Your password must be between 6 and 20 characters long. Contain, at least, one uppercase letter, a lowercase letter, a special character and a number")
            // );
            AddNotifications(new ValidationContract().Requires()
                .IsTrue((lenghtpass >= 6 && lenghtpass <= 20), "Customer.PassWord", "Sua senha deve conter entre 6 e 20 Caracteres")
            );
        }
    }
}