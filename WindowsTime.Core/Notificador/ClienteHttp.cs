using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using WindowsTime.Core.DTO;

namespace WindowsTime.Core.Notificador
{
    public static class ClienteHttp
    {
        private static readonly string URI_UTILIZACAO_DE_JANELA =  $"{WindowsTimeConfigSection.Current.EnderecoDoServico}/atividade";

        public static void PostarUtilizacaoDeProgramas(AtividadeDoUsuarioDTO atividadeDoUsuarioDTO)
        {
            var uri = new Uri(URI_UTILIZACAO_DE_JANELA);

            var conteudoJson = JsonConvert.SerializeObject(atividadeDoUsuarioDTO);
            var conteudo = new StringContent(conteudoJson, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.PostAsync(uri, conteudo);
            }
        }
    }
}
