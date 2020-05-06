using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using WindowsTime.Core.Monitorador.Api;

namespace WindowsTime.Core.Monitorador
{
    public abstract class Programa
    {
        private readonly Dictionary<string, bool> _areasVisitadas = new Dictionary<string, bool>();
        private static Programa _programaDesconhecido;

        public TipoDePrograma Tipo { get; protected set; }
        public Process Processo { get; protected set; }
        public string Nome { get; protected set; }
        public string Executavel { get; protected set; }
        public int TotalDeAreasVisitadas { get; private set; }

        public IEnumerable<string> AreasVisitadas { get { return _areasVisitadas.Keys.ToList(); } }
        public abstract Image Icon { get; }


        // construtor
        protected Programa(Janela janela)
        {
            TotalDeAreasVisitadas = 1;

            _areasVisitadas.Add(janela.Titulo, true);
        }

        public static Programa Carregar(Janela janela)
        {
            var processo = WindowsApi.GetProcess(janela.WindowsHandle);
            var isWindowsStoreApp = WindowsApi.IsWindowsStoreApp(processo);

            var programa = (isWindowsStoreApp)
                ? new ProgramaWindowsStore(processo, janela) as Programa
                : new ProgramaWin32(processo, janela);

            return programa;
        }

        public static Programa Desconhecido(Janela janela)
        {
            return _programaDesconhecido ?? (_programaDesconhecido = new ProgramaDesconhecido(janela));
        }


        public void NotificarNovaAreaAcessada(string novaArea)
        {
            if (!_areasVisitadas.ContainsKey(novaArea))
            {
                _areasVisitadas.Add(novaArea, true);
                TotalDeAreasVisitadas++;
            }
        }
    }
}