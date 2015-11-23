using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using WindowsTime.Core.Dados;

namespace WindowsTime.Core.Notificador
{
    public static class ClienteHttp
    {
        private const string URI_UTILIZACAO_DE_JANELA = "http://localhost:49815/api/tempo";

        public static void PostarUtilizacaoDeProgramas(UtilizacaoDePrograma utilizacaoDePrograma)
        {
            var uri = new Uri(URI_UTILIZACAO_DE_JANELA);

            var conteudoJson = JsonConvert.SerializeObject(utilizacaoDePrograma);
            var conteudo = new StringContent(conteudoJson, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.PostAsync(uri, conteudo);
            }
        }
    }
}
