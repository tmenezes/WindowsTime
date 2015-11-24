using System;
using System.Collections.Generic;

namespace WindowsTime.Dominio
{
    public class AtividadeDoUsuario
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Data { get; set; }

        public virtual IList<Janela> Janelas { get; set; }


        public AtividadeDoUsuario()
        {
        }

        public AtividadeDoUsuario(Usuario usuario)
        {
            Usuario = usuario;
            Data = DateTime.Today;
            Janelas = new List<Janela>();
        }
    }
}