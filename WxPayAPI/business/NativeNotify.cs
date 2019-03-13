using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WxPayAPI
{
    /// <summary>
    /// Scan QRcode payment mode 1, a callback processing class
    /// Receive the scan result sent by the WeChat payment backend, call the unified order interface and return the order result to the WeChat payment backend.
    /// </summary>
    public class NativeNotify:Notify
    {
        public NativeNotify(Page page):base(page)
        {

        }

        public override void ProcessNotify()
        {
            WxPayData notifyData = GetNotifyData();//notify data from wechat backend

            //Check if openid and product_id return
            if (!notifyData.IsSet("openid") || !notifyData.IsSet("product_id"))
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "回调数据异常");//call error message
                Log.Info(this.GetType().ToString(), "The data WeChat post is error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            //call the unified interface to obtain the order result
            string openid = notifyData.GetValue("openid").ToString();
            string product_id = notifyData.GetValue("product_id").ToString();
            WxPayData unifiedOrderResult = new WxPayData();
            try
            {
                unifiedOrderResult = UnifiedOrder(openid, product_id);
            }
            catch(Exception ex)//If the exception is thrown when the unified interface is called, the result is immediately returned to the WeChat payment backend.
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "统一下单失败");//unified order error
                Log.Error(this.GetType().ToString(), "UnifiedOrder failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            //If the order fails, the result will be returned immediately to the WeChat payment backend.
            if (!unifiedOrderResult.IsSet("appid") || !unifiedOrderResult.IsSet("mch_id") || !unifiedOrderResult.IsSet("prepay_id"))
            {
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "统一下单失败");//unified order error
                Log.Error(this.GetType().ToString(), "UnifiedOrder failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            //If the unified order is successful, the successful result will be returned to the WeChat payment backend.
            WxPayData data = new WxPayData();
            data.SetValue("return_code", "SUCCESS");
            data.SetValue("return_msg", "OK");
            data.SetValue("appid", WxPayConfig.GetConfig().GetAppID());
            data.SetValue("mch_id", WxPayConfig.GetConfig().GetMchID());
            data.SetValue("nonce_str", WxPayApi.GenerateNonceStr());
            data.SetValue("prepay_id", unifiedOrderResult.GetValue("prepay_id"));
            data.SetValue("result_code", "SUCCESS");
            data.SetValue("err_code_des", "OK");
            data.SetValue("sign", data.MakeSign());

            // Rob do this
            try
            {
                // When payment success, do something to save data to database
            }
            catch (Exception e)
            {

            }

            Log.Info(this.GetType().ToString(), "UnifiedOrder success , send data to WeChat : " + data.ToXml());
            page.Response.Write(data.ToXml());
            page.Response.End();
        }

        private WxPayData UnifiedOrder(string openId,string productId)
        {
            //Unified Order
            WxPayData req = new WxPayData();
            req.SetValue("body", "test");
            req.SetValue("attach", "test");
            req.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());
            req.SetValue("total_fee", 1);
            req.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            req.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            req.SetValue("goods_tag", "test");
            req.SetValue("trade_type", "NATIVE");
            req.SetValue("openid", openId);
            req.SetValue("product_id", productId);
            WxPayData result = WxPayApi.UnifiedOrder(req);
            return result;
        }
    }
}