using BlackJack.Controller;
using BlackJack.Model;
using BlackJack.View;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            IView view = new SimpleView(); // new view.SwedishView();
            PlayGame controller = new PlayGame(game, view);

            while (controller.Play());
        }
    }
}
