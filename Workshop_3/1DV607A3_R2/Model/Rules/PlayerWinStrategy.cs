namespace BlackJack.Model.Rules
{
    class PlayerWinStrategy : IWinStrategy
    {
        public bool IsDealerWinner(int bustLimit, Player dealer, Player player)
        {
            var dealerScore = dealer.CalcScore();
            var playerScore = player.CalcScore();

            if (playerScore > bustLimit) return true;
            if (dealerScore <= bustLimit)
                if (dealerScore > playerScore)
                    return true;


            return false;
        }
    }
}
