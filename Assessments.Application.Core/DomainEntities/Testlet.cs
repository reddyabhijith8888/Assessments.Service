namespace Assessments.Application.Core.DomainEntities
{
    public class Testlet
    {
        public string TestletId;
        private List<Assessment> Items;
        public Testlet(string testletId, List<Assessment> items)
        {
            TestletId = testletId;
            Items = items;
        }
    }
}
