using InteliSystem.Utils.GlobalClasses;

namespace InteliSystem.Cores.CustomersRules;

public class CustomerAdd : ClassRuleBase
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public short Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? EMail { get; set; }
    public string? PassWord { get; set; }
}