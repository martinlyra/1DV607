using System;
using System.Collections.Generic;

namespace BlackJack.View
{
    class SwedishView : IView 
    {
        public void DisplayWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("Hej Black Jack Världen");
            Console.WriteLine("----------------------");
            Console.WriteLine("Skriv 'p' för att Spela, 'h' för nytt kort, 's' för att stanna 'q' för att avsluta\n");
        }
        public int GetInput()
        {
            return Console.In.Read();
        }
        public void DisplayCard(Model.Card a_card)
        {
            if (a_card.GetColor() == Model.Card.Color.Hidden)
            {
                Console.WriteLine("Dolt Kort");
            }
            else
            {
                String[] colors = new String[(int)Model.Card.Color.Count]
                    { "Hjärter", "Spader", "Ruter", "Klöver" };
                String[] values = new String[(int)Model.Card.Value.Count] 
                    { "två", "tre", "fyra", "fem", "sex", "sju", "åtta", "nio", "tio", "knekt", "dam", "kung", "ess" };
                Console.WriteLine("{0} {1}", colors[(int)a_card.GetColor()], values[(int)a_card.GetValue()]);
            }
        }
        public void DisplayPlayerHand(IEnumerable<Model.Card> a_hand, int a_score)
        {
            DisplayHand("Spelare", a_hand, a_score);
        }
        public void DisplayDealerHand(IEnumerable<Model.Card> a_hand, int a_score)
        {
            DisplayHand("Croupier", a_hand, a_score);
        }
        public void DisplayGameOver(bool a_dealerIsWinner)
        {
            Console.Write("Slut: ");
            if (a_dealerIsWinner)
            {
                Console.WriteLine("Croupiern Vann!");
            }
            else
            {
                Console.WriteLine("Du vann!");
            }
        }

        private void DisplayHand(String a_name, IEnumerable<Model.Card> a_hand, int a_score)
        {
            Console.WriteLine("{0} Har: ", a_name);
            foreach (Model.Card c in a_hand)
            {
                DisplayCard(c);
            }
            Console.WriteLine("Poäng: {0}", a_score);
            Console.WriteLine("");
        }
    }
}
