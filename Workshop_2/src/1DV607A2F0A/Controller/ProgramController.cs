using _1DV607A2.Model;
using _1DV607A2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV607A2.Controller
{
    class ProgramController
    {
        ProgramController(DataController dataController, IUserInterface userInterface)
        {

        }

        public void RunLoop()
        {
            
        } 

        public void View(string objectID)
        {

        }

        public void Edit(string objectID)
        {

        }

        public void ShowMembers(ListDisplayMode displayMode)
        {

        }

        public void ShowBoats()
        {

        }

        public bool IsExiting { get; set; } = false;

        internal DataController DataController { get; private set; }
    }
}
