﻿using System;
using System.Collections.Generic;

namespace WindowsTime.Core.Dados
{
    public class UtilizacaoDePrograma
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Data { get; set; }

        public IEnumerable<DadosDoPrograma> Programas { get; set; }
    }
}
