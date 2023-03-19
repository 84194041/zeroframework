using ZeroFramework.DeviceCenter.Application.Services.Generics;

namespace ZeroFramework.DeviceCenter.Application.Models.Products
{
    public class MeasurementUnitPagedRequestModel : PagedRequestModel
    {
        public string? Keyword { get; set; }
    }
}
