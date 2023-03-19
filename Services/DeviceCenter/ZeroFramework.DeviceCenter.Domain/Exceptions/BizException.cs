namespace ZeroFramework.DeviceCenter.Domain.Exceptions
{
    public class BizException : Exception
    {
        public BizException()
        {

        }

        public BizException(string message) : base(message) { }

        public BizException(string message, Exception ex) : base(message, ex) { }
    }
}
