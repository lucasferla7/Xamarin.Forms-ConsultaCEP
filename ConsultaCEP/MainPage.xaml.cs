using ConsultaCEP.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConsultaCEP
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Buscar.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            var cep = Cep.Text.Trim();
            if (CepValido(cep))
            {
                try
                {
                    var endereco = ViaCepService.BuscarEnderecoViaCep(cep);


                    if (endereco != null)
                    {
                        Resultado.Text = $"Endereço: {endereco.Logradouro}, Bairro {endereco.Bairro}, Cidade {endereco.Localidade} - {endereco.Uf},";
                    } 
                    else
                    {
                        DisplayAlert("ERRO", $"O endereço não foi encontrado para o CEP informado: {cep}", "OK");
                    }
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRITICO", e.Message, "OK");
                }
            }
        }

        private bool CepValido(string cep)
        {
            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "CEP inválido! O CEP deve conter 8 caracteres", "OK");
                return false;
            }

            var novoCep = 0;
            if (!int.TryParse(cep, out novoCep))
            {
                DisplayAlert("Erro", "CEP inválido! O CEP deve ser composto apensar por números", "OK");
                return false;
            }

            return true;
        }
    }
}
