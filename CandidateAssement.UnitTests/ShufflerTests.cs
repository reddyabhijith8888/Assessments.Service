using Assessments.Application.Core.DomainEntities;
using Assessments.Application.Core.Helpers;
using Assessments.Application.Core.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace CandidateAssement.UnitTests
{
    public class ShufflerTests
    {
        private ITestShuffler<Assessment> CreateMockTestShuffler() => new TestShuffler(Mock.Of<ILogger<TestShuffler>>());

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void WhenInputWithNoOfQuestionsOnTopIsPassedToShuffler_ThenResponseShouldContainTopQuestionsAsExpectedInInput(int noOfQuestions)
        {
            var shuffler = CreateMockTestShuffler();
            var data = TestData.TestData.GetOriginalTestData();
            var result = shuffler.GenerateShuffledList(data, noOfQuestions);

            for (int i = 0; i < noOfQuestions; i++)
            {
                result[i].AssessmentType.Should().Be(AssessmentTypeEnum.Pretest);
            }
        }

        [Theory]
        [InlineData(5)]
        public void WhenRequestedQuestionsOnTopAreMoreThanInTheRequest_ThenShufflerReturnsExistingOnTop(int noOfQuestions)
        {
            var shuffler = CreateMockTestShuffler();
            var data = TestData.TestData.GetOriginalTestData();
            var totalPreTestInData = data.Count(x => x.AssessmentType == AssessmentTypeEnum.Pretest);
            var result = shuffler.GenerateShuffledList(data, noOfQuestions);

            for (int i = 0; i < totalPreTestInData; i++)
            {
                result[i].AssessmentType.Should().Be(AssessmentTypeEnum.Pretest);
            }
        }


        // Added multiple same inline data just to test the randomness of the functionality and it should not break the code, we can also implement loop
        [Theory]
        [InlineData(0)]
        [InlineData(0)]
        [InlineData(0)]
        [InlineData(0)]
        public void WhenRequestedQuestionsAreZero_ThenShufflerReturnsRandomList(int noOfQuestions)
        {
            var shuffler = CreateMockTestShuffler();
            var data = TestData.TestData.GetOriginalTestData();
            var totalPreTestInData = data.Count(x => x.AssessmentType == AssessmentTypeEnum.Pretest);
            var result = shuffler.GenerateShuffledList(data, noOfQuestions);
           result.FirstOrDefault()!.AssessmentType.Should().BeOneOf(AssessmentTypeEnum.Pretest, AssessmentTypeEnum.Operational);
        }

        [Theory]
        [InlineData(2)]
        public void WhenAnEmptyInputIsPassedToShuffler_ThenResponseShouldThrowException(int noOfQuestions)
        {
            var shuffler = CreateMockTestShuffler();
            Action result = () => shuffler.GenerateShuffledList(null, noOfQuestions);
            result.Should().Throw<ArgumentNullException>();
        }
    }
}