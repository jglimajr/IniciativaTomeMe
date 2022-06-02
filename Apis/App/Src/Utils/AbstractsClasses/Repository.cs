using System.Data;
using System.Data.Common;
using InteliSystem.Utils.Interfaces;

namespace InteliSystem.Utils.AbstractsClasses
{
    public abstract class Repository : IRepository
    {
        private IDbConnection _conn;

        protected Repository(IDbConnection conn) => this._conn = conn;
        public virtual IDbConnection Connection => this._conn;

        public void Dispose()
        {
            this._conn?.Dispose();
        }
    }
}