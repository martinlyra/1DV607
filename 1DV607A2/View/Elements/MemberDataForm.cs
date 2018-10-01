using _1DV607A2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.View.Elements
{
    class MemberDataForm: AbstractDataForm
    {
        public MemberDataForm(MemberData associatedObject) : base(associatedObject)
        {
            inputFields.Add(new TextInputField("Name", associatedObject?.Name));
            inputFields.Add(new TextInputField("Personal Number", associatedObject?.PersonalNumber));
        }
    }
}
