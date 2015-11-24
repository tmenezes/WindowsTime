using System.Collections.Generic;
using WindowsTime.Core.Dominio;

namespace WindowsTime.Core.DTO
{
    public class AtividadeDoUsuarioDTO
    {
        public string EmailDoUsuario { get; set; }

        public IEnumerable<ProgramaDTO> Programas { get; set; }

        public AtividadeDoUsuarioDTO()
        {
        }
        public AtividadeDoUsuarioDTO(IEnumerable<ProgramaDTO> programas)
        {
            EmailDoUsuario = Usuario.Corrente.Email;
            Programas = programas;
        }
    }
}
