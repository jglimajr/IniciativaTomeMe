using System;
using System.Text.Json.Serialization;
using InteliSystem.Utils.Enumerators;

namespace InteliSystem.Utils.GlobalClasses
{
    public abstract class ClassRuleBase
    {
        public virtual string? Id { get; set; }
        public virtual StatusValues Status { get; set; }
    }
}