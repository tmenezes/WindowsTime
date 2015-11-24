using Ploeh.AutoFixture;
using WindowsTime.Core.Dominio;
using WindowsTime.Core.DTO;

namespace WindowsTime.UnitTest
{
    public static class StubHelper
    {
        private static readonly Fixture _fixture = new Fixture();

        public static AtividadeDoUsuarioDTO NovoAtividadeDoUsuarioDTO()
        {
            return NovoAtividadeDoUsuarioDTO(5);
        }
        public static AtividadeDoUsuarioDTO NovoAtividadeDoUsuarioDTO(int qtdeJanelas)
        {
            return NovoAtividadeDoUsuarioDTO(1, qtdeJanelas);
        }
        public static AtividadeDoUsuarioDTO NovoAtividadeDoUsuarioDTO(int qtdeProgramas, int qtdeJanelas)
        {
            var atividadeDTO = _fixture.Build<AtividadeDoUsuarioDTO>()
                                      .With(a => a.Programas, _fixture.Build<ProgramaDTO>()
                                                                      .Without(x => x.Icone)
                                                                      .With(x => x.Janelas, _fixture.Build<JanelaDTO>().CreateMany(qtdeJanelas))
                                                                      .CreateMany(qtdeProgramas))
                                      .Create();

            return atividadeDTO;
        }

        public static Programa NovoPrograma()
        {
            return _fixture.Build<Programa>().Create();
        }

        public static Usuario NovoUsuario()
        {
            return _fixture.Build<Usuario>().Without(u => u.Id).Create();
        }
    }
}