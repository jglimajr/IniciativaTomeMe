using System;
using System.ComponentModel;

namespace InteliSystem.Utils.Enumerators
{
    public enum ShoppingStatus
    {
        [Description("Em Aberto")] Open = 1,
        [Description("Finalizado")] Close,
        [Description("Sem Preço")] WithoutPrice,
        [Description("Cancelado")] Canceled = 99
    }
}