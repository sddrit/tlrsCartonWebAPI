using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tlrsCartonManager.Api.Error
{
    public class ApiErrorMessage
    {
        public List<ApiErrorMessageDescription> Errors { get; set; }
    }
    public class ApiErrorMessageDescription
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
