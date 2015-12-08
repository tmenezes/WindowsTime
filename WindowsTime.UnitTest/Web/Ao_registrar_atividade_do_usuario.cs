using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using WindowsTime.Core.Dominio;
using WindowsTime.Core.DTO;
using WindowsTime.DAO;
using WindowsTime.Web.AppCode;

namespace WindowsTime.UnitTest.Web
{
    [TestClass]
    public abstract class Ao_registrar_atividade_do_usuario : AbstractTesteAutoAct
    {
        AtividadeDoUsuarioDTO atividadeDTO;
        Usuario usuario;
        Programa programa1, programa2;

        IUsuarioRepository mockIUsuarioRepository;
        IAtividadeDoUsuarioRepository mockIAtividadeDoUsuarioRepository;
        IProgramaRepository mockIProgramaRepository;

        RegistradorDeAtividadeDoUsuario registradorDeAtividade;
        PrivateObject registradorDeAtividadePrivateObject;

        public override void Arrange()
        {
            atividadeDTO = StubHelper.NovoAtividadeDoUsuarioDTO(qtdeProgramas: 2, qtdeJanelas: 3);
            usuario = new Usuario(atividadeDTO.EmailDoUsuario);

            programa1 = StubHelper.NovoPrograma();
            programa2 = StubHelper.NovoPrograma();

            mockIUsuarioRepository = MockRepository.GenerateMock<IUsuarioRepository>();
            mockIUsuarioRepository.Expect(mock => mock.ObterUsuarioPorEmail(atividadeDTO.EmailDoUsuario))
                                  .Repeat.Once()
                                  .Return(usuario);

            mockIAtividadeDoUsuarioRepository = MockRepository.GenerateMock<IAtividadeDoUsuarioRepository>();

            mockIProgramaRepository = MockRepository.GenerateMock<IProgramaRepository>();
            mockIProgramaRepository.Expect(mock => mock.ObterProgramaPorNome(atividadeDTO.Programas.First().Nome))
                                   .Repeat.Times(3)
                                   .Return(programa1);
            mockIProgramaRepository.Expect(mock => mock.ObterProgramaPorNome(atividadeDTO.Programas.ElementAt(1).Nome))
                                   .Repeat.Times(3)
                                   .Return(programa2);

            registradorDeAtividade = new RegistradorDeAtividadeDoUsuario(mockIAtividadeDoUsuarioRepository, mockIProgramaRepository, mockIUsuarioRepository);
            registradorDeAtividadePrivateObject = new PrivateObject(registradorDeAtividade);
        }

        public override void Act()
        {
            registradorDeAtividadePrivateObject.Invoke("RegistrarUtilizacaoDeProgramas", atividadeDTO);
        }


        [TestMethod]
        public void Deve_obter_usuario()
        {
            mockIUsuarioRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void Deve_obter_programas()
        {
            mockIProgramaRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void Deve_salvar_atividade()
        {
            mockIAtividadeDoUsuarioRepository.VerifyAllExpectations();
        }


        [TestClass]
        public class Para_nova_atividade : Ao_registrar_atividade_do_usuario
        {
            AtividadeDoUsuario atividadeSalva = null;

            public override void Arrange()
            {
                base.Arrange();

                mockIAtividadeDoUsuarioRepository.Expect(mock => mock.ObterAtividadeDoUsuarioDoDia(usuario))
                                                 .Repeat.Once()
                                                 .Return(null);

                mockIAtividadeDoUsuarioRepository.Expect(mock => mock.Save(null)).IgnoreArguments()
                                                 .Repeat.Once()
                                                 .WhenCalled(a => atividadeSalva = a.Arguments.First() as AtividadeDoUsuario);
            }

            [TestMethod]
            public void Quantidade_de_janelas_deve_ser_6()
            {
                Assert.IsNotNull(atividadeSalva);
                Assert.AreEqual(6, atividadeSalva.Janelas.Count);
            }
        }

        [TestClass]
        public class Para_atividade_existente : Ao_registrar_atividade_do_usuario
        {
            AtividadeDoUsuario atividadeSalva = null;
            AtividadeDoUsuario atividadeDoUsuario = null;

            public override void Arrange()
            {
                base.Arrange();

                atividadeDoUsuario = new AtividadeDoUsuario(usuario);
                atividadeDoUsuario.Janelas.Add(new Janela(atividadeDoUsuario, "Teste", programa1, 1));

                mockIAtividadeDoUsuarioRepository.Expect(mock => mock.ObterAtividadeDoUsuarioDoDia(usuario))
                                                 .Repeat.Once()
                                                 .Return(atividadeDoUsuario);

                mockIAtividadeDoUsuarioRepository.Expect(mock => mock.Save(null)).IgnoreArguments()
                                                 .Repeat.Once()
                                                 .WhenCalled(a => atividadeSalva = a.Arguments.First() as AtividadeDoUsuario);
            }

            [TestMethod]
            public void Quantidade_de_janelas_deve_ser_7()
            {
                Assert.IsNotNull(atividadeSalva);
                Assert.AreEqual(7, atividadeSalva.Janelas.Count);
            }
        }
    }
}
