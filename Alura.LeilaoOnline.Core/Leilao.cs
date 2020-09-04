using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }

    public class Leilao
    {
        private Interessada _ultimoCliente;
        private IList<Lance> _lances;
        private IModalidadeAvaliacao _avaliador;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }


        public double ValorDestino { get;}

        public Leilao(string peca, IModalidadeAvaliacao avaliador )
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            _avaliador = avaliador;
        }

        private bool NovoLanceAceito(Interessada cliente)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento && (cliente != _ultimoCliente));
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceAceito(cliente))
            {
                if (cliente != _ultimoCliente)
                {
                    _lances.Add(new Lance(cliente, valor));
                    _ultimoCliente = cliente;
                }

            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
                throw new System.InvalidOperationException("Erro ao encerrar Leilão que não estava em andamento.");

            Ganhador = _avaliador.Avalia(this);
            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}