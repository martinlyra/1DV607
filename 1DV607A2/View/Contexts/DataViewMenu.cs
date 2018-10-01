using _1DV607A2.Controller;
using _1DV607A2.Model;
using _1DV607A2.View.Contexts;
using _1DV607A2.View.Elements;
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
        List<ListEntry> listEntries = new List<ListEntry>();
        ListEntry activeSelection;

        public DataViewMenu(UserInterface ui) : base(ui)
        {
            UpdateList();
        }

        public override void Draw()
        {
            Console.Clear();

            var buffer = $"Registery viewer ({(viewMode == ViewMode.ListCompact? "Compact" : "Verbose")} mode, {ui.DataController.RetrieveByQuery(d=>d).Count()} entries)\n\n";
            DrawList(ref buffer);
            buffer += "\nUse UP- and DOWN-ARROW keys to for navigation\nX to change between Compact and Verbose listing\nENTER to view selection\nESC to go back";

            Console.WriteLine(buffer);
        }

        public override void Update(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                default: break;
                case ConsoleKey.UpArrow:
                    {
                        var index = listEntries.IndexOf(activeSelection);
                        index = Modulo(index - 1, listEntries.Count);
                        SetSelection(listEntries.ElementAt(index));
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        var index = listEntries.IndexOf(activeSelection);
                        index = Modulo(index + 1, listEntries.Count);
                        SetSelection(listEntries.ElementAt(index));
                        break;
                    }
                case ConsoleKey.X:
                    {
                        if (viewMode != ViewMode.DataView)
                            viewMode = (viewMode == ViewMode.ListCompact? ViewMode.ListVerbose : ViewMode.ListCompact);
                        UpdateList();
                        break;
                    }
                case ConsoleKey.Escape: ui.SetActiveContext(new MainMenu(ui)); break;
            }
        }

        int Modulo(int a, int b)
        {
            return (int)(a - (b * Math.Floor((double)a / b)));
        }

        void UpdateList()
        {
            switch (viewMode)
            {
                default: case ViewMode.ListCompact: BuildCompactList(); break;
                case ViewMode.ListVerbose: BuildVerboseList(); break;
                case ViewMode.DataView: break;
            }
        }

        private void DrawList(ref string buffer)
        {
            foreach (ListEntry entry in listEntries)
                buffer += $"{entry}\n";
        }

        private void BuildCompactList()
        {
            BuildList(
                (data) => $"{data.Name}, (ID: {data.ID}), {data.Boats.Count} boat(s)"
                );
        }

        private void BuildVerboseList()
        {
            BuildList(
                (data) => $"{data.Name}, PN: {data.PersonalNumber}, ID: {data.ID}\n" +
                $"{data.Boats.Select((boat) => $"* {boat.Length}metre {boat.Type.ToString()}")}"
                );
        }

        private void BuildList(Func<MemberData, string> formatter)
        {
            var data = ui.DataController.RetrieveByQuery(
                obj => 
                {
                    if (obj.GetType() == typeof(MemberData))
                        return (MemberData)obj;
                    else return null;
                }
                );

            listEntries.Clear();

            foreach (MemberData entry in data)
            {
                var le = new ListEntry(entry.ID, formatter.Invoke(entry));
                listEntries.Add(le);
            }

            SetSelection(listEntries.First());
        }

        private void SetSelection(ListEntry listEntry)
        {
            if (activeSelection != null)
                activeSelection.IsSelected = false;
            activeSelection = listEntry;
            activeSelection.IsSelected = true;
        }
    }
}
