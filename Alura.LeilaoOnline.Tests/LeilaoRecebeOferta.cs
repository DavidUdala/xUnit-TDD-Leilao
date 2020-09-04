using Alura.LeilaoOnline.Core;
using Xunit;
using System.Linq;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
            int qtdeEsperada, double[] ofertas)
        {
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh",modalidade);

            var fulano = new Interessada("Fulano", leilao);

            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();


            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                    leilao.RecebeLance(fulano, ofertas[i]);
                else
                    leilao.RecebeLance(maria, ofertas[i]);
            }

            leilao.TerminaPregao();

            leilao.RecebeLance(fulano, 1000);

            var qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeObtida);
        }

        [Theory]
        [InlineData(1, new double[] { 800, 900 })]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimo(double qtdeEsperada, double[] ofertas)
        {
            var modalidade = new MaiorValor();

            var leilao = new Leilao("Van Gogh",modalidade);

            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();

            foreach (var valor in ofertas)
                leilao.RecebeLance(fulano, valor);

            leilao.TerminaPregao();

            leilao.RecebeLance(fulano, 1000);

            var qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeObtida);
        }
    }
}
