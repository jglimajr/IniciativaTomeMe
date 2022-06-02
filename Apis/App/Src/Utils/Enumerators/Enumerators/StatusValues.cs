using System.ComponentModel;

namespace InteliSystem.Utils.Enumerators
{
    public enum StatusValues
    {
        [Description("Ativo/Habilitato")] Enable = 1,
        [Description("Waiting")] Waiting = 2,
        [Description("Bloqueado")] Blocked = 97,
        [Description("Cancelado/Desabilitado")] Disable = 98,
        [Description("Excluido")] Excluded = 99
    }
}