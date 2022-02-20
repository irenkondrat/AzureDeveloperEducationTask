using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace eShopKondratFunction
{
    public static class DeliveryOrderProcessor
    {
        [FunctionName("DeliveryOrderProcessor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] Order req,
            [CosmosDB(databaseName: "orderdetail", collectionName: "orederContainer",ConnectionStringSetting = "myCosmosDb",
                PartitionKey = "{Id}")] IAsyncCollector<Order> order,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            await order.AddAsync(req);

            return new OkObjectResult("This HTTP triggered function executed successfully.");
        }
    }
}
