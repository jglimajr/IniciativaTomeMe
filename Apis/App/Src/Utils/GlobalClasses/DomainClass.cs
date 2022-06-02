using System;
using System.ComponentModel.DataAnnotations;
using InteliSystem.Utils.Dapper.Extensions.Attributes;
using InteliSystem.Utils.Enumerators;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Interfaces;

namespace InteliSystem.Utils.GlobalClasses
{
    public abstract class DomainClass : ClassBase, IClassBase
    {
        public DomainClass()
        {
            Id = Guid.NewGuid().ToOnlyKey();
            Status = StatusValues.Enable;
            DateCreation = DateTime.Now;
            DateUpdate = DateTime.Now;
        }

        public DomainClass(string? id, StatusValues status)
            : this()
        {
            Id = id;
            Status = status;
        }
        [Key]
        public string? Id { get; private set; }
        public virtual StatusValues Status { get; private set; }
        [InsertProperty, UpdateProperty(false)]
        public DateTime? DateCreation { get; private set; }
        [InsertProperty(false), UpdateProperty]
        public DateTime? DateUpdate { get; private set; }
    }
}