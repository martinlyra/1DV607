using _1DV607A2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.View.Elements
{
    abstract class AbstractDataForm
    {
        protected List<InputField> inputFields = new List<InputField>();

        public AbstractDataForm(DataObject associatedObject)
        {
            inputFields.Add(new TextInputField("ID", associatedObject?.ID));
            inputFields.Add(new TextInputField("Time created", associatedObject.Timestamp?.ToString()));
        }

        public virtual void Draw()
        {

        }
    }
}
