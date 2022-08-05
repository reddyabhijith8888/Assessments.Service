namespace Assessments.Application.Core.DomainEntities
{
    public class TestConfigurationSettings
    {
        public int preTestsOnTop { get; set; }
        public List<Assessment> listOfQuestions { get; set; } = new List<Assessment>();

    }
}
