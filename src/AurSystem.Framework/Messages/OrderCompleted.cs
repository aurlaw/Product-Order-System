using AurSystem.Framework.Models;
using AurSystem.Framework.Models.Domain;

namespace AurSystem.Framework.Messages;

public interface OrderCompleted : FutureCompleted
{
    Guid OrderId { get; }
    Order Order { get; }
    string Status { get; }
}