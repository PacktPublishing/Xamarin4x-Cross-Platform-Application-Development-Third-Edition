using System;
using NUnit.Framework;

namespace XamSnap.Tests
{
    public class BaseTest
    {
        [SetUp]
        public virtual void SetUp()
        {
            ServiceContainer.Register<IWebService>(() => new FakeWebService { SleepDuration = 0 });
            ServiceContainer.Register<ISettings>(() => new FakeSettings());
        }
    }
}

