using _1DV607A2.Model;
using _1DV607A2.View.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.View.Contexts
{
    class CreateDataMenu : AbstractContext
    {
        Type targetType;

        int selection = 0;
        List<TextField> fields;
        string errorMessage;

        public CreateDataMenu(Type targetType, UserInterface ui) : base(ui)
        {
            this.targetType = targetType;

            fields = new List<TextField>();
            if (targetType == typeof(MemberData))
            {
                fields.Add(new TextField("Name"));
                fields.Add(new TextField("Personal number"));
            }
            else if (targetType == typeof(BoatData))
            {
                fields.Add(new TextField("Type"));
                fields.Add(new TextField("Length"));
                fields.Add(new TextField("Owner's ID"));
            }
        }

        public override void Draw()
        {
            Console.Clear();

            var item = targetType == typeof(MemberData) ? "member" : "data";
            var buffer = $"Create a new {item}; fill out following fields:\n\n";
            for (int i = 0; i < fields.Count; i++)
                buffer += $"{(i == selection ? '>' : ' ')} {fields[i].ToString()}\n";

            buffer += "\n\nUse UP- and DOWN-ARROWS to change field to edit, press ENTER to send the form\n";
            buffer += $"{errorMessage}";

            Console.Write(buffer);

            Console.SetCursorPosition(2 + fields[selection].GetCursorOffset(), 2 + selection);
        }

        public override void Update(ConsoleKeyInfo keyInfo)
        {
            var key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.UpArrow: selection = selection - 1 % fields.Count; break;
                case ConsoleKey.DownArrow: selection = selection + 1 % fields.Count; break;
                default: fields[selection].Update(keyInfo); break;
                case ConsoleKey.Enter: TrySendRequest(); break;
            }
        }

        void TrySendRequest()
        {
            errorMessage = "";
            if (targetType == typeof(MemberData))
            {
                var numberValue = fields[1].Value;
                var isAllDigits = numberValue.Sum(c => { return ("0123456789".Contains(c)) ? 0 : c; }) == 0;

                if (numberValue.Length != 10 || !isAllDigits)
                    errorMessage += "The personal number has to be all digits and ten digits long!";
            }

            if (errorMessage.Length < 1)
            {
                ui.DataController.CreateData(targetType, fields.Select(f => f.Value).ToList());
                ui.SetActiveContext(new MainMenu(ui));
            }
        }
    }
}
