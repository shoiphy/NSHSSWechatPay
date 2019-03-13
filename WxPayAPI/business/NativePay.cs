using System;
using System.Collections.Generic;
using System.Web;

namespace WxPayAPI
{
    public class NativePay
    {
        /**
        * Generate a scan payment mode-URL
        * @param productId product id
        * @return mode 1 URL
        */
        public string GetPrePayUrl(string productId)
        {
            Log.Info(this.GetType().ToString(), "Native pay mode 1 url is producing...");

            WxPayData data = new WxPayData();
            data.SetValue("appid", WxPayConfig.GetConfig().GetAppID());//official account id 
            data.SetValue("mch_id", WxPayConfig.GetConfig().GetMchID());//vendor id
            data.SetValue("time_stamp", WxPayApi.GenerateTimeStamp());//time stamp
            data.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//random string
            data.SetValue("product_id", productId);//product ID
            data.SetValue("sign", data.MakeSign());//signature
            string str = ToUrlParams(data.GetValues());//convert to url string
            string url = "weixin://wxpay/bizpayurl?" + str;

            Log.Info(this.GetType().ToString(), "Get native pay mode 1 url : " + url);
            return url;
        }

        /**
        * Generate direct payment url, payment url is valid for 2 hours, mode 2
        * @param productId product ID
        * @return mode 2 URL
        */
        public string GetPayUrl(string productId)
        {
            Log.Info(this.GetType().ToString(), "Native pay mode 2 url is producing...");

            WxPayData data = new WxPayData();
            data.SetValue("body", "test");//product description
            data.SetValue("attach", "test");//attachments
            data.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());//random string
            data.SetValue("total_fee", 1);//total amount fee(price unit is cent)
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//order start time
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//order end time
            data.SetValue("goods_tag", "jjj");//goods tag
            data.SetValue("trade_type", "NATIVE");//order type
            data.SetValue("product_id", productId);//product id

            WxPayData result = WxPayApi.UnifiedOrder(data);//Call the unified order interface
            string url = result.GetValue("code_url").ToString();//Get the QR code link returned by the unified order interface

            Log.Info(this.GetType().ToString(), "Get native pay mode 2 url : " + url);
            return url;
        }

        /**
        * Convert parameter array to url format
        * @param map Mapping table of keys and values
        * @return URL string
        */
        private string ToUrlParams(SortedDictionary<string, object> map)
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in map)
            {
                buff += pair.Key + "=" + pair.Value + "&";
            }
            buff = buff.Trim('&');
            return buff;
        }
    }
}