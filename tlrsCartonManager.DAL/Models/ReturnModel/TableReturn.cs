using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.DAL.Models
{
    public class TableReturn
    {
        public string OutValue { get; set; }
        public string Reason { get; set; }
    }
    public class CartonValidationResult
    {
        public int CartonNo { get; set; }
        public string Reason { get; set; }
        public bool  CanProcess { get; set; }
        public string CartonStatus { get; set; }
    }
    public class AlternativeValidationResult
    {
        public string AlternativeNo { get; set; }
        public int? CartonNo { get; set; }
        public string Reason { get; set; }
        public bool CanProcess { get; set; }
        public string CartonStatus { get; set; }
    }

    public class LoginValidationResult
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public int? Id { get; set; }
    }
}
