using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidator;
using InteliSystem.Utils.Dapper.Extensions.Attributes;
using InteliSystem.Utils.Enumerators;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Interfaces;

namespace InteliSystem.Utils.GlobalClasses
{
    public abstract class DomainBase : ClassBase
    {
        public DomainBase()
        {
            this.Id = Guid.NewGuid().ToStringKey();
            this.Status = StatusValues.Enable;
            this.DateCreation = DateTime.Now;
            this.DateUpdate = DateTime.Now;
        }
        protected DomainBase(string id, StatusValues status)
            : this()
        {
            Id = id;
            Status = status;
        }
        [Key()]
        public virtual string Id { get; private set; }
        public virtual StatusValues Status { get; private set; }
        [InsertProperty(true), UpdateProperty(false)]
        public DateTime DateCreation { get; private set; }
        [InsertProperty(false), UpdateProperty(true)]
        public DateTime DateUpdate { get; private set; }

        public void SetStatus(StatusValues status) => this.Status = status;
        public void SetDateUpdateToNow() => this.DateUpdate = DateTime.Now;
        public void SetId(string id) => this.Id = id;
    }
}