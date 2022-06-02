using System;
using System.ComponentModel;

namespace InteliSystem.Utils.Enumerators
{
    public enum DiscountType
    {
        [Description("Valor")] Value = 1,
        [Description("Percentual")] Percentage
    }
}