using InteliSystem.Utils.Dapper.Extensions.Attributes;
using InteliSystem.Utils.Enumerators;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.GlobalClasses;

namespace InteliSystem.Entities.Customers;

class Messages
{
    private const string _firtName = "Primeiro Nome não informado";
    private const string _lastName = "Segundo Nome não informado";
    private const string _eMail = "E-Mail não informado";
    private const string _eMailNotValid = "Primeiro Nome não informado";

    public static string EMailNotValid => _eMailNotValid;

    public static string EMail => _eMail;

    public static string LastName => _lastName;

    public static string FirtName => _firtName;
}


public class Customer : DomainClass
{
    private Customer() { }
    public Customer(string? id, StatusValues status = StatusValues.Enable)
        : base(id: id, status: status) { }

    public Customer(string? firstName, string? lastName, short gender, DateTime? birthDate, string? eMail, string? passWord)
        : base()
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        BirthDate = birthDate;
        EMail = eMail;
        PassWord = passWord;
    }

    public Customer(string? id, string? firstName, string? lastName, short gender, DateTime? birthDate, string? eMail, string? passWord, StatusValues status = StatusValues.Enable)
        : base(id: id, status: status)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        BirthDate = birthDate;
        EMail = eMail;
        PassWord = passWord;
    }

    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public short Gender { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public string? EMail { get; private set; }
    [InsertProperty, UpdateProperty(false)]
    public string? PassWord { get; private set; }

    public override bool Equals(object? obj)
    {
        return obj is Customer customer &&
               FirstName == customer.FirstName &&
               LastName == customer.LastName &&
               Gender == customer.Gender &&
               BirthDate == customer.BirthDate &&
               EMail == customer.EMail &&
               PassWord == customer.PassWord;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName, Gender, BirthDate, EMail, PassWord);
    }

    private void Validate()
    {
        if (this.FirstName.IsEmpty())
            this.AddNotification("FistName", Messages.FirtName);
        if (this.LastName.IsEmpty())
            this.AddNotification("LastName", Messages.LastName);
        if (this.EMail.IsNotEMail())
            this.AddNotification("EMail", Messages.EMailNotValid);
    }
}