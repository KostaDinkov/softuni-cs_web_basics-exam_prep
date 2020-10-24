using System;
using System.Collections.Generic;

namespace BattleCards.Validation
{
    class InputValidationException: Exception
    {
        public IList<string> Errors { get; set; }
        public InputValidationException(IList<string> errors)
        {
            Errors = errors;
        }

        public string ToHtmlString()
        {
            return string.Join("<br>", Errors);
        }
    }
}
