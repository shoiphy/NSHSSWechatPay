using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace WxPayAPI
{
    /// <summary>
    /// Callback processing base class
    /// Mainly responsible for receiving data sent from the WeChat payment backend, and verifying the data.
    /// Subclasses are derived on this basis and override their own callback processing
    /// </summary>
    public class Notify
    {
        public Page page {get;set;}
        public Notify(Page page)
        {
            this.page = page;
        }

        /// <summary>
        /// Receive data sent from the WeChat payment background and verify the signature
        /// </summary>
        /// <returns>Data returned by WeChat payment backend</returns>
        public WxPayData GetNotifyData()
        {
            //Receive data from the WeChat backend POST
            System.IO.Stream s = page.Request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = s.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            s.Flush();
            s.Close();
            s.Dispose();

            Log.Info(this.GetType().ToString(), "Receive data from WeChat : " + builder.ToString());

            //Convert data format and verify signature
            WxPayData data = new WxPayData();
            try
            {
                data.FromXml(builder.ToString());
            }
            catch(WxPayException ex)
            {
                //If the signature is wrong, the result will be returned immediately to the WeChat payment backend.
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", ex.Message);
                Log.Error(this.GetType().ToString(), "Sign check error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            Log.Info(this.GetType().ToString(), "Check sign success");
            return data;
        }

        //Derived classes need to override this method for different callback processing
        public virtual void ProcessNotify()
        {

        }
    }
}