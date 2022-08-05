using Assessments.Application.Core.DomainEntities;
using Assessments.Application.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Assessments.Service.Service
{
    public class TestProducer : ITestProducer
    {
        private readonly ITestShuffler<Assessment> _testShuffler;
        private readonly ILogger<TestProducer> _logger;
        public TestProducer(ITestShuffler<Assessment> testShuffler, ILogger<TestProducer> logger)
        {
            _testShuffler = testShuffler ?? throw new ArgumentNullException(nameof(TestProducer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IReadOnlyCollection<Assessment> ProduceAsync(IReadOnlyList<Assessment> listOfItems, int noOfPretestQuestionsOnTop)
        {
            try
            {
                if (listOfItems == null || listOfItems.Count == 0) throw new ArgumentNullException(nameof(listOfItems));

                var ispretestQuestionsAvailable = listOfItems.Count(x => x.AssessmentType == AssessmentTypeEnum.Pretest) < noOfPretestQuestionsOnTop;

                if(ispretestQuestionsAvailable)
                {
                    throw new ArgumentException("questions requested on top are more than whats there in the list");
                }
                return _testShuffler.GenerateShuffledList(listOfItems, noOfPretestQuestionsOnTop);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
