using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Models.IndexModel
{
    public class ChoiceModel
    {
        public int ChoiceId { get; set; }

        public string IsChecked { get; set; }
    }

    public class AnswerModel
    {
        public int TestId { get; set; }

        public int QuestionId { get; set; }

        public Guid Token { get; set; }

        public List<ChoiceModel> UserChoices { get; set; }

        public string Answer { get; set; }

        public string Direction { get; set; }

        public List<ChoiceModel> UserSelectedId
        {
            get
            {
                return UserChoices == null ? new List<ChoiceModel>() :
                    UserChoices.Where(x => x.IsChecked == "on" || "true".Equals(x.IsChecked, StringComparison.InvariantCultureIgnoreCase))
                    .Select(x=>new ChoiceModel {
                        ChoiceId = x.ChoiceId,
                        IsChecked = x.IsChecked
                    }).ToList();
            }
        }
    }
}
