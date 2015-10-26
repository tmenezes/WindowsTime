namespace WindowsTime.Core.Dados
{
    public class Usuario
    {
        private static Usuario _usuarioCorrente = new Usuario() { Id = 1, Nome = "Thiago Menezes", Email = "tmenezes@outlook.com.br" };

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public static Usuario Corrente => _usuarioCorrente;
    }
}