using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using WindowsTime.Monitorador.Api;

namespace WindowsTime.Monitorador
{
    public abstract class Programa
    {
        private readonly Dictionary<string, bool> _areasVisitadas = new Dictionary<string, bool>();

        public TipoDePrograma Tipo { get; protected set; }
        public Process Processo { get; private set; }
        public string Nome { get; protected set; }
        public string Executavel { get; protected set; }
        public int TotalDeAreasVisitadas { get; private set; }

        public IEnumerable<string> AreasVisitadas { get { return _areasVisitadas.Keys.ToList(); } }
        public abstract Image Icone { get; }


        // construtor
        protected Programa(Process processo, string titulo)
        {
            Processo = processo;
            TotalDeAreasVisitadas = 1;

            _areasVisitadas.Add(titulo, true);
        }

        public static Programa Criar(int windowsHandle, string titulo)
        {
            var processo = WindowsApi.GetProcess(windowsHandle);
            var windowsStoreApp = WindowsApi.IsWindowsStoreApp(processo);

            var programa = (windowsStoreApp)
                ? new ProgramaWindowsStore(processo, titulo) as Programa
                : new ProgramaWin32(processo, titulo);

            return programa;
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