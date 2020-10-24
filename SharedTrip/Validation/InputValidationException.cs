using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Validation
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
