using System.Collections;
using System.Collections.Generic;
using BattleCards.ViewModels.Card;

namespace BattleCards.Services
{
    public interface ICardService
    {
        void Create(AddCardInputModel input, string userId);
        ICollection<CardViewModel> GetAll();
        ICollection<CardViewModel> UserCards(string userId);
        void RemoveFromCollection(string cardId, string userId);
        void AddToCollection(string cardId, string userId);
    }
}
