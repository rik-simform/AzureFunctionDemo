using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctionDemo
{
    public static class NameAndNumber
    {
        [FunctionName("NameAndNumber")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request, This is a AZURE Demo");

            string name = req.Query["name"];
            int num1 = Convert.ToInt32( req.Query["n1"]);
            int num2 = Convert.ToInt32(req.Query["n2"]);
            int sum = num1 + num2;

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string and two numbers or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully. And the sum of two varaibles is {sum}";

            return new OkObjectResult(responseMessage);
        }
    }
}
