using _1DV607A2.View.Component;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1DV607A2.View.Window
{
    class MainMenuView : ListView<Action>
    {
        public MainMenuView(List<ListItem<Action>> menuItems)
        {
            TopText = "The Jolly Pirate -Registry\n\n";
            BottomText = "\nPress the corresponding digit key for the option you'd like to use";
            MenuItems = menuItems;
        }

        public override void OnItemSelected(ListItem<Action> item)
        {
            item.Data.Invoke();
        }
    }
}
