using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Validation
{
    class ValidationResult:IValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<string>();
        }
        public bool IsValid { get; set; }
        public IList<string> Errors { get; set; }

        public override string ToString()
        {
            if (Errors.Count == 0)
            {
                return "Data is valid. No errors";
            }
            else
            {
                return string.Join('\n', Errors);
            }
        }
    }
}
