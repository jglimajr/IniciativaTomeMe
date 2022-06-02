using System;
using System.Data;

namespace InteliSystem.Utils.Interfaces
{
    public interface IRepository : IDisposable
    {
        IDbConnection Connection { get; }
    }
}