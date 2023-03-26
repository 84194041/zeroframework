using ZeroFramework.Payment.WeChat.Models;

namespace ZeroFramework.Payment.WeChat.Services
{
    public class WeChatPayApiService
    {
        private readonly WeChatPayConfig weChatPayConfig;

        private readonly IHttpClientFactory _httpClientFactory;

        public WeChatPayApiService(WeChatPayConfig weChatPayConfig, IHttpClientFactory httpClientFactory)
        {
            this.weChatPayConfig = weChatPayConfig;
            _httpClientFactory = httpClientFactory;
        }

        /**
        * 提交被扫支付API
        * 收银员使用扫码设备读取微信用户刷卡授权码以后，二维码或条码信息传送至商户收银台，
        * 由商户收银台或者商户后台调用该接口发起支付。
        * @param WxPayData inputObj 提交给被扫支付API的参数
        * @param int timeOut 超时时间
        * @throws WxPayException
        * @return 成功时返回调用结果，其他抛异常
        */
        public WeChatPayReuestModel Micropay(WeChatPayReuestModel inputObj, int timeOut = 10)
        {
            string url = "https://api.mch.weixin.qq.com/pay/micropay";

            //检测必填参数
            if (!inputObj.IsSet("body"))
            {
                throw new Exception("提交被扫支付API接口中，缺少必填参数body！");
            }
            else if (!inputObj.IsSet("out_trade_no"))
            {
                throw new Exception("提交被扫支付API接口中，缺少必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new Exception("提交被扫支付API接口中，缺少必填参数total_fee！");
            }
            else if (!inputObj.IsSet("auth_code"))
            {
                throw new Exception("提交被扫支付API接口中，缺少必填参数auth_code！");
            }

            inputObj.SetValue("spbill_create_ip", WeChatPayConfig.IP); //终端ip

            inputObj.SetValue("appid", weChatPayConfig.APPID!); //公众账号ID  

            inputObj.SetValue("mch_id", weChatPayConfig.MCHID!); //商户号

            inputObj.SetValue("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串

            if (!string.IsNullOrEmpty(weChatPayConfig.SUB_MAC_ID)) inputObj.SetValue("sub_mch_id", weChatPayConfig.SUB_MAC_ID); //子商户号

            if (!string.IsNullOrEmpty(weChatPayConfig.SUB_APPID)) inputObj.SetValue("sub_appid", weChatPayConfig.SUB_APPID); //子公众账号ID

            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();

            //var start = DateTime.Now;//请求开始时间

            string response = HttpService.Post(_httpClientFactory.CreateClient(), xml, url, false, timeOut).Result;//调用HTTP通信接口以提交数据到API

            //var end = DateTime.Now;

            //int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的结果转换为对象以返回
            WeChatPayReuestModel result = new(weChatPayConfig.KEY!);
            result.FromXml(response);

            return result;
        }

        /**
        *    
        * 查询订单
        * @param WxPayData inputObj 提交给查询订单API的参数
        * @param int timeOut 超时时间
        * @throws WxPayException
        * @return 成功时返回订单查询结果，其他抛异常
        */
        public WeChatPayReuestModel OrderQuery(WeChatPayReuestModel inputObj, int timeOut = 30)
        {
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new Exception("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }

            inputObj.SetValue("appid", weChatPayConfig.APPID!);//公众账号ID
            inputObj.SetValue("mch_id", weChatPayConfig.MCHID!);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串

            if (!string.IsNullOrEmpty(weChatPayConfig.SUB_MAC_ID)) inputObj.SetValue("sub_mch_id", weChatPayConfig.SUB_MAC_ID);//子商户号

            if (!string.IsNullOrEmpty(weChatPayConfig.SUB_APPID)) inputObj.SetValue("sub_appid", weChatPayConfig.SUB_APPID);//子公众账号ID

            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();

            var start = DateTime.Now;

            //Log.Debug("WxPayApi", "OrderQuery request : " + xml);
            string response = HttpService.Post(_httpClientFactory.CreateClient(), xml, url, false, timeOut).Result;//调用HTTP通信接口提交数据
            //Log.Debug("WxPayApi", "OrderQuery response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的数据转化为对象以返回
            WeChatPayReuestModel result = new WeChatPayReuestModel(weChatPayConfig.KEY!);
            result.FromXml(response);

            return result;
        }

        /**
        * 
        * 撤销订单API接口
        * @param WxPayData inputObj 提交给撤销订单API接口的参数，out_trade_no和transaction_id必填一个
        * @param int timeOut 接口超时时间
        * @throws WxPayException
        * @return 成功时返回API调用结果，其他抛异常
        */
        public WeChatPayReuestModel Reverse(WeChatPayReuestModel inputObj, int timeOut = 30)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/reverse";

            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new Exception("撤销订单API接口中，参数out_trade_no和transaction_id必须填写一个！");
            }

            inputObj.SetValue("appid", weChatPayConfig.APPID!);//公众账号ID
            inputObj.SetValue("mch_id", weChatPayConfig.MCHID!);//商户号
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串

            if (!string.IsNullOrEmpty(weChatPayConfig.SUB_MAC_ID)) inputObj.SetValue("sub_mch_id", weChatPayConfig.SUB_MAC_ID);//子商户号

            if (!string.IsNullOrEmpty(weChatPayConfig.SUB_APPID)) inputObj.SetValue("sub_appid", weChatPayConfig.SUB_APPID);//子公众账号ID

            inputObj.SetValue("sign", inputObj.MakeSign());//签名


            string xml = inputObj.ToXml();

            var start = DateTime.Now;//请求开始时间

            //Log.Debug("WxPayApi", "Reverse request : " + xml);

            string response = HttpService.Post(_httpClientFactory.CreateClient(), xml, url, true, timeOut, weChatPayConfig.SSLCERT_PATH!, weChatPayConfig.SSLCERT_PASSWORD!).Result;

            //Log.Debug("WxPayApi", "Reverse response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);

            WeChatPayReuestModel result = new WeChatPayReuestModel(weChatPayConfig.KEY!);
            result.FromXml(response);

            return result;
        }

        /**
        * 生成随机串，随机串包含字母或数字
        * @return 随机串
        */
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /**
        * 根据当前系统时间加随机序列来生成订单号
        * @return 订单号
        */
        public string GenerateOutTradeNo()
        {
            var ran = new Random();
            return string.Format("{0}{1}{2}", weChatPayConfig.MCHID, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }
    }
}
