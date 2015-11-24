using System.Collections.Generic;
using WindowsTime.Dominio;

namespace WindowsTime.Core.DTO
{
    public class UtilizacaoDTO
    {
        public string EmailDoUsuario { get; set; }

        public IEnumerable<ProgramaDTO> Programas { get; set; }

        public UtilizacaoDTO()
        {
        }
        public UtilizacaoDTO(IEnumerable<ProgramaDTO> programas)
        {
            EmailDoUsuario = Usuario.Corrente.Email;
            Programas = programas;
        }
    }
}
