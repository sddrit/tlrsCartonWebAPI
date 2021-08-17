using System;
using System.Collections.Generic;
using System.Linq;

namespace tlrsCartonManager.Api.Util.Permission
{
    public static class ModulePermissionsUtil
    {
        public static int[] GetPermissions(this int permission)
        {
            var binaryNumber = Convert.ToString(permission, 2);
            var permissions = new List<int>();

            if (permission == 0)
            {
                return permissions.ToArray();
            }

            for (var i = 0; i < binaryNumber.Length; i++)
            {
                var point = int.Parse(binaryNumber[binaryNumber.Length - (i + 1)].ToString());
                permissions.Add((int)(Math.Pow(2, i) * point));
            }

            return permissions.Where(p => p != 0).ToArray();
        }
    }
}
