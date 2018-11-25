using System.Linq;

namespace BlackJack.Model.Rules
{
    class SoftSeventeenHitStrategy : IHitStrategy
    {
        const int scoreLimit = 17;

        public bool DoHit(Player dealer)
        {
            var hand = dealer.GetHand();
            var score = dealer.CalcScore();
            var aceCount = hand.Where(
                (card) => card.GetValue() == Card.Value.Ace)
                .Count();

            if (aceCount > 0)
            {
                if (score == scoreLimit)
                    return true;
            }
            else
                if (score < scoreLimit) return true;

            return false;
        }
    }
}
