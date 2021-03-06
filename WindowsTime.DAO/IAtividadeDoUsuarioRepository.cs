﻿using WindowsTime.Core.Dominio;

namespace WindowsTime.DAO
{
    public interface IAtividadeDoUsuarioRepository
    {
        AtividadeDoUsuario ObterAtividadeDoUsuarioDoDia(Usuario usuario);
        void Save(AtividadeDoUsuario atividadeDoUsuario);
    }
}