using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Utility
{
    public static class CustomerStoredProcedureSearch
    {
        public static string StoredProcedureName = "customerSearch";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
           "@value",
           "@searColumn",
           "@sortOrder",
           "@pageIndex",
           "@pageSize",
           "@totalRecords"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters) + " OUTPUT";
    }
    public static class CustomerStoredProcedure
    {
        public static string StoredProcedureName = "customerInsertUpdateDelete";
        public static List<string> StoredProcedureParameters = new List<string>()
        {
            "@trackingId" ,
            "@customerCode",
            "@name",
            "@address1",
            "@address2",
            "@address3",
            "@telephone1",
            "@telephone2",
            "@fax",
            "@zipCode",
            "@countryId",
            "@email",
            "@contractNo",
            "@contractStartDate",
            "@contractEndDate",
            "@deliveryName",
            "@deliveryAddress1",
            "@deliveryAddress2 ",
            "@deliveryAddress3 ",
            "@deliveryTelephone1",
            "@deliveryTelephone2",
            "@deliveryFax  ",
            "@pickUpName ",
            "@pickUpAddress1",
            "@pickUpAddress2 ",
            "@pickUpAddress3",
            "@pickUpTelephone1",
            "@pickUpTelephone2",
            "@pickUpFax",
            "@city",
            "@contactName",
            "@contactAddress1",
            "@contactAddress2",
            "@contactAddress3",
            "@contactTelephone1",
            "@contactTelephone2",
            "@contactFax",
            "@poNo",
            "@vatNo",
            "@svatNo",
            "@billingCycle",
            "@route",
            "@isSeparateInvoice",
            "@contactPersonInv",
            "@subInvoice",
            "@serviceProvided",
            "@accountType",
            "@mainCustomerCode",
            "@active",
            "@user",
            "@statementType",
            "@authorizationHeader",
            "@authorizationDetail",
            "@includeMainAccountAuthorization",
            "@isManualInvoice",
            "@billingName",
            "@docketCopies"

        };
        public static List<string> StoredProcedureTypeNames = new List<string>()
        {
            "dbo.udtCustomerAuthorizationListHeader",
            "dbo.udtCustomerAuthorizationListDetail"
        };
        public static string Sql = "EXEC " + StoredProcedureName + " " + string.Join(",", StoredProcedureParameters);
    }
}
