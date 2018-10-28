namespace BlackJack.Model.Rules
{
    interface INewGameStrategy
    {
        bool NewGame(Deck deck, Dealer dealer, Player player);
    }
}
