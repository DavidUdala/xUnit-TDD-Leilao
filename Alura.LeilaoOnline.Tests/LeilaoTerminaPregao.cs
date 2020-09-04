using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1350, 1250, new double[] { 800, 1150, 1250, 1350, 1400 })]
        public void RetornaValorSuperioMaisProximoDadoLeilaoNessaModalidade(double valorEsperado, double valorDestino,
            double[] ofertas)
        {
            //Arranje

            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            //Assert
            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                    leilao.RecebeLance(fulano, ofertas[i]);
                else
                    leilao.RecebeLance(maria, ofertas[i]);
            }
            leilao.TerminaPregao();
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1200, 990 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 700, 300 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //********* TRIPLE A **********
            //Arranje - cenário
            //GIVEN
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
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

            //Act - método sob teste
            //WHEN
            leilao.TerminaPregao();

            //Assert
            //THEN
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //********* TRIPLE A **********
            //Arranje - cenário
            var modalidade = new MaiorValor();

            var leilao = new Leilao("Van Gogh", modalidade);

            //Assert
            var excessaoObtida = Assert.Throws<System.InvalidOperationException>(
                //act
                //GIVEN
                () => leilao.TerminaPregao()
            );

            var msgEsperada = "Erro ao encerrar Leilão que não estava em andamento.";
            Assert.Equal(msgEsperada, excessaoObtida.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //********* TRIPLE A **********
            //Arranje - cenário
            //GIVEN
            var modalidade = new MaiorValor();

            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();

            //Act - método sob teste
            //WHEN
            leilao.TerminaPregao();

            //Assert
            //THEN
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

    }
}
