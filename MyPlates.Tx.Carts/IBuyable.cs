using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TxDot.Web.Services;

namespace MyPlates.Tx.Carts
{
    public interface IBuyable
    {
        Guid GetItemId();
        string GetTraceNumber();
        string GetDescription();
        void CreateOrder(int orderId);
        TxDotResponse ProcessOrder(string traceNumber, int orderId, DateTime ePaySendTime, DateTime ePayReceiveTime);
        decimal GetTotalCost();
        void GenerateNewTraceNumber();
    }
}
