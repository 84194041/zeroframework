using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace ZeroFramework.Payment.WeChat.Services
{
    public class HttpService
    {
        public static async Task<string> Post(HttpClient? httpClient, string xml, string url, bool isUseCert, int timeout, string certPath = null!, string certPwd = null!)
        {
            var httpclientHandler = new HttpClientHandler();

            //是否使用证书
            if (isUseCert)
            {
                if (string.IsNullOrEmpty(certPath))
                {
                    throw new Exception("证书路径为空");
                }

                if (!File.Exists(certPath))
                {
                    throw new Exception("证书不存在");
                }
                if (string.IsNullOrEmpty(certPwd))
                {
                    throw new Exception("证书密钥为空");
                }

                httpclientHandler.ClientCertificates.Add(new X509Certificate2(certPath, certPwd));
            }

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                httpclientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true;
            }

            var content = new StringContent(xml!);
            content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            httpClient!.Timeout = TimeSpan.FromSeconds(timeout);

            var httpContent = await httpClient.PostAsync(url, content);

            var dataAsString = await httpContent.Content.ReadAsStringAsync();

            return dataAsString;
        }
    }
}
