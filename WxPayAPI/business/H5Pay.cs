using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WxPayAPI
{
    public class H5Pay
    {
        /// <summary>
        /// get pay url
        /// </summary>
        /// <param name="thip">ip address</param>
        /// <param name="total_fee">total fee (unit:cent)</param>
        /// <param name="out_trade_no">order no</param>
        /// <param name="body">body</param>
        /// <param name="attach">attachment</param>
        /// <param name="wap_url"></param>
        /// <param name="wap_name"></param>
        /// <param name="redirect_url"> Callback page's url after payment is completed</param>
        /// <returns></returns>
        public string GetPayUrl(string thip,int total_fee=1,string out_trade_no = "123456",string body = "body",string attach = "attach",string wap_url="www.nshss.cn",string wap_name="NSHSS",string redirect_url="www.nshss.cn")
        {
            Log.Info(this.GetType().ToString(), "H5 pay url is producing...");

            WxPayData data = new WxPayData();

            data.SetValue("body",body);
            data.SetValue("attach", attach);
            data.SetValue("out_trade_no",out_trade_no);
            data.SetValue("total_fee",total_fee);
            data.SetValue("spbill_create_ip", thip);//client IP  
            data.SetValue("trade_type", "MWEB");//trade type
            data.SetValue("scene_info", "{'h5_info':{'type':'Wap','wap_url':'"+wap_url+"','wap_name':'"+wap_name+"'}}");//scene information

            WxPayData result = WxPayApi.UnifiedOrder(data);//Call unified order interface 
            string url = result.GetValue("mweb_url").ToString();//Get the link returned by the unified order interface

            if (!string.IsNullOrEmpty(redirect_url)) url += "&redirect_url=" + redirect_url;// Callback page's url after payment is completed

            Log.Info(this.GetType().ToString(), "Get H5 pay url : " + url);
            return url;
        }

        //string thip, int total_fee = 1, string out_trade_no = "123456", string body = "body", string attach = "attach", string wap_url = "www.nshss.cn", string wap_name = "NSHSS", string redirect_url = "www.nshss.cn"

        /// <summary>
        /// get pay url
        /// </summary>
        /// <param name="thip">ip address</param>
        /// <param name="dic">parameters: total_fee,out_trade_no ,body , attach , wap_url , wap_name , redirect_url</param>
        /// <returns></returns>
        public string GetPayUrl(string thip ,Dictionary<string,string> dic)
        {
            return GetPayUrl(thip, Convert.ToInt32(dic["total_fee"]), dic["out_trade_no"], dic["body"], dic["attach"], dic["wap_url"], dic["wap_name"], dic["redirect_url"]);
            
        }
    }
}