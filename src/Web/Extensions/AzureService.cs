using Microsoft.eShopWeb.Web.Pages.Basket;
using Microsoft.eShopWeb.Web.Interfaces;
using Azure.Messaging.ServiceBus;
using System.Text.Json;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System.Text;
using System.Net.Mime;
using System.Net.Http.Headers;

namespace Microsoft.eShopWeb.Web.Extensions
{
    public class AzureService : IAzureService
    {
        public ServiceBusSender Sender { get; private set; }
        public AzureService(string connectionString, string queueName)
        {
            ServiceBusClient client = new ServiceBusClient(connectionString);
            Sender = client.CreateSender(queueName);
        }

        public async Task SendToServiceBus(BasketViewModel items)
        {
            await Sender.SendMessageAsync(new ServiceBusMessage(new BinaryData(JsonSerializer.Serialize(items))));
        }

        public async Task SendToFunction(Order order)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://eshopkondratfunction2.azurewebsites.net/api/DeliveryOrderProcessor");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var str = JsonSerializer.Serialize(order);
                await httpClient.PostAsync(string.Empty, new StringContent(str, Encoding.UTF8, MediaTypeNames.Application.Json));

            }
        }

    }
}
