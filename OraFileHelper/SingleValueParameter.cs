using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OraFileHelper
{
    public sealed class SingleValueParameter : Parameter
    {
        public ParameterValue Value { get; set; }

        protected override void ToValueString(StringBuilder builder, int indent)
        {
            if (Value != null)
            {
                builder.Append(" ");
                builder.Append(Value.ToString());
            }
        }
    }
}
