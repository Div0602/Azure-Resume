using System.Net.Http;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class GetResumeCounter
    {
        [FunctionName("GetResumeCounter")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "AzureResume",
                collectionName: "Counter",
                ConnectionStringSetting = "AzureResumeConnectionString",
                Id = "1",           // the document id in CosmosDB
                PartitionKey = "1"  // if your container has a partition key
            )] Counter counter,
            [CosmosDB(
                databaseName: "AzureResume",
                collectionName: "Counter",
                ConnectionStringSetting = "AzureResumeConnectionString",
                Id = "1",           // the document id in CosmosDB
                PartitionKey = "1" 
            )] out Counter updatedCounter,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            if (counter == null)
            {
                // If document doesn't exist, create a new one
                counter = new Counter
                {
                    Id = "1",
                    Count = 0
                };
            }

            counter.Count += 1;
            updatedCounter = counter;

            var jsonToReturn = JsonConvert.SerializeObject(counter);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }
}
