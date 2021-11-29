using System.Collections.Generic;

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
