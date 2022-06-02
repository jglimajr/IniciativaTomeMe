using System;
using System.Collections.Generic;
using FluentValidator;
using InteliSystem.Utils.Interfaces;
using InteliSystem.Utils.Notifications;

namespace InteliSystem.Utils.GlobalClasses
{
    public abstract class RulesBase : InteliNotification, IClassBase, IDisposable
    {
        private IRepository _repository;
        protected RulesBase(IRepository repository) => _repository = repository;

        public virtual void Dispose() => this._repository?.Dispose();
    }
}