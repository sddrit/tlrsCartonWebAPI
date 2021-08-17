using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.Core.Enums
{
    public enum ModulePermission
    {
        None = 0,
        Add = 1,
        Edit = 2,
        Delete = 4,
        Print = 8,
        View = 16,
        All = 32
    }
}
