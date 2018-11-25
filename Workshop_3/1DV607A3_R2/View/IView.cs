using System.Collections.Generic;

namespace BlackJack.View
{
    interface IView
    {
        void DisplayWelcomeMessage();
        int GetInput();
        void DisplayCard(Model.Card a_card);
        void DisplayPlayerHand(IEnumerable<Model.Card> a_hand, int a_score);
        void DisplayDealerHand(IEnumerable<Model.Card> a_hand, int a_score);
        void DisplayGameOver(bool a_dealerIsWinner);
    }
}
