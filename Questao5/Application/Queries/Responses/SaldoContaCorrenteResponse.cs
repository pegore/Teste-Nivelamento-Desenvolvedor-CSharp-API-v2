namespace Questao5.Application.Queries.Responses
{
    public class SaldoContaCorrenteResponse
    {
        public int Numero { get; set; }
        public string Nome { get; set; }
        public DateTime DataHoraConsulta { get; set; } = DateTime.Now;
        public decimal Saldo { get; set; }
    }
}
