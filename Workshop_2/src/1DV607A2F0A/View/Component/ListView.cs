using _1DV607A2.View.Window;
using System;
using System.Collections.Generic;

namespace _1DV607A2.View.Component
{
    abstract class ListView<T> : IWindow
    {
        public string TopText { get; set; }
        public string BottomText { get; set; }
        public List<ListItem<T>> MenuItems { get; set; }

        public void Draw()
        {
            Console.WriteLine(TopText);
            foreach (var item in MenuItems)
            {
                Console.WriteLine($"{item.Key}. {item.Text}");
            }
            Console.WriteLine(BottomText);
        }

        public void Update()
        {
            var input = Console.ReadKey();
            foreach (var item in MenuItems)
                if (item.Key == input.Key)
                    OnItemSelected(item);
        }

        public abstract void OnItemSelected(ListItem<T> item);
    }
}
