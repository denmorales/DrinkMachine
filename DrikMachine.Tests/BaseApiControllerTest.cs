using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DrinkMachine.Tests
{
    public class BaseApiControllerTest
    {
        /// <summary>
        /// Gets or sets the test http client.
        /// </summary>
        private HttpClient TestHttpClient { get; set; }

        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        private ApiWebApplicationFactory Factory { get; set; }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [OneTimeTearDown]
#pragma warning disable 1998
        public async Task Dispose()
#pragma warning restore 1998
        {
            this.TestHttpClient?.Dispose();
            this.Factory.Dispose();
        }

        /// <summary>
        /// The setup.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [OneTimeSetUp]
#pragma warning disable 1998
        public async Task Setup()
#pragma warning restore 1998
        {
            this.Factory = new ApiWebApplicationFactory();
            this.TestHttpClient = this.Factory.CreateClient();
        }

        /// <summary>
        /// The call api method.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <typeparam name="TRequest">
        /// the request type.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// the response type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// the exception.
        /// </exception>
        protected async Task<TResponse> CallApiMethod<TRequest, TResponse>(TRequest request, string url, string method)
        {
            string bodyJson = JsonConvert.SerializeObject(request);
            HttpContent httpContent = new StringContent(bodyJson, Encoding.UTF8, "application/json");
            Func<string, HttpContent, Task<HttpResponseMessage>> httpMethod;

            switch (method.ToUpperInvariant())
            {
                case "POST":
                    httpMethod = async (path, content) =>
                        await this.TestHttpClient.PostAsync(path, httpContent);
                    break;
                case "PATCH":
                    httpMethod = async (path, content) =>
                        await this.TestHttpClient.PatchAsync(path, httpContent);
                    break;
                case "PUT":
                    httpMethod = async (path, content) =>
                        await this.TestHttpClient.PutAsync(path, httpContent);
                    break;
                case "DELETE":
                    httpMethod = async (path, content) =>
                        await this.TestHttpClient.DeleteAsync(path);
                    break;
                case "GET":
                    httpMethod = async (path, content) =>
                        await this.TestHttpClient.GetAsync(path);
                    break;
                default:
                    throw new Exception($"unsupported http method {method}");
            }

            Stopwatch stopwatch = Stopwatch.StartNew();

            var responseMessage = await httpMethod(url, new StringContent(bodyJson, Encoding.UTF8, "application/json"));

            stopwatch.Stop();
            await TestContext.Out.WriteLineAsync($"Request {url} {method}ed in {stopwatch.Elapsed}");

            Assert.NotNull(responseMessage);
            responseMessage.EnsureSuccessStatusCode();
            string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            TResponse response = JsonConvert.DeserializeObject<TResponse>(jsonResponse);
            Assert.NotNull(response);
            return response;
        }

        /// <summary>
        /// The get api method.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <typeparam name="TResponse">
        /// the response type
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected async Task<TResponse> GetApiMethod<TResponse>(string url)
        {
            return await this.CallApiMethod<object, TResponse>(null, url, "GET");
        }

    }
}