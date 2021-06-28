using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public class CompanyProfileStoredProcedure
    {

        public static string StoredProcedureName = "companyProfileUpdate";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@id",
            "@companyName",
            "@address1",
            "@address2",
            "@address3",
            "@country",
            "@tel",
            "@fax",
            "@email",
            "@vatNo",
            "@nbtNo",
            "@svatNo",
            "@userId",
            "@taxEffectiveDate"
            };
        public static List<string> StoredProcedureTypeNames = new List<string>()
        {
            "dbo.udtTaxEffectiveDate",

        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);

    }


}
