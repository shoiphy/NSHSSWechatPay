using System;
namespace WxPayAPI.lib
{
    public class DemoConfig:IConfig
    {
        public DemoConfig()
        {
        }


        //=======【Basic setting】=====================================
        /* WeChat official account information configuration
        * APPID：Bind payment APPID (must be configured)
        * MCHID：Vendor number (must be configured)
        * KEY：Vendor payment key, refer to account opening email settings (must be configured), please keep it safe to avoid key leakage
        * APPSECRET：Public account secert (only need to be configured when JSAPI pays), please keep it safely to avoid key leakage
        */

        public string GetAppID(){
            return "wxf1fe2700317ddd8f";
        }
        public string GetMchID(){
            return "1517219131";
        }
        public string GetKey(){
            return "hyb123456HYB123456hyb123456HYB12";
        }
        public string GetAppSecret(){
            return "ef807adf869f9dc545ffdbef6bf9457b";
        }



        //=======【Certificate path setting】===================================== 
        /* Certificate path, note that you should fill in the absolute path (required only for refunds or cancellations)
         * 1.The certificate file cannot be placed in the web server virtual directory, and should be placed in a directory with access control to prevent others from downloading;
         * 2.It is recommended to change the certificate file name to a file that is complicated and not easy to guess.
         * 3.The vendor server should be protected against viruses and Trojans and not be hacked by the hackers.
        */
        public string GetSSlCertPath(){
            return "";
        }
        public string GetSSlCertPassword(){
            return "";
        }



        //=======【Payment result notification url】===================================== 
        /* Payment result notification callback url for merchants to receive payment results
        */
        public string GetNotifyUrl(){
            return "http://47.98.167.97/example/ResultNotifyPage.aspx";//notify page url
        }

        //=======【Vendor system backend machine IP】===================================== 
        /* This parameter can be configured manually or automatically in the program.
        */
        public string GetIp(){
            return "0.0.0.0";
        }


        //=======【Proxy server settings】===================================
        /* The default IP and port numbers are 0.0.0.0 and 0 respectively. The agent is not enabled at this time (set if necessary)
        */
        public string GetProxyUrl(){
            return "";
        }


        //=======【Reporting information configuration】===================================
        /* Speed report level, 0. Close report; 1. Report only when error occurs; 2. Full report
        */
        public int GetReportLevel(){
            return 1;
        }


        //=======【Log level】===================================
        /* Log level, 0. No output log; 1. Output only error information; 2. Output error and normal information; 3. Output error information, normal information and debugging information
        */
        public int GetLogLevel(){
            return 1;
        }
    }
}
