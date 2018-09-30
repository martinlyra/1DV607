using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.View.Contexts
{
    abstract class AbstractContext
    {
        protected readonly UserInterface ui;

        public AbstractContext(UserInterface ui)
        {
            this.ui = ui;
        }

        public virtual void Draw()
        {

        }

        public virtual void Update(ConsoleKeyInfo keyInfo)
        {

        }

        protected string BuildHorizontialLine(char linecharacter, int length)
        {
            return new string(linecharacter, length);
        }
    }
}
