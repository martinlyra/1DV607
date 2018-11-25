using _1DV607A2.View.Component;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1DV607A2.View.Window
{
    class ListSelectionView<T> : ListView<T>
    {
        public T SelectedItem { get; private set; }

        public override void OnItemSelected(ListItem<T> item)
        {
            SelectedItem = item.Data;
        }
    }
}
