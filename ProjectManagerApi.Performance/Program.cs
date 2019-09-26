using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Net;
using System.Net.Http;

namespace ProjectManagerApi.Performance
{
    public class ProjectManagerPerformanceTest
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl = "http://localhost:49708/api/User/GetAllUsers";
        public ProjectManagerPerformanceTest()
        {

        }

        [Benchmark]
        public HttpStatusCode InvokeController() => httpClient.GetAsync(apiUrl).GetAwaiter().GetResult().StatusCode;
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ProjectManagerPerformanceTest>();
            Console.ReadLine();
        }
    }
}

