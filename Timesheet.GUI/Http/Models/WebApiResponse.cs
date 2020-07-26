using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Timesheet.GUI.Http.Models
{
    public class ErrorItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Message { get; set; }
    }

    public class WebApiResponse<T>
    {
        public bool IsSucceded { get; set; }
        public string Message { get; set; }
        public List<ErrorItem> Errors { get; set; }
        public T Response { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
