using Assessments.Application.Core.DomainEntities;
using Assessments.Application.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Assessments.Application.Core.Helpers
{
    public class TestShuffler : ITestShuffler<Assessment>
    {
        private ILogger<TestShuffler> _logger;
        public TestShuffler(ILogger<TestShuffler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(TestShuffler));
        }

        public IReadOnlyList<Assessment> GenerateShuffledList(IReadOnlyList<Assessment> listOfItems, int noOfPretestQuestionsOnTop)
        {
            if(listOfItems == null || !listOfItems.Any())
            {
                throw new ArgumentNullException(nameof(listOfItems));
            }
            var shuffledList = new Assessment[listOfItems.Count].ToList();
            var excludedList = new List<int>();

            foreach (var item in listOfItems)
            {
                var randomNum = GetRandomNumber(listOfItems.Count, excludedList);

                excludedList.Add(randomNum);
                shuffledList[randomNum] = item;
            }

            if(noOfPretestQuestionsOnTop == 0)
            {
                return shuffledList;
            }

            var pretestQuestions = shuffledList.Where(x => x.AssessmentType == AssessmentTypeEnum.Pretest);
            if(pretestQuestions.Count() > noOfPretestQuestionsOnTop)
            {
                pretestQuestions.Take(noOfPretestQuestionsOnTop);
            }
            var restOfQuestions = shuffledList.Except(pretestQuestions);
            shuffledList = new List<Assessment>();
            shuffledList.AddRange(pretestQuestions);
            shuffledList.AddRange(restOfQuestions);
            return shuffledList;
        }

        private static int GetRandomNumber(int length, List<int> excludedNumbers)
        {
            Random randomGenerator = new Random();
            int currentNumber = randomGenerator.Next(length);

            if(!excludedNumbers.Any() || excludedNumbers.Count == length)
            {
                return currentNumber;
            }
            while (excludedNumbers.Any(x => x.Equals(currentNumber)))
            {
                currentNumber = randomGenerator.Next(length);
            }
            return currentNumber;
        }
    }
}
