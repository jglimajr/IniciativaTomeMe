using System.Collections.Generic;
using System.Linq;
using InteliSystem.Utils.Dapper.Extensions.Attributes;
using InteliSystem.Utils.Interfaces;

namespace InteliSystem.Utils.Notifications;

public abstract class InteliNotification : IClassBase
{
    private Dictionary<string, string> _messages;
    public InteliNotification()
    {
        _messages = new Dictionary<string, string>();
    }

    public void AddNotification(string property, string message)
    {
        this._messages.Add(property, message);
    }
    public void AddNotifications(IDictionary<string, string> add)
    {
        add.ToList().ForEach(reg =>
        {
            this._messages.Add(reg.Key, reg.Value);
        });
    }
    [WriteProperty(false)]
    public virtual IDictionary<string, string> GetAllNotifications => this._messages;
    [WriteProperty(false)]
    public virtual bool ExistNotifications => (this._messages.Count > 0);

}