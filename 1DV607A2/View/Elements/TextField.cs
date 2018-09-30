using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.View.Elements
{
    class TextField
    {
        public TextField(string label)
        {
            Label = label;
        }

        public void Update(ConsoleKeyInfo keyInfo)
        {
            var key = keyInfo.KeyChar;
            if (key == '\b' && Value.Length > 0)
            {
                CursorPosition--;
                Value = Value.Remove(CursorPosition, 1);
            }
            else if (key == '\r')
            {

            }
            else
            {
                Value += key;
                CursorPosition++;
            }
        }

        public int GetCursorOffset()
        {
            return Label.Length + Value.Length + 2;
        }

        public override string ToString()
        {
            return $"{Label}: {Value}";
        }

        public string Label { get; set; } = "";
        public string Value { get; set; } = "";
        public int CursorPosition { get; set; } = 0;
    }
}
