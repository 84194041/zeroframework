namespace ZeroFramework.DeviceCenter.Domain.Exceptions
{
    /// <summary>
    /// 自定义业务异常，可以由前端抛出友好的提示
    /// </summary>
    public class BizException : Exception
    {
        public BizException()
        {

        }

        public BizException(string message) : base(message) { }

        public BizException(string message, Exception ex) : base(message, ex) { }
    }
}
