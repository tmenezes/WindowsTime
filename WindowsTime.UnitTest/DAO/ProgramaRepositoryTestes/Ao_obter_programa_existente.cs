﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsTime.DAO;

namespace WindowsTime.UnitTest.DAO.ProgramaRepositoryTestes
{
    [TestClass]
    public class Ao_obter_programa_existente : AbstractDaoTesteAutoAct
    {
        public override void Arrange()
        {
        }

        public override void Act()
        {
        }

        [TestMethod]
        public void Deve_obter_com_sucesso()
        {
            var programa = new ProgramaRepository().ObterProgramaPorNome("Visual Studio 2050");

            Assert.IsNotNull(programa);
            Assert.AreEqual(1, programa.Id);
            Assert.AreEqual("Visual Studio 2050", programa.Nome);
        }
    }
}