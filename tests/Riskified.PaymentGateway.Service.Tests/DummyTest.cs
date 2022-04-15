using NUnit.Framework;

// ReSharper disable All
#pragma warning disable

namespace Camilyo.MyService.Service.Tests
{
    /// <summary>
    /// This class is added as a workaround to make sure NUnit assembly is loaded by referencing it directly.
    /// Without it, when using Camilyo.Framework.Testing + FluentAssertions, tests will not run.
    /// </summary>
    public class DummyTest
    {
        public void Pass()
        {
            Assert.Pass();
        }
    }
}
