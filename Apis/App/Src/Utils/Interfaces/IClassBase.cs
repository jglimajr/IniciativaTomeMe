using System;
using System.Collections.Generic;
using FluentValidator;

namespace InteliSystem.Utils.Interfaces
{
    public interface IClassBase
    {
        IDictionary<string, string> GetAllNotifications { get; }
        bool ExistNotifications { get; }
    }
}