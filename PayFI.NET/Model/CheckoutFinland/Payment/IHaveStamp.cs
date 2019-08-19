using System;

namespace PayFI.NET.Library.Model.CheckoutFinland.Payment
{
    public interface IHaveStamp
    {
        Guid Stamp { get; }
    }

    public interface IHaveOrderId
    {
        string OrderId { get; }
    }

    public interface IHaveTransactionId
    {
        Guid TransactionId { get; }
    }

    public interface IHaveReference
    {
        string Reference { get; }
    }
}
