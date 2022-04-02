using Ardalis.SmartEnum;

namespace AurSystem.Framework.Models;

public sealed class OrderStatus : SmartEnum<OrderStatus>
{
    public static readonly OrderStatus Pending = new OrderStatus(nameof(Pending), 1);
    public static readonly OrderStatus Processing = new OrderStatus(nameof(Processing), 2);
    public static readonly OrderStatus Faulted = new OrderStatus(nameof(Faulted), 3);
    public static readonly OrderStatus Completed = new OrderStatus(nameof(Completed), 4);
    public OrderStatus(string name, int value) : base(name, value)
    {
    }
}
