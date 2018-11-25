namespace BlackJack.Model.Rules
{
    interface IWinStrategy
    {
        bool IsDealerWinner(int bustLimit, Player dealer, Player player);
    }
}
