using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Web.Pages.Basket;

namespace Microsoft.eShopWeb.Web.Interfaces
{
    public interface IAzureService
    {
        Task SendToServiceBus(BasketViewModel items);

        Task SendToFunction(Order order);

    }
}
