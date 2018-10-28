using _1DV607A2.View;

namespace _1DV607A2
{
    class Program
    {
        static SimpleUserInterface userInterface;

        static void Main(string[] args)
        {
            userInterface = new SimpleUserInterface();

            while (!userInterface.IsExiting)
            {
                userInterface.RunLoop();
            }
        }
    }
}
