using Assessments.Application.Core.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateAssement.UnitTests.TestData
{
    internal static class TestData
    {
        internal static IReadOnlyList<Assessment> GetOriginalTestData()
        {
            return new List<Assessment>()
            {
                new Assessment{AssessmentType = AssessmentTypeEnum.Pretest, AssessmentTypeId = "1"},
                new Assessment{AssessmentType = AssessmentTypeEnum.Operational, AssessmentTypeId = "2"},
                new Assessment{AssessmentType = AssessmentTypeEnum.Operational, AssessmentTypeId = "3"},
                new Assessment{AssessmentType = AssessmentTypeEnum.Pretest, AssessmentTypeId = "4"},
                new Assessment{AssessmentType = AssessmentTypeEnum.Operational, AssessmentTypeId = "5"},
                new Assessment{AssessmentType = AssessmentTypeEnum.Operational, AssessmentTypeId = "6"},
                new Assessment{AssessmentType = AssessmentTypeEnum.Pretest, AssessmentTypeId = "7"},
                new Assessment{AssessmentType = AssessmentTypeEnum.Operational, AssessmentTypeId = "8"},
                new Assessment{AssessmentType = AssessmentTypeEnum.Operational, AssessmentTypeId = "9"},
                new Assessment{AssessmentType = AssessmentTypeEnum.Pretest, AssessmentTypeId = "10"},
                new Assessment{AssessmentType = AssessmentTypeEnum.Operational, AssessmentTypeId = "11"},
                new Assessment{AssessmentType = AssessmentTypeEnum.Pretest, AssessmentTypeId = "12"},
            };
        }

        internal static IReadOnlyList<Assessment> GetRecordsByQuantity(int noOfRecords)
        {
            return GetOriginalTestData().Take(noOfRecords).ToList();
        }
    }
}
