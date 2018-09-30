using _1DV607A2.Controller;
using _1DV607A2.View.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.View
{
    class UserInterface
    {
        AbstractContext activeContext;

        public UserInterface()
        {
            DataController = new DataController();
            SetActiveContext(new MainMenu(this));
        }

        public void RunLoop()
        {
            activeContext.Draw();
            activeContext.Update(Console.ReadKey());
        }

        public void SetActiveContext(AbstractContext context)
        {
            activeContext = context;
        }

        public bool IsExiting { get; set; } = false;

        internal DataController DataController { get; private set; }
    }
}
