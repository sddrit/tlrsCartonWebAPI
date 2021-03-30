using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public static class Status
    {
        public enum TransactionStatus
        {
            None,
            Active,
            InActive,
            Disable
        }
          public enum TransactionTypes
        {
            Insert,
            Update,
            Delete
        }
    }
}
