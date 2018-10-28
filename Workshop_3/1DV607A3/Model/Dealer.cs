using BlackJack.Model.Rules;

namespace BlackJack.Model
{
    class Dealer : Player
    {
        private Deck deck = null;
        private const int maxScore = 21;

        private INewGameStrategy newGameRule;
        private IHitStrategy hitRule;
        private IWinStrategy winRule;


        public Dealer(RulesFactory rulesFactory)
        {
            newGameRule = rulesFactory.GetNewGameRule();
            hitRule = rulesFactory.GetHitRule();
            winRule = rulesFactory.GetWinRule();
        }

        public bool NewGame(Player player)
        {
            if (deck == null || IsGameOver())
            {
                deck = new Deck();
                ClearHand();
                player.ClearHand();
                return newGameRule.NewGame(deck, this, player);   
            }
            return false;
        }

        public bool Hit(Player player)
        {
            if (deck != null && player.CalcScore() < maxScore && !IsGameOver())
            {
                player.DealCard(deck.GetCard());

                return true;
            }
            return false;
        }

        public bool Stand()
        {
            if (deck != null)
            {
                ShowHand();
                while (hitRule.DoHit(this))
                {
                    DealCard(deck.GetCard());
                }
                return true;
            }
            return false;
        }

        public bool IsDealerWinner(Player player)
        {
            return winRule.IsDealerWinner(maxScore, this, player);
        }

        public bool IsGameOver()
        {
            if (deck != null && /*CalcScore() >= g_hitLimit*/ hitRule.DoHit(this) != true)
            {
                return true;
            }
            return false;
        }
    }
}
