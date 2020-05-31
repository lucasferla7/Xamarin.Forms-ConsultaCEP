using ConsultaCEP.Service.Model;
using Newtonsoft.Json;
using System.Net;

namespace ConsultaCEP.Service
{
    public class ViaCepService
    {
        private static string Url = "http://viacep.com.br/ws/{0}/json";
    
        public static EnderecoModel BuscarEnderecoViaCep(string cep)
        {
            var novaUrl = string.Format(Url, cep);

            using (var wc = new WebClient())
            {
                var conteudo = wc.DownloadString(novaUrl);

                var endereco = JsonConvert.DeserializeObject<EnderecoModel>(conteudo);

                if (endereco.Cep == null)
                    return null;

                return endereco;
            }
        }
    }
}
