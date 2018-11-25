using System;
using System.Collections.Generic;
using System.Text;
using _1DV607A2.Controller;
using _1DV607A2.Model;
using _1DV607A2.View.Component;
using _1DV607A2.View.Window;

namespace _1DV607A2.View
{
    class WindowedInterface : IUserInterface
    {
        IWindow CurrentWindow { get; set; }
        MainMenuView UserMenuView { get; set; }
        MainMenuView GuestMenuView { get; set; }
        BoatEditorView BoatEditor { get; set; }
        MemberEditorView MemberEditor { get; set; }

        ProgramController ProgramController { get; set; }

        WindowedInterface(ProgramController controller)
        {
            ProgramController = controller;

            UserMenuView = new MainMenuView(new List<ListItem<Action>>(){
                new ListItem<Action>(ConsoleKey.D1, "View members (Compact)",
                () => controller.ShowMembers(ListDisplayMode.Compact)),
                new ListItem<Action>(ConsoleKey.D2, "View members (Verbose)",
                () => controller.ShowMembers(ListDisplayMode.Verbose)),
                new ListItem<Action>(ConsoleKey.D3, "View boats",
                () => controller.ShowBoats()),
                new ListItem<Action>(ConsoleKey.D4, "Create member",
                () => controller.CreateMember())
                new ListItem<Action>(ConsoleKey.D5, "Create boat",
                () => controller.CreateBoat())
            });
        }

        private void Show(IWindow window)
        {
            CurrentWindow = window;   
        }

        public void Draw()
        {
            Console.Clear();
            CurrentWindow.Draw();
        }

        public void ShowBoat(BoatData boat)
        {
            throw new NotImplementedException();
        }

        public void ShowBoatEditor(DataMode dataMode, BoatData boat)
        {
            throw new NotImplementedException();
        }

        public void ShowBoats(ListDisplayMode displayMode, IEnumerable<BoatData> boats)
        {
            throw new NotImplementedException();
        }

        public void ShowMainMenuGuest()
        {
            throw new NotImplementedException();
        }

        public void ShowMember(MemberData member)
        {
            throw new NotImplementedException();
        }

        public void ShowMemberEditor(DataMode dataMode, MemberData member)
        {
            throw new NotImplementedException();
        }

        public void ShowMembers(ListDisplayMode displayMode, IEnumerable<MemberData> members)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void ShowMainMenuUser()
        {
            Show(UserMenuView);    
        }
    }
}
