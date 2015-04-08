using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OraFileHelper.Tests
{
    [TestClass]
    public class ConfigurationFileTests
    {
        private const string testOraFile = @"
SID_LIST_LISTENER =
  (SID_LIST =
    (SID_DESC =
      (SID_NAME = ORCL1)
      (SDU = 32768)
      (ORACLE_HOME = C:\oracle\product\11.2.0\dbhome_1)
    )
    (SID_DESC =
      (SID_NAME = ORCL2)
      (SDU = 32768)
      (ORACLE_HOME = C:\oracle\product\11.2.0\dbhome_1)
    )
  )

LISTENER =
  (DESCRIPTION_LIST =
    (DESCRIPTION =
      (ADDRESS =
        (PROTOCOL = TCP)
        (HOST = localhost)
        (PORT = 1521)
      )
    )
  )
";

        [TestMethod]
        public void Test_SerializeAndDeserializeYieldSameResults()
        {
            new ConfigurationFile(testOraFile).ToString().Should().Be(testOraFile);
        }
    }
}
