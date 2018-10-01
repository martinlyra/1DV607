using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.View.Elements
{
    class ListEntry
    {
        public ListEntry(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return (IsSelected ? '>' : ' ') + " " + Value;
        }

        public void OnClicked()
        {
            Command.Invoke();
        }

        public bool IsSelected { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Action Command { get; set; }
    }
}
