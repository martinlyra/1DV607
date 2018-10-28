namespace BlackJack.Model.Rules
{
    interface IHitStrategy
    {
        bool DoHit(Model.Player a_dealer);
    }
}
