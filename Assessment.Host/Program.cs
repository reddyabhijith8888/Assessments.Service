using Assessments.Application.Core.DomainEntities;
using Assessments.Application.Core.Helpers;
using Assessments.Application.Core.Interfaces;
using Assessments.Service.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Assessment.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<ITestProducer, TestProducer>()
            .AddSingleton<ITestShuffler<Assessments.Application.Core.DomainEntities.Assessment>, TestShuffler>()
            .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");


            var config = new TestConfigurationSettings();
            using (var r = new StreamReader("TestConfiguration.json"))
            {
                string json = r.ReadToEnd();
                config = JsonConvert.DeserializeObject<TestConfigurationSettings>(json) ?? throw new ArgumentNullException("No configuration found");
            }

            var testProducer = serviceProvider.GetService<ITestProducer>();
            var result = testProducer.ProduceAsync(config.listOfQuestions, config.preTestsOnTop);

            Console.WriteLine("Hello World!");
        }
    }
}
