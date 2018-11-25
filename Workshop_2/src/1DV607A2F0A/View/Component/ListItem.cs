using System;
using System.Collections.Generic;
using System.Text;

namespace _1DV607A2.View.Component
{
    class ListItem<DataType>
    {
        public ListItem(ConsoleKey key, string text, DataType data)
        {
            Key = key;
            Text = text;
            Data = data;
        }

        public ConsoleKey Key { get; private set; }
        public string Text { get; private set; }
        public DataType Data { get; private set; }
    }
}
