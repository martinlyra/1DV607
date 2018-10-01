using _1DV607A2.Controller;
using _1DV607A2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1DV607A2.View
{
    class SimpleUserInterface
    {
        enum ListDisplayMode
        {
            Compact,
            Verbose
        }

        enum DataMode
        {
            View,
            Create,
            Edit,
            Delete
        }

        public SimpleUserInterface()
        {
            DataController = new DataController();
        }

        public void RunLoop()
        {
            //TODO: Add an draw and update loop for drawing the main menu and then reading input
            try
            {
                Console.Clear();
                Console.WriteLine(
                    "The Jolly Pirate - Registry\n\n" +
                    "1. View members (Compact)\n" +
                    "2. View members (Verbose)\n" +
                    "3. View boats\n" +
                    "4. Create member\n" +
                    "5. Create boat\n" +
                    "\n" +
                    "Press the corresponding digit key for the option you'd like to use"
                    );
                var input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.D1: ShowMemberMenu(DataMode.View); break;
                    case ConsoleKey.D2: ShowMemberMenu(DataMode.View, ListDisplayMode.Verbose); break;
                    case ConsoleKey.D3: ShowBoatMenu(DataMode.View); break;
                    case ConsoleKey.D4: ShowMemberMenu(DataMode.Create); break;
                    case ConsoleKey.D5: ShowBoatMenu(DataMode.Create); break;
                    default: break;
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.ToString());
                Console.WriteLine("\nPress any key to return to main menu...");

                Console.ReadKey();
            }
        }

        void ShowMemberMenu(DataMode dataMode, ListDisplayMode displayMode = ListDisplayMode.Compact, string selectedID = null)
        {
            Console.Clear();

            string selected = selectedID;
            MemberData selectedData = null;
            if (dataMode != DataMode.Create)
            {
                if (selected == null)
                    selected = SelectMember($"Please select the member whose details you wish to {(dataMode == DataMode.View ? "view" : "edit")}:\n", displayMode);

                selectedData = (MemberData)DataController.RetrieveByID(selected);
            }

            switch (dataMode)
            {
                case DataMode.Create: case DataMode.Edit:
                    {
                        Dictionary<string, object> arguments = new Dictionary<string, object>();

                        Console.WriteLine("Please fill out the following fields");
                        Console.Write("Name: ");
                        arguments.Add("name", Console.ReadLine());

                        Console.Write("Personal Number: ");
                        var pn = ReadValidInput((s) => {
                            var res = s.Length != 10 || s.Sum(c => { return ("0123456789".Contains(c)) ? 0 : c; }) > 0;
                            if (res)
                                Console.WriteLine("The personal number has to be all digits and ten digits long!");
                            return res;
                            });
                        arguments.Add("personal-num", pn);

                        if (dataMode == DataMode.Create)
                            DataController.CreateData(typeof(MemberData), arguments);
                        else
                            DataController.ChangeData(selected, arguments);
                        break;
                    }
                case DataMode.View:
                    {
                        Console.Clear();
                        Console.Write(
                            $"Member ID: {selectedData.ID}\n" +
                            $"Time of creation: {selectedData.Timestamp}\n\n" +
                            $"Name: {selectedData.Name}\n" +
                            $"Personal Number: {selectedData.PersonalNumber}\n" +
                            $"Boats: ({selectedData.Boats.Count})\n");
                        foreach (BoatData boat in selectedData.Boats)
                            Console.WriteLine($" - {boat.Length}m {boat.Type} (Boat ID: {boat.ID})");

                        Console.WriteLine("\nPress E to edit this - Press D to delete this - Any else to return to main menu...");

                        var key = Console.ReadKey().Key;

                        switch (key)
                        {
                            case ConsoleKey.E: ShowMemberMenu(DataMode.Edit, selectedID: selected); break;
                            case ConsoleKey.D: ShowDeleteConfirmation(selected); break;
                            default: return;
                        }

                        break;
                    }
            }
        }

        void ShowBoatMenu(DataMode dataMode, string selectedID = null)
        {
            Console.Clear();

            string selected = selectedID;
            BoatData selectedData = null;
            if (dataMode != DataMode.Create)
            {
                if (selected == null)
                    selected = SelectBoat($"Please select the boat whose details you wish to {(dataMode == DataMode.View?"view":"edit")}:\n");

                selectedData = (BoatData)DataController.RetrieveByID(selected);
            }

            switch (dataMode)
            {
                case DataMode.Create: case DataMode.Edit:
                    {
                        Dictionary<string, object> arguments = new Dictionary<string, object>();

                        var owner = SelectMember("Please select the owner to register the new boat on\n");
                        arguments.Add("owner", owner);

                        Console.Write("Specify length of vessel in metre (m):");
                        arguments.Add("length", int.Parse(Console.ReadLine()));

                        var boatType = Enum.Parse(typeof(BoatType), ListSelection<string>("Please specify type of vessel", Enum.GetNames(typeof(BoatType)).ToList<object>()));
                        arguments.Add("type", boatType);

                        if (dataMode == DataMode.Create)
                            DataController.CreateData(typeof(BoatData), arguments);
                        else
                            DataController.ChangeData(selected, arguments);
                        break;
                    }
                case DataMode.View:
                    {
                        Console.Clear();
                        Console.Write(
                            $"Boat ID: {selectedData.ID}\n" +
                            $"Time of creation: {selectedData.Timestamp}\n\n" +
                            $"Length: {selectedData.Length} metre\n" +
                            $"Type: {selectedData.Type}\n" + 
                            ( selectedData.Owner == null ? $"Owner: Owner not found\n" :
                            $"Owner: {selectedData.Owner.Name} (M-ID: {selectedData.Owner.ID})\n"));

                        Console.WriteLine("\nPress E to edit this - Press D to delete this - Any else to return to main menu...");

                        var key = Console.ReadKey().Key;

                        switch (key)
                        {
                            case ConsoleKey.E: ShowBoatMenu(DataMode.Edit, selected); break;
                            case ConsoleKey.D: ShowDeleteConfirmation(selected); break;
                            default: return;
                        }

                        break;
                    }
            }
        }

        void ShowDeleteConfirmation(string selected)
        {
            Console.WriteLine("\nAre you sure you want to delete this selection? (y/n)");
            var response = ReadValidInput((s) =>
            {
                s = s.ToLower();
                return !(s == "y" || s == "n");
            }).ToLower();

            if (response == "y")
                DataController.DeleteData(selected);
        }

        string SelectMember(string topMessage, ListDisplayMode listDisplay = ListDisplayMode.Compact)
        {
            var members = DataController.RetrieveByQuery(d => d.GetType() == typeof(MemberData)).ToList<object>();

            return ListSelection<MemberData>(topMessage, members, listDisplay).ID;
        }

        string SelectBoat(string topMessage, ListDisplayMode listDisplay = ListDisplayMode.Compact)
        {
            var boats = DataController.RetrieveByQuery(d => d.GetType() == typeof(BoatData)).ToList<object>();

            return ListSelection<BoatData>(topMessage, boats, listDisplay).ID;
        }

        T ListSelection<T>(string topMessage, List<object> dataList, ListDisplayMode listDisplay = ListDisplayMode.Compact)
        {
            int selection = -1;
            while (selection >= dataList.Count() || selection < 0)
            {
                Console.Clear();
                Console.WriteLine(topMessage);
                PrintList(dataList.ToList(), listDisplay);
                Console.WriteLine("\nWrite the index number for the item you want to select");
                int.TryParse(Console.ReadLine(), out selection);
            }

            return (T)dataList.ElementAt(selection);
        }

        void PrintList(List<object> list, ListDisplayMode displayMode)
        {
            foreach (object entry in list)
            {
                if (entry == null)
                    continue;

                var line = $"{list.IndexOf(entry)} ";

                if (entry.GetType() == typeof(MemberData))
                    line += MemberToString((MemberData)entry, displayMode);
                else if (entry.GetType() == typeof(BoatData))
                    line += BoatToString((BoatData)entry, displayMode);
                else
                    line += entry.ToString();
                
                Console.WriteLine(line);
            }
        }

        string BoatToString(BoatData data, ListDisplayMode displayMode)
        {
            var owner = data.Owner;
            return $"ID: {data.ID}, {data.Length}m {data.Type}, " + (owner == null ? "ownerless" :$" owned by {owner.Name} (ID: {owner.ID})");
        }

        string MemberToString(MemberData data, ListDisplayMode displayMode)
        {
            string result = "";
            if (displayMode == ListDisplayMode.Compact)
            {
                result += $"{data.Name}, PN: {data.PersonalNumber} ID: {data.ID}, Boats;\n";
                foreach (BoatData boat in data.Boats)
                    result += $"   {boat.Length}m {boat.Type} (ID: {boat.ID})\n";
            }
            else
                result += $"{data.Name}, ID: {data.ID}, {data.Boats.Count} boat(s)";

            return result;
        }

        string ReadValidInput(Func<string, bool> filterFunction)
        {
            string input = Console.ReadLine();
            while (filterFunction.Invoke(input))
                input = Console.ReadLine();
            return input;
        }

        public bool IsExiting { get; set; } = false;

        internal DataController DataController { get; private set; }
    }
}
