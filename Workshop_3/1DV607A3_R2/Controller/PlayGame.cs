using BlackJack.Model;
using BlackJack.View;

namespace BlackJack.Controller
{
    class PlayGame : IPlayerObserver
    {
        private Game game;
        private IView view;

        public PlayGame(Game game, IView view)
        {
            this.game = game;
            this.view = view;

            game.AddObserver(this);
        }

        public bool Play()
        {
            Draw();

            int input = view.GetInput();

            if (input == 'p')
            {
                game.NewGame();
            }
            else if (input == 'h')
            {
                game.Hit();
            }
            else if (input == 's')
            {
                game.Stand();
            }

            return input != 'q';
        }

        public void OnHandChanged()
        {
            Draw();

            System.Threading.Thread.Sleep(400); // hang thread for 2/5ths of a second
        }

        private void Draw()
        {
            view.DisplayWelcomeMessage();
            view.DisplayDealerHand(game.GetDealerHand(), game.GetDealerScore());
            view.DisplayPlayerHand(game.GetPlayerHand(), game.GetPlayerScore());

            if (game.IsGameOver())
            {
                view.DisplayGameOver(game.IsDealerWinner());
            }
        }
    }
}
