namespace Assessments.Application.Core.Interfaces
{
    public interface ITestShuffler<T>
    {
        IReadOnlyList<T> GenerateShuffledList(IReadOnlyList<T> listOfItems, int noOfPretestQuestions);
    }
}
