using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Models.Question
{
    public class Test
    {
        public int TestID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int DurationInMinute { get; set; }

        public IList<Registration> Registrations { get; set; }

        public IList<TestQuestion> TestQuestions { get; set; }



    }
}
