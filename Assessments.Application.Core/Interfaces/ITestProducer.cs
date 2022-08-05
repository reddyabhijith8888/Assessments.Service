using Assessments.Application.Core.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessments.Application.Core.Interfaces
{
    public interface ITestProducer
    {
        IReadOnlyCollection<Assessment> ProduceAsync(IReadOnlyList<Assessment> listOfItems, int noOfPreTestQuestionsOnTop);
    }
}
