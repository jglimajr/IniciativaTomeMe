using System.ComponentModel;

namespace InteliSystem.Utils.Enumerators
{
    public enum RulesValues
    {
        [Description("Administrador")] Admin = 1,
        [Description("Gerente")] Management,
        [Description("Funcionario")] Employee,
        [Description("Cliente")] Customer,
    }
}