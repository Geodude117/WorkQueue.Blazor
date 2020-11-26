using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models.QuestionModels
{
    public interface IQuestion
    {
        string Text { get; set; }
        int Order { get; set; }
        bool HasValidation { get; set; }

    }
}
