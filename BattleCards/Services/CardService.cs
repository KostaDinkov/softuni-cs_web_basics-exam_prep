using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BattleCards.Data;
using BattleCards.Models;
using BattleCards.Validation;
using BattleCards.ViewModels.Card;
using Microsoft.EntityFrameworkCore;

namespace BattleCards.Services
{
    class CardService : ICardService
    {
        private ApplicationDbContext db;

        public CardService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(AddCardInputModel input, string userId)
        {
            var validationResult = ValidateAddCardInput(input);
            if (!validationResult.IsValid)
                throw new InputValidationException(validationResult.Errors);

            var card = new Card
            {
                Name = input.Name,
                Attack = int.Parse(input.Attack),
                Health = int.Parse(input.Health),
                Keyword = input.Keyword,
                Description = input.Description,
                ImageUrl = input.Image
            };

            db.Cards.Add(card);
            db.UserCards.Add(new UserCard { UserId = userId, CardId = card.Id });
            db.SaveChanges();
        }

        public ICollection<CardViewModel> GetAll()
        {
            return db.Cards.Select(x => new CardViewModel
            {
                Attack = x.Attack,
                Health = x.Health,
                ImageUrl = x.ImageUrl,
                Description = x.Description,
                Id = x.Id,
                Keyword = x.Keyword,
            }).ToList();
        }

        public ICollection<CardViewModel> UserCards(string userId)
        {
            return db.UserCards
                .Where(c => c.UserId == userId)
                .Select(x => new CardViewModel
                {
                    Name = x.Card.Name,
                    Attack = x.Card.Attack,
                    Health = x.Card.Health,
                    ImageUrl = x.Card.ImageUrl,
                    Description = x.Card.Description,
                    Id = x.Card.Id,
                    Keyword = x.Card.Keyword,
                }).ToList();
        }

        public void RemoveFromCollection(string cardId, string userId)
        {
            var userCard = db.UserCards.Find(userId, cardId);
            db.UserCards.Remove(userCard);
            db.SaveChanges();
        }

        public void AddToCollection(string cardId, string userId)
        {
            if (!db.UserCards.Any(x => x.CardId == cardId && x.UserId == userId))
            {
                db.UserCards.Add(new UserCard {CardId = cardId, UserId = userId});
                db.SaveChanges();
            }
        }

        private IValidationResult ValidateAddCardInput(AddCardInputModel input)
        {

            var result = new ValidationResult { IsValid = true };

            if (input.Name.Length < 5 || input.Name.Length > 20)
                SetError("Name must be between 5 and 20 characters long.");

            if (string.IsNullOrWhiteSpace(input.Image))
                SetError("Image Url cannot be empty.");

            if (string.IsNullOrWhiteSpace(input.Keyword))
                SetError("Keyword cannot be empty.");

            if (!int.TryParse(input.Attack, out _) || int.Parse(input.Attack) < 0)
                SetError("Attack must be a nonnegative integer");

            if (!int.TryParse(input.Health, out _) || int.Parse(input.Health) < 0)
                SetError("Health must be a nonnegative integer");

            if (input.Description.Length > 200)
                SetError("Description length cannot be greater than 200 characters.");


            return result;
            void SetError(string error)
            {
                result.Errors.Add(error);
                result.IsValid = false;
            }
        }
    }
}