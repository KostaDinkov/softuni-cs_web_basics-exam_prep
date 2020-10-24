using System.Collections.Generic;

namespace BattleCards.Validation
{
    public interface IValidationResult
    {
        public bool IsValid { get; set; }
        public IList<string> Errors { get; set; }

    }
}
