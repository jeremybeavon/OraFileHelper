using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using OraFileHelper.Parser;

namespace OraFileHelper
{
    public sealed class ConfigurationFile
    {
        public ConfigurationFile()
        {
            Parameters = new List<Parameter>();
        }

        public ConfigurationFile(string text)
        {
            AntlrInputStream input = new AntlrInputStream(text);
            OraLexer lexer = new OraLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            OraParser parser = new OraParser(tokens);
            if (parser.NumberOfSyntaxErrors != 0)
            {
                throw new ArgumentException("text parameter could not be parsed.", "text");
            }

            Parameters = CreateParameters(parser.configuration_file().parameter());
        }

        public IList<Parameter> Parameters { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            ToString(builder);
            return builder.ToString();
        }

        public void ToString(StringBuilder builder)
        {
            foreach (Parameter parameter in Parameters)
            {
                builder.AppendLine();
                parameter.ToString(builder, 0);
                builder.AppendLine();
            }
        }

        private static Parameter CreateParameter(OraParser.ParameterContext parameter)
        {
            Parameter newParameter = null;
            if (parameter.value() != null)
            {
                newParameter = new SingleValueParameter()
                {
                    Value = new ParameterValue(parameter.value())
                };
            }
            else if (parameter.value_list() != null)
            {
                newParameter = new MultipleValueParameter()
                {
                    Values = parameter.value_list().value().Select(value => new ParameterValue(value)).ToList()
                };
            }
            else if (parameter.parameter() != null)
            {
                IList<Parameter> parameters = CreateParameters(parameter.parameter());
                newParameter = new ParameterGroup()
                {
                    Parameters = parameters
                };
            }

            if (newParameter == null)
            {
                throw new NotImplementedException("Unable to parsed parameter");
            }

            newParameter.Name = parameter.keyword().GetText();
            return newParameter;
        }

        private static IList<Parameter> CreateParameters(IEnumerable<OraParser.ParameterContext> parameters)
        {
            return parameters.Select(parameter => CreateParameter(parameter)).ToList();
        }
    }
}
