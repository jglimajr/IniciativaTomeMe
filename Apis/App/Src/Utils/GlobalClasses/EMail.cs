using FluentValidator;
using FluentValidator.Validation;

namespace InteliSystem.Utils.GlobalClasses
{
    public class EMail : Notifiable
    {
        private EMail() { }
        public EMail(string address)
        {
            this.Address = address.Trim().ToLower();
            AddNotifications(new ValidationContract().Requires()
                .IsEmailOrEmpty(this.Address, "EMail.Address", "E-Mail não é válido")
            );
        }

        public string Address { get; private set; }
    }
}