using System;

namespace WindowsTime.Core.Dominio
{
    public class Usuario
    {
        private static readonly Usuario _usuarioCorrente = new Usuario() { Id = 1, Nome = "Thiago Menezes", Email = "tmenezes@outlook.com.br" };

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataDeCadastro { get; set; }

        public static Usuario Corrente => _usuarioCorrente;


        public Usuario()
        {

        }

        public Usuario(string email)
        {
            Email = email;
        }
    }
}