using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WxPayAPI.example
{
    public partial class H5PayPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            Log.Info(this.GetType().ToString(), "button click");
            H5Pay h5Pay = new H5Pay();
            string scip = GetIP();//Get the real IP of the client

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["total_fee"] = total_fee.Text;//1 , price unit: cent
            dic["out_trade_no"] = out_trade_no.Text; // "orderno123456";
            dic["body"] = body.Text; // "product_info";
            dic["attach"] = attach.Text; // "attachment";
            dic["wap_url"] = "http://www.nshss.cn/";
            dic["wap_name"] = "NSHSS";
            dic["redirect_url"] = "http://www.nshss.cn/";
            string url = h5Pay.GetPayUrl(scip,dic);//H5 payment through unified order interface

            Response.Redirect(url);//Jump to the WeChat payment middle page
        }

        //Because the H5 payment requires the merchant to upload the user's real ip address "spbill_create_ip" in the unified order interface, the following method needs to be called.
        public string GetIP()
        {
            HttpRequest request = HttpContext.Current.Request;
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = "0.0.0.0";
            }
            return result;
        }
    }
}