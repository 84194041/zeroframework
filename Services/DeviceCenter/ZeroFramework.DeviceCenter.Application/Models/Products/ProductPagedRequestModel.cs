using ZeroFramework.DeviceCenter.Application.Services.Generics;

namespace ZeroFramework.DeviceCenter.Application.Models.Products
{
    public class ProductPagedRequestModel : PagedRequestModel
    {
        public string? Keyword { get; set; }
    }
}
