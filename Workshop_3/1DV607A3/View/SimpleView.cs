using System;
using System.Collections.Generic;

namespace BlackJack.View
{
    class SimpleView : IView
    {

        public void DisplayWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("Hello Black Jack World");
            Console.WriteLine("Type 'p' to Play, 'h' to Hit, 's' to Stand or 'q' to Quit\n");
        }

        public int GetInput()
        {
            return Console.In.Read();
        }

        public void DisplayCard(Model.Card a_card)
        {
            Console.WriteLine("{0} of {1}", a_card.GetValue(), a_card.GetColor());
        }

        public void DisplayPlayerHand(IEnumerable<Model.Card> a_hand, int a_score)
        {
            DisplayHand("Player", a_hand, a_score);
        }

        public void DisplayDealerHand(IEnumerable<Model.Card> a_hand, int a_score)
        {
            DisplayHand("Dealer", a_hand, a_score);
        }

        private void DisplayHand(String a_name, IEnumerable<Model.Card> a_hand, int a_score)
        {
            Console.WriteLine("{0} Has: ", a_name);
            foreach (Model.Card c in a_hand)
            {
                DisplayCard(c);
            }
            Console.WriteLine("Score: {0}", a_score);
            Console.WriteLine("");
        }

        public void DisplayGameOver(bool a_dealerIsWinner)
        {
            Console.Write("Game over: ");
            if (a_dealerIsWinner)
            {
                Console.WriteLine("Dealer Won!");
            }
            else
            {
                Console.WriteLine("You Won!");
            }
            
        }
    }
}
