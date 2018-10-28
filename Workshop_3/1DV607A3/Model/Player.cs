using System.Collections.Generic;
using System.Linq;

namespace BlackJack.Model
{
    class Player : Observable<IPlayerObserver>
    {
        private List<Card> hand = new List<Card>();

        public void DealCard(Card card, bool showCard = true)
        {
            card.Show(showCard);
            hand.Add(card);

            Notify((o) => o.OnHandChanged());
        }

        public IEnumerable<Card> GetHand()
        {
            return hand.Cast<Card>();
        }

        public void ClearHand()
        {
            hand.Clear();
        }

        public void ShowHand()
        {
            foreach (Card c in GetHand())
            {
                c.Show(true);
            }
        }

        public int CalcScore()
        {
            int[] cardScores = new int[(int)Card.Value.Count]
                {2, 3, 4, 5, 6, 7, 8, 9, 10, 10 ,10 ,10, 11};
            int score = 0;

            foreach(Card c in GetHand()) {
                if (c.GetValue() != Card.Value.Hidden)
                {
                    score += cardScores[(int)c.GetValue()];
                }
            }

            if (score > 21)
            {
                foreach (Card c in GetHand())
                {
                    if (c.GetValue() == Card.Value.Ace && score > 21)
                    {
                        score -= 10;
                    }
                }
            }

            return score;
        }
    }
}
