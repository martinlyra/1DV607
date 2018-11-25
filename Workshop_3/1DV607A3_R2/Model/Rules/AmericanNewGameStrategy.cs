namespace BlackJack.Model.Rules
{
    class AmericanNewGameStrategy : INewGameStrategy
    {
        public bool NewGame(Deck deck, Dealer dealer, Player player)
        {
            player.DealCard(deck.GetCard());

            dealer.DealCard(deck.GetCard());

            player.DealCard(deck.GetCard());

            dealer.DealCard(deck.GetCard(), showCard: false);

            return true;
        }
    }
}
