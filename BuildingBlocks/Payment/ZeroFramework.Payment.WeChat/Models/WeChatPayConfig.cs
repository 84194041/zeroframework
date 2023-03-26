namespace ZeroFramework.Payment.WeChat.Models
{
    /// <summary>
    /// 配置账号信息
    /// </summary>
    public class WeChatPayConfig
    {
        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置）
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置）
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
        */
        public string? APPID { get; private set; } // = "wxf8cd6503bc401637";

        public string? MCHID { get; private set; } //= "1273453801";

        public string? KEY { get; private set; }  //= "5D3C38DC652742ADB20C849665616838";

        public string? APPSECRET { get; private set; }  //= "51c56b886b5be869567dd389b3e5d1d6";

        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public string? SSLCERT_PATH { get; private set; } //= "WxPay/cert/apiclient_cert.p12";

        public string? SSLCERT_PASSWORD { get; private set; }  //= "1233410002";

        public string? SUB_APPID { get; private set; }  //= "wxe694f1b44a262b3e";

        public string? SUB_MAC_ID { get; private set; } // = "1241414402";

        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        public const string NOTIFY_URL = "http://paysdk.weixin.qq.com/example/ResultNotifyPage.aspx";

        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        public const string IP = "8.8.8.8";

        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        public const string PROXY_URL = "http://10.152.18.220:8080";

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public const int REPORT_LEVENL = 1;

        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public const int LOG_LEVENL = 3;

        public WeChatPayConfig(string appId, string mchId, string key, string appSecret, string sslCert_Path, string sslCert_Password, string sub_appid, string sub_mch_id, string platformPath)
        {
            this.APPID = appId;
            this.MCHID = mchId;
            this.KEY = key;
            this.APPSECRET = appSecret;
            this.SSLCERT_PATH = sslCert_Path;
            this.SSLCERT_PASSWORD = sslCert_Password;
            this.SUB_MAC_ID = sub_mch_id;
            this.SUB_APPID = sub_appid;

            if (SSLCERT_PATH.StartsWith("/") || SSLCERT_PATH.StartsWith("\\"))
            {
                SSLCERT_PATH = SSLCERT_PATH.Substring(1);
            }

            if (!string.IsNullOrEmpty(platformPath) && !string.IsNullOrEmpty(SSLCERT_PATH))
            {
                this.SSLCERT_PATH = Path.Combine(platformPath, SSLCERT_PATH);
            }
        }
    }
}
