namespace BlackJack.Model
{
    interface IPlayerObserver : IObserver
    {
        void OnHandChanged();
    }
}
