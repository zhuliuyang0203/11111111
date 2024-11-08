using NUnit.Framework;
using System.Collections.Generic;

namespace OpenQA.Selenium
{
    [TestFixture]
    public class CommandTests
    {
        [Test]
        public void CommandSerializesAnonymousType()
        {
            var parameters = new Dictionary<string, object>
            {
                ["arg"] = new { param1 = true, param2 = false },
            };

            var command = new Command(new SessionId("session"), "test command", parameters);

            Assert.That(command.ParametersAsJsonString, Is.EqualTo("""{"arg":{"param1":true,"param2":false}}"""));
        }
    }
}
