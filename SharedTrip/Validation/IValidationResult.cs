using System.Collections.Generic;

namespace SharedTrip.Validation
{
    public interface IValidationResult
    {
        public bool IsValid { get; set; }
        public IList<string> Errors { get; set; }

    }
}
