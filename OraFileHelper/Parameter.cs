using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OraFileHelper
{
    public abstract class Parameter
    {
        public string Name { get; set; }

        public sealed override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            ToString(builder, 0);
            return builder.ToString();
        }

        public void ToString(StringBuilder builder, int indent)
        {
            builder.Append(Name);
            builder.Append(" =");
            ToValueString(builder, indent);
        }

        protected abstract void ToValueString(StringBuilder builder, int indent);
    }
}
