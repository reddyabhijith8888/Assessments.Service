using Assessments.Application.Core.DomainEntities;
using Assessments.Application.Core.Helpers;
using Assessments.Application.Core.Interfaces;
using Assessments.Service.Service;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CandidateAssement.UnitTests
{
    public class AssessmentProducerTests
    {
        private ITestProducer _producer => CreateMockTestProducer();
        private ITestProducer CreateMockTestProducer()
        {
            ITestShuffler<Assessment> mockShuffler = new TestShuffler(Mock.Of<ILogger<TestShuffler>>());
            return new TestProducer(mockShuffler, Mock.Of<ILogger<TestProducer>>());
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void WhenInputWithNoOfQuestionsOnTopIsPassedToTestProducer_ThenResponseShouldContainTopQuestionsAsExpectedInInput(int noOfQuestions)
        {
            var data = TestData.TestData.GetRecordsByQuantity(10);
            var result = _producer.ProduceAsync(data, noOfQuestions).ToList();

            for (int i = 0; i < noOfQuestions; i++)
            {
                result[i].AssessmentType.Should().Be(AssessmentTypeEnum.Pretest);
            }
        }

        [Theory]
        [InlineData(5)]
        [InlineData(7)]
        public void WhenRequestedQuestionsOnTopAreMoreThanInTheRequest_ThenTestProducerValidatesAndReturnError(int noOfQuestions)
        {
            var data = TestData.TestData.GetRecordsByQuantity(10);
            Action result = () => _producer.ProduceAsync(data, noOfQuestions);
            // Never check the error messages in tests since these might be changing depending on the business requirement.
            //the reason of checking over here is to prove this is giving the write error/exception
            result.Should().Throw<ArgumentException>().WithMessage("questions requested on top are more than whats there in the list");

           
        }

        [Theory]
        [InlineData(2)]
        public void WhenAnEmptyInputIsPassedToShuffler_ThenResponseShouldThrowException(int noOfQuestions)
        {
            var data = TestData.TestData.GetRecordsByQuantity(10);
            Action result = () => _producer.ProduceAsync(null, noOfQuestions);
            result.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(2)]
        public void WhenMoreThanTenQuestionsInInput_ThenResponseShouldThrowException(int noOfQuestions)
        {
            var data = TestData.TestData.GetRecordsByQuantity(11);
            Action result = () => _producer.ProduceAsync(data, noOfQuestions);
            // Never check the error messages in tests since these might be changing depending on the business requirement.
            //the reason of checking over here is to prove this is giving the write error/exception
            result.Should().Throw<ArgumentException>().WithMessage("should be equal to 10 questions only");
        }

        [Theory]
        [InlineData(2)]
        public void WhenLessThanTenQuestionsInInput_ThenResponseShouldThrowException(int noOfQuestions)
        {
            var data = TestData.TestData.GetRecordsByQuantity(4);
            Action result = () => _producer.ProduceAsync(data, noOfQuestions);
            // Never check the error messages in tests since these might be changing depending on the business requirement.
            //the reason of checking over here is to prove this is giving the write error/exception
            result.Should().Throw<ArgumentException>().WithMessage("should be equal to 10 questions only");
        }
    }
}
