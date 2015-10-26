using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WindowsTime.Core.Dados;

namespace WindowsTime.Core.Notificador
{
    public static class ClienteHttp
    {
        private const string URI_UTILIZACAO_DE_JANELA = "http://localhost:8080/tempo";

        public static void PostarUtilizacaoDeProgramas(UtilizacaoDePrograma utilizacaoDePrograma)
        {
            var uri = new Uri(URI_UTILIZACAO_DE_JANELA);
            var conteudo = new StringContent("", Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.PostAsync(uri, conteudo);
        }
    }
}
