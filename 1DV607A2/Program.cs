using _1DV607A2.View;

namespace _1DV607A2
{
    class Program
    {
        static void Main(string[] args)
        {
            var ui = new SimpleUserInterface();

            while (!ui.IsExiting)
            {
                ui.RunLoop();
            }
        }
    }
}
