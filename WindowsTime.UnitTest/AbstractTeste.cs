using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WindowsTime.UnitTest
{
    [TestClass]
    public abstract class AbstractTeste
    {
        public abstract void Arrange();
        public abstract void Act();

        [TestInitialize]
        public void Init()
        {
            this.Arrange();
        }
    }

    [TestClass]
    public abstract class AbstractTesteAutoAct
    {
        public abstract void Arrange();
        public abstract void Act();

        [TestInitialize]
        public void Init()
        {
            this.Arrange();
            this.Act();
        }
    }
}