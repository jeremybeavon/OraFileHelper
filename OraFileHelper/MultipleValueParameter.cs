using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OraFileHelper
{
    public sealed class MultipleValueParameter : Parameter
    {
        public MultipleValueParameter()
        {
            Values = new List<ParameterValue>();
        }

        public IList<ParameterValue> Values { get; set; }

        protected override void ToValueString(StringBuilder builder, int indent)
        {
            builder.Append(" (");
            builder.Append(string.Join(", ", Values.Select(value => value.ToString())));
            builder.Append(")");
        }
    }
}
