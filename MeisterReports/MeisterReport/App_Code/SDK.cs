using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeisterSDKReporting.Controllers;
using MeisterSDKReporting.MeisterModel;
using MeisterCore.Support;
using System.Security;
using System.Text;
using System.Net.Http.Headers;

namespace MeisterReporting
{
    /// <summary>
    /// Summary description for SDK
    /// </summary>
    public class SDK
    {
        public MeisterSDKReporting.MeisterSDKReporting reportSdk { get; } = MeisterSDKReporting.MeisterSDKReporting.Instance; public SDK()
        {
        }
        public SDK(String userName, SecureString password, Uri gateway, string client, MeisterSupport.Languages language = MeisterSupport.Languages.CultureBased)
        {
            var credentials = Encoding.ASCII.GetBytes(userName + ":" + MeisterSupport.ToUnSecureString(password));
            reportSdk.AuthenticationHeaderValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            reportSdk.Gateway = gateway;
            reportSdk.Client = client;
            reportSdk.Language = language;
        }
        public MeisterStatus Authenticate()
        {
            reportSdk.Authenticate(reportSdk.Language);
            return reportSdk.MeisterStatus;
        }
        public string HttpsStatusDescription()
        {
            return reportSdk.MeisterStatus.StatusCodeDescription;
        }
    }
}