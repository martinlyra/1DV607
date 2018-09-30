using _1DV607A2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2
{
    class Program
    {
        static void Main(string[] args)
        {
            var ui = new UserInterface();

            while (!ui.IsExiting)
            {
                ui.RunLoop();
            }
        }
    }
}
