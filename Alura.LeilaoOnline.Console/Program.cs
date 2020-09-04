using Alura.LeilaoOnline.Core;
using System;

namespace Alura.LeilaoOnline.Console
{
    class Program
    {
        static void Main()
        {
            LeilaoComVariosLances();
            LeilaoComApenasUmLance();
        }
        private static void LeilaoComVariosLances()
        {
            //********* TRIPLE A **********
            //Arranje - cenário
            //GIVEN
            var modalidade = new MaiorValor();

            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 990);

            //Act - método sob teste
            //WHEN
            leilao.TerminaPregao();

            //Assert
            //THEN
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);
        }


        private static void LeilaoComApenasUmLance()
        {
            //********* TRIPLE A **********
            //Arranje - cenário
            //GIVEN
            var modalidade = new MaiorValor();

            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            //WHEN
            leilao.TerminaPregao();

            //Assert
            //THEN
            var valorEsperado = 800;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);

        }



        private static void Verifica(double esperado, double obtido)
        {
            var cor = System.Console.ForegroundColor;
            if (esperado == obtido)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("TESTE OK");
            }
            else
            {
                System.Console.WriteLine($"TESTE FALHOU! Esperado: {esperado}, obitido: {obtido}.");
                System.Console.ForegroundColor = ConsoleColor.Red;
            }
            
            System.Console.ForegroundColor = cor;
        }
    }
}
