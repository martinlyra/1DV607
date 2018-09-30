using _1DV607A2.Controller;
using _1DV607A2.Model;
using _1DV607A2.View.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.View
{
    class DataViewMenu: AbstractContext
    {
        enum ViewMode
        {
            DataView,
            ListCompact,
            ListVerbose,
            Exit
        }

        ViewMode viewMode = ViewMode.ListCompact;

        public DataViewMenu(UserInterface ui) : base(ui)
        {
            
        }

        public override void Draw()
        {
            Console.Clear();

            var buffer = $"Registery viewer ({ui.DataController.RetrieveByQuery(d=>d).Count()})\n\n";
            switch (viewMode)
            {
                default: case ViewMode.ListCompact: BuildCompactList(ref buffer); break;
                case ViewMode.ListVerbose: BuildVerboseList(ref buffer); break;
                case ViewMode.DataView: break;
            }

            Console.WriteLine(buffer);
        }

        public override void Update(ConsoleKeyInfo keyInfo)
        {
            
        }

        private void BuildCompactList(ref string buffer)
        {
            BuildList(ref buffer,
                (data) => $"{data.Name}, (ID: {data.ID}), {data.Boats.Count} boat(s)"
                );
        }

        private void BuildVerboseList(ref string buffer)
        {
            BuildList(ref buffer,
                (data) => $"{data.Name}, PN: {data.PersonalNumber}, ID: {data.ID}, boats"
                );
        }

        private void BuildList(ref string buffer, Func<MemberData, string> formatter)
        {
            var data = ui.DataController.RetrieveByQuery(
                obj => 
                {
                    if (obj.GetType() == typeof(MemberData))
                        return (MemberData)obj;
                    else return null;
                }
                );

            foreach (MemberData entry in data)
            {
                buffer += $"* {formatter.Invoke(entry)}\n";
            }
        }
    }
}
