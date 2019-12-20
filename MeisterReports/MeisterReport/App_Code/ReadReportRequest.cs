using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeisterReporting
{
    /// <summary>
    /// Summary description for ReadReportRequest
    /// </summary>
    public class ReadReportRequest
    {
        public string reportGuid { get; set; }
        public bool keepReport { get; set; }
    }
}
