using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MeisterCore;

namespace MeisterReporting
{
    /// <summary>
    /// Summary description for Controller
    /// </summary>
    public class Controller
    {
        public bool IsOD4 { get; set; }
        public Controller()
        {

        }
        private AuthenticationHeaderValue headerValue { get; set; }
        private Uri Gateway { get; set; }
        private string GatewayClient { get; set; }
        private MeisterCore.Support.MeisterSupport.MeisterExtensions me { get; set; }
        private MeisterCore.Support.MeisterSupport.MeisterOptions mo { get; set; }
        private MeisterCore.Support.MeisterSupport.RuntimeOptions ro { get; set; }
        private MeisterCore.Support.MeisterSupport.AuthenticationModes am { get; set; }
        protected Resource<dynamic, dynamic> resource;
        HttpClient Client { get; set; }
        public bool Authenticate(string userid, string psw, Uri gatewayUri, string sapclient, bool od4)
        {
            string bae = userid + ":" + psw;
            var byteArray = Encoding.ASCII.GetBytes(bae);
            headerValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            me = MeisterCore.Support.MeisterSupport.MeisterExtensions.RemoveNullsAndEmptyArrays;
            mo = MeisterCore.Support.MeisterSupport.MeisterOptions.None;
            if (od4)
                mo = MeisterCore.Support.MeisterSupport.MeisterOptions.UseODataV4;
            ro = MeisterCore.Support.MeisterSupport.RuntimeOptions.ExecuteSync;
            Gateway = gatewayUri;
            GatewayClient = sapclient;
            MeisterCore.Support.MeisterSupport.AuthenticationModes am = MeisterCore.Support.MeisterSupport.AuthenticationModes.Basic;
            resource = new Resource<dynamic, dynamic>(gatewayUri, headerValue, sapclient, me, mo, am, ro,MeisterCore.Support.MeisterSupport.Languages.CultureBased);
            return resource.Authenticate();
        }
        public T ExecuteRequest<R, T>(string ep, R req)
        {
            Resource<R, T> resource = new Resource<R, T>(Gateway, headerValue, GatewayClient, me, mo, am, ro,MeisterCore.Support.MeisterSupport.Languages.CultureBased);
            return resource.Execute(ep, req);
        }
        public void CleanUp()
        {
            resource = null;
        }
        public AuthenticationHeaderValue Authenticate()
        {
            string uid = string.Empty;
            string psw = string.Empty;
            uid = ConfigurationManager.AppSettings["UserName"];
            psw = ConfigurationManager.AppSettings["Password"];
            string uap = string.Format("{0},{1}", uid, psw);
            var byteArray = Encoding.ASCII.GetBytes(uap);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
}