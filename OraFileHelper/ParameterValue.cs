using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OraFileHelper.Parser;

namespace OraFileHelper
{
    public sealed class ParameterValue
    {
        public ParameterValue()
        {
        }

        internal ParameterValue(OraParser.ValueContext value)
        {
            if (value.WORD() != null)
            {
                Value = value.WORD().GetText();
            }
            else if (value.QUOTED_STRING() != null)
            {
                string textValue = value.QUOTED_STRING().GetText();
                switch (textValue[0])
                {
                    case '\'':
                        QuoteType = QuoteType.Single;
                        break;
                    case '"':
                        QuoteType = QuoteType.Double;
                        break;
                }

                Value = textValue.Substring(1, textValue.Length - 2);
            }
        }

        public string Value { get; set; }

        public QuoteType QuoteType { get; set; }

        public override string ToString()
        {
            string quote = string.Empty;
            switch (QuoteType)
            {
                case QuoteType.Single:
                    quote = "'";
                    break;
                case QuoteType.Double:
                    quote = "\"";
                    break;
            }

            return string.Format("{0}{1}{0}", quote, Value);
        }
    }
}
