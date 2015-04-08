using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OraFileHelper
{
    public sealed class ParameterGroup : Parameter
    {
        public ParameterGroup()
        {
            Parameters = new List<Parameter>();
        }

        public IList<Parameter> Parameters { get; set; }

        protected override void ToValueString(StringBuilder builder, int indent)
        {
            if (Parameters == null)
            {
                return;
            }

            indent++;
            string indentText = string.Empty.PadLeft(indent * 2, ' ');
            foreach (Parameter parameter in Parameters)
            {
                builder.AppendLine();
                builder.Append(indentText);
                builder.Append("(");
                parameter.ToString(builder, indent);
                if (parameter is ParameterGroup)
                {
                    builder.AppendLine();
                    builder.Append(indentText);
                }

                builder.Append(")");
            }
        }
    }
}
