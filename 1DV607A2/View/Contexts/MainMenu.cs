using _1DV607A2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.View.Contexts
{
    class MainMenu: AbstractContext
    {
        enum MenuAction
        {
            MainMenu,
            ViewData,
            CreateMember,
            CreateBoat,
            Exit
        }

        Dictionary<char, MenuAction> keyMap = new Dictionary<char, MenuAction>
        {
            ['1'] = MenuAction.ViewData,
            ['2'] = MenuAction.CreateMember,
            ['3'] = MenuAction.CreateMember,
            ['0'] = MenuAction.Exit
        };

        public MainMenu(UserInterface ui) : base(ui)
        {
        }

        public override void Draw()
        {
            Console.Clear();

            var printWidth = Console.BufferWidth;
            var buffer =
                BuildHorizontialLine('=', printWidth - 1) + "\r\n" +
                "The Jolly Pirate - Registery ()\n" +
                BuildHorizontialLine('=', printWidth - 1) + "\r\n" + 
                "1. View registery\n" +
                "2. Create new member\n" +
                "3. Register new boat\n" +
                "0. Exit program\n" +
                "\nPress a specified key to execute action\n";

            Console.Write(buffer);
        }

        public override void Update(ConsoleKeyInfo keyInfo)
        {
            MenuAction action = MenuAction.MainMenu; 
            var key = keyInfo.KeyChar;
            if (keyMap.ContainsKey(key))
                action = keyMap[key];

            switch (action)
            {
                default: break;
                case MenuAction.Exit: ui.IsExiting = true; break;
                case MenuAction.CreateMember: ui.SetActiveContext(new CreateDataMenu(typeof(MemberData), ui)); break;
                case MenuAction.CreateBoat: ui.SetActiveContext(new CreateDataMenu(typeof(BoatData), ui)); break;
                case MenuAction.ViewData: ui.SetActiveContext(new DataViewMenu(ui)); break;
            }
        }
    }
}
