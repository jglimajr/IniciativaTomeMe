using System.ComponentModel;

namespace InteliSystem.Utils.Enumerators
{
    public enum ReturnValues
    {
        [Description("Sucesso")] Success = 1,
        [Description("Falha")] Failure,
        [Description("Não Econtrado")] NotFound,
        [Description("Retorne ao Login")] ReturnToLogin
    }
}