using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.View.Elements
{
    class TextInputField: InputField
    {
        public TextInputField(string label)
        {
            Label = label;
        }

        public TextInputField(string label, string value)
        {
            Label = label;
            Value = value;
        }

        public void Update(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.Enter:
                    {
                        break;
                    }
                case ConsoleKey.Backspace:
                    {
                        if (Value.Length > 0 && CursorPosition > 0)
                        {
                            CursorPosition--;
                            Value = Value.Remove(CursorPosition, 1);
                        }
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        if (CursorPosition > 0)
                            CursorPosition--;
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        if (CursorPosition < Value.Length)
                            CursorPosition++;
                        break;
                    }
                default:
                    {
                        Value = Value.Insert(CursorPosition, keyInfo.KeyChar.ToString());
                        CursorPosition++;
                        break;
                    }
            }
        }

        public int GetCursorOffset()
        {
            return Label.Length + CursorPosition + 2;
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
