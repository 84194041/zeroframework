﻿namespace ZeroFramework.DeviceCenter.Domain.Exceptions
{
    public class ApiResult
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public object? Data { get; set; }
    }
}
