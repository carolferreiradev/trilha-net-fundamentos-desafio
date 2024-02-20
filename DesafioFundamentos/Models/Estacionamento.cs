using System.Globalization;
using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine();

            bool placaValida = ValidarPlaca(placa);

            if (placaValida == false) return;

            veiculos.Add(placa);
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            string placa = Console.ReadLine();

            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                bool horasValidas = int.TryParse(Console.ReadLine(), out var horas);

                if (horasValidas == false)
                {
                    Console.WriteLine("O valor digitado não é valido");
                    return;
                }

                decimal valorTotal = 0;
                valorTotal = precoInicial + (precoPorHora * horas);

                veiculos.Remove(placa);

                var culture = CultureInfo.CreateSpecificCulture("pt-BR");
                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: {valorTotal.ToString("C", culture)}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");

                foreach (string veiculo in veiculos)
                {
                    Console.WriteLine(veiculo);
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        private bool ValidarPlaca(string placa)
        {
            if (string.IsNullOrEmpty(placa))
            {
                Console.WriteLine("Placa do veículo inválida.");
                return false;
            }

            if (veiculos.Contains(placa))
            {
                Console.WriteLine("Veiculo já incluso no estacionamento.");
                return false;
            }

            string padrao = @"^[A-Za-z]{3}\d{1}[A-Za-z]\d{2}$";

            Regex regex = new Regex(padrao);

            if (regex.IsMatch(placa) == false)
            {
                Console.WriteLine("Placa inválida. Por favor digite uma placa que siga o padrão ABC1D23");
                return false;
            }

            return true;
        }
    }
}
