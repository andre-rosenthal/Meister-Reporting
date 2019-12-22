using MeisterCore;
using System;

namespace MeisterReporting
{
    /// <summary>
    /// Summary description for Model
    /// </summary>
    public class Model
    {

        public Controller Controller { get; private set; }
        public MeisterCore.MeisterException MeisterException { get; set; }
        public Model()
        {
            Controller = new Controller();
        }

        public RES RunMeister<REQ,RES>(REQ req, string call, out MeisterException exception)
        {
            exception = null;
            try
            {
                return Controller.ExecuteRequest<REQ, RES>(call, req);
            }
            catch (MeisterCore.MeisterException me)
            {
                exception = me;
                return default(RES);
            }
        }
    }
}